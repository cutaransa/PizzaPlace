
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
    public class PizzasController : ApiController
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

        public PizzasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _ti = CultureInfo.CurrentCulture.TextInfo;
            _filePath = checkPathAndFile(HttpContext.Current.Server.MapPath("~/Files"));
            _tempPath = checkPathAndFile(HttpContext.Current.Server.MapPath("~/Uploads"));
        }

        [HttpPost]
        [Route("api/Pizzas/")]
        public IEnumerable<ListPizzaDto> GetPizzas()
        {
            var pizzas = _unitOfWork.Pizzas.GetPizzas();

            return pizzas.Select(Mapper.Map<Pizza, ListPizzaDto>);
        }

        [HttpPost]
        [Route("api/Pizzas/detail/")]
        public IHttpActionResult GetPizza(int id)
        {
            var pizza = _unitOfWork.Pizzas.GetPizza(id);

            if (pizza == null)
                return Content(HttpStatusCode.NotFound, "Pizza not found.");

            var curPizza = Mapper.Map<Pizza, ViewPizzaDto>(pizza);
            curPizza.pizzaTypes = _unitOfWork.PizzaTypes.GetPizzaTypes().Select(Mapper.Map<PizzaType, ListPizzaTypeDto>);

            return Content(HttpStatusCode.OK, curPizza);
        }

        [HttpPost]
        [Route("api/Pizzas/add/")]
        public IHttpActionResult Add(AddPizzaDto dto)
        {
            List<string> errors = new List<string>();
            errors = ModelErrorChecker.Check(ModelState);

            if (errors.Count == 0)
            {
                var userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();

                var pizza = new Pizza()
                {
                    Code = dto.code,
                    Price = dto.price,
                    Size =dto.size,
                    TypeId = dto.typeId,
                    CreatedDate = DateTime.Now,
                    CreatedById = userId,
                    ModifiedDate = DateTime.Now,
                    ModifiedById = userId
                };

                _unitOfWork.Pizzas.Add(pizza);
                _unitOfWork.Complete();
            }
            else
                return Content(HttpStatusCode.NotAcceptable, errors.ToList());

            return Ok();
        }

        [HttpPost]
        [Route("api/Pizzas/update/")]
        public IHttpActionResult Update(EditPizzaDto dto)
        {
            List<string> errors = new List<string>();
            errors = ModelErrorChecker.Check(ModelState);

            if (errors.Count == 0)
            {
                var userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
                var pizza = _unitOfWork.Pizzas.GetPizza(dto.typeId);

                pizza.Code = dto.code;
                pizza.Price = dto.price;
                pizza.Size = dto.size;
                pizza.TypeId = dto.typeId;
                pizza.ModifiedDate = DateTime.Now;
                pizza.ModifiedById = userId;

                _unitOfWork.Complete();
            }
            else
                return Content(HttpStatusCode.NotAcceptable, errors.ToList());

            return Ok();
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/Pizzas/types/")]
        public IEnumerable<ListPizzaTypeDto> GetTypes()
        {
            var types = _unitOfWork.PizzaTypes.GetPizzaTypes();

            return types.Select(Mapper.Map<PizzaType, ListPizzaTypeDto>);

        }

        [HttpPost]
        [Route("api/Pizzas/upload/")]
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
                    string destFile = System.IO.Path.Combine(_filePath, "Pizza", tempFile);

                    System.IO.File.Copy(sourceFile, destFile, true);

                    var file = new PizzaPlace.Core.Models.File()
                    {
                        FileName = Path.GetFileName(localFileName),
                        FileDestination = sourceFile,
                        TempName = tempFile,
                        TempDestination = destFile,
                        Description = "Pizza",
                        Status = "Processing",
                        UploadedById = userId,
                        UploadedDate = DateTime.Now
                    };
                    _unitOfWork.Files.Add(file);
                    _unitOfWork.Complete();

                    BackgroundJob.Enqueue(() => UploadUtility.ToPizzas(file.FileId));

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