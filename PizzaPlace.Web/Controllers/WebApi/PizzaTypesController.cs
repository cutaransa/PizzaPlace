
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
using System.IO;
using System.Threading.Tasks;
using System.Globalization;
using System.Web;
using PizzaPlace.Core.Providers;
using Hangfire;
using PizzaPlace.Web.Core.Utilities;

namespace PizzaPlace.Web.Controllers.WebApi
{
    [Authorize]
    public class PizzaTypesController : ApiController
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

        public PizzaTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _ti = CultureInfo.CurrentCulture.TextInfo;
            _filePath = checkPathAndFile(HttpContext.Current.Server.MapPath("~/Files"));
            _tempPath = checkPathAndFile(HttpContext.Current.Server.MapPath("~/Uploads"));
        }

        [HttpPost]
        [Route("api/PizzaTypes/")]
        public IEnumerable<ListPizzaTypeDto> GetPizzaTypes()
        {
            var pizzaTypes = _unitOfWork.PizzaTypes.GetPizzaTypes();

            return pizzaTypes.Select(Mapper.Map<PizzaType, ListPizzaTypeDto>);
        }

        [HttpPost]
        [Route("api/PizzaTypes/detail/")]
        public IHttpActionResult GetPizzaType(int id)
        {
            var pizzaType = _unitOfWork.PizzaTypes.GetPizzaType(id);

            if (pizzaType == null)
                return Content(HttpStatusCode.NotFound, "PizzaType not found.");

            var curPizzaType = Mapper.Map<PizzaType, ViewPizzaTypeDto>(pizzaType);
            curPizzaType.categories = _unitOfWork.Categories.GetCategories().Select(Mapper.Map<Category, ListCategoryDto>);

            return Content(HttpStatusCode.OK, curPizzaType);
        }

        [HttpPost]
        [Route("api/PizzaTypes/add/")]
        public IHttpActionResult Add(AddPizzaTypeDto dto)
        {
            List<string> errors = new List<string>();
            errors = ModelErrorChecker.Check(ModelState);

            if (errors.Count == 0)
            {
                var userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();

                var pizzaType = new PizzaType()
                {
                    Code = dto.code,
                    Name = dto.name,
                    Ingredients =dto.ingredients,
                    CategoryId = dto.categoryId,
                    CreatedDate = DateTime.Now,
                    CreatedById = userId,
                    ModifiedDate = DateTime.Now,
                    ModifiedById = userId
                };

                _unitOfWork.PizzaTypes.Add(pizzaType);
                _unitOfWork.Complete();
            }
            else
                return Content(HttpStatusCode.NotAcceptable, errors.ToList());

            return Ok();
        }

        [HttpPost]
        [Route("api/PizzaTypes/update/")]
        public IHttpActionResult Update(EditPizzaTypeDto dto)
        {
            List<string> errors = new List<string>();
            errors = ModelErrorChecker.Check(ModelState);

            if (errors.Count == 0)
            {
                var userId = Request.GetOwinContext().Authentication.User.Identity.GetUserId();
                var pizzaType = _unitOfWork.PizzaTypes.GetPizzaType(dto.typeId);

                pizzaType.Code = dto.code;
                pizzaType.Name = dto.name;
                pizzaType.Ingredients = dto.ingredients;
                pizzaType.CategoryId = dto.categoryId;
                pizzaType.ModifiedDate = DateTime.Now;
                pizzaType.ModifiedById = userId;

                _unitOfWork.Complete();
            }
            else
                return Content(HttpStatusCode.NotAcceptable, errors.ToList());

            return Ok();
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/PizzaTypes/categories/")]
        public IEnumerable<ListCategoryDto> GetCategories()
        {
            var categories = _unitOfWork.Categories.GetCategories();

            return categories.Select(Mapper.Map<Category, ListCategoryDto>);

        }

        [HttpPost]
        [Route("api/PizzaTypes/upload/")]
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
                    string destFile = System.IO.Path.Combine(_filePath, "Type", tempFile);

                    System.IO.File.Copy(sourceFile, destFile, true);

                    var file = new PizzaPlace.Core.Models.File()
                    {
                        FileName = Path.GetFileName(localFileName),
                        FileDestination = sourceFile,
                        TempName = tempFile,
                        TempDestination = destFile,
                        Description = "Type",
                        Status = "Processing",
                        UploadedById = userId,
                        UploadedDate = DateTime.Now
                    };
                    _unitOfWork.Files.Add(file);
                    _unitOfWork.Complete();

                    BackgroundJob.Enqueue(() => UploadUtility.ToPizzaTypes(file.FileId));

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