
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

namespace PizzaPlace.Web.Controllers.WebApi
{
    [Authorize]
    public class PizzasController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public PizzasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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


    }
}