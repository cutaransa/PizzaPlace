
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

namespace PizzaPlace.Web.Controllers.WebApi
{
    [Authorize]
    public class PizzaTypesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public PizzaTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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


    }
}