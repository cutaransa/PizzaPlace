
using AutoMapper;
using PizzaPlace.Core;
using PizzaPlace.Core.Models;
using PizzaPlace.Core.Utilities;
using PizzaPlace.Web.Core.Dtos.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace PizzaPlace.Web.Controllers.WebApi
{
    [Authorize]
    public class CategoriesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("api/Categories/")]
        public IEnumerable<ListCategoryDto> GetCategories()
        {
            var categories = _unitOfWork.Categories.GetCategories();

            return categories.Select(Mapper.Map<Category, ListCategoryDto>);
        }

        [HttpPost]
        [Route("api/Categories/detail/")]
        public IHttpActionResult GetCategory(int id)
        {

            var curCategory = _unitOfWork.Categories.GetCategory(id);

            if (curCategory == null)
                return Content(HttpStatusCode.NotFound, "Category not found.");

            var category = Mapper.Map<Category, CategoryDto>(curCategory);

            return Content(HttpStatusCode.OK, category);
        }

        [HttpPost]
        [Route("api/Categories/add/")]
        public IHttpActionResult Add(AddCategoryDto dto)
        {
            List<string> errors = new List<string>();
            errors = ModelErrorChecker.Check(ModelState);

            if (errors.Count == 0)
            {
                var category = new Category()
                {
                    Name = dto.name,
                };

                _unitOfWork.Categories.Add(category);
                _unitOfWork.Complete();
            }
            else
                return Content(HttpStatusCode.NotAcceptable, errors.ToList());

            return Ok();
        }

        [HttpPost]
        [Route("api/Categories/update/")]
        public IHttpActionResult Update(EditCategoryDto dto)
        {
            List<string> errors = new List<string>();
            errors = ModelErrorChecker.Check(ModelState);

            if (errors.Count == 0)
            {
                var category = _unitOfWork.Categories.GetCategory(dto.categoryId);

                category.Name = dto.name;

                _unitOfWork.Complete();
            }
            else
                return Content(HttpStatusCode.NotAcceptable, errors.ToList());

            return Ok();
        }

    }
}