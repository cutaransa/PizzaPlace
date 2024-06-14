
using AutoMapper;
using PizzaPlace.Core;
using PizzaPlace.Core.Models;
using PizzaPlace.Core.Utilities;
using PizzaPlace.Web.Core.Dtos.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System;
using System.Net.Http;
using System.Diagnostics;
using System.Drawing;
using Hangfire;
using PizzaPlace.Core.Providers;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Web;
using PizzaPlace.Web.Core.Utilities;

namespace PizzaPlace.Web.Controllers.WebApi
{
    [Authorize]
    public class OrdersController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private static string _filePath;
        private static string _tempPath;
        private static TextInfo _ti;

        private string checkPathAndFile(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _ti = CultureInfo.CurrentCulture.TextInfo;
            _filePath = checkPathAndFile(HttpContext.Current.Server.MapPath("~/Files"));
            _tempPath = checkPathAndFile(HttpContext.Current.Server.MapPath("~/Uploads"));
        }

        [HttpPost]
        [Route("api/Orders/")]
        public IEnumerable<ListOrderDto> GetOrders()
        {
            var orders = _unitOfWork.Orders.GetOrders();

            return orders.Select(Mapper.Map<Order, ListOrderDto>);
        }

        [HttpPost]
        [Route("api/Orders/detail/")]
        public IHttpActionResult GetOrder(int id)
        {
            var order = _unitOfWork.Orders.GetOrder(id);

            if (order == null)
                return Content(HttpStatusCode.NotFound, "Order not found.");

            return Content(HttpStatusCode.OK, Mapper.Map<Order, ViewOrderDto>(order));
        }

        [HttpPost]
        [Route("api/Orders/upload/")]
        public async Task<HttpResponseMessage> Upload(HttpRequestMessage Request)
        {
            List<string> errors = new List<string>();
            errors = ModelErrorChecker.Check(ModelState);

            if (errors.Count == 0)
            {
                var userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();

                try
                {
                    if (!Request.Content.IsMimeMultipartContent())
                    {
                        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                    }

                    checkPathAndFile(_tempPath);

                    var multipartFormDataStreamProvider = new CustomUploadFile(_tempPath);

                    await Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);

                    string localFileName = multipartFormDataStreamProvider
                    .FileData.Select(multiPartData => multiPartData.LocalFileName).SingleOrDefault();

                    string tempFile = string.Format("{0}{1}", Guid.NewGuid().ToString(), Path.GetExtension(Path.GetFileName(localFileName)));
                    string sourceFile = System.IO.Path.Combine(_tempPath, Path.GetFileName(localFileName));
                    string destFile = System.IO.Path.Combine(_filePath, "Order", tempFile);

                    System.IO.File.Copy(sourceFile, destFile, true);

                    var file = new PizzaPlace.Core.Models.File()
                    {
                        FileName = Path.GetFileName(localFileName),
                        FileDestination = sourceFile,
                        TempName = tempFile,
                        TempDestination = destFile,
                        Description = "Order",
                        Status = "Processing",
                        UploadedById = userId,
                        UploadedDate = DateTime.Now
                    };
                    _unitOfWork.Files.Add(file);
                    _unitOfWork.Complete();

                    BackgroundJob.Enqueue(() => UploadUtility.ToOrders(file.FileId));

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(Path.GetFileName(localFileName))
                    };
                }
                catch (Exception)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Unable to saved file.")
                    };
                }
            }
            else
                return new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("No file chosen")
                };
        }

    }
}