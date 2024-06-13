
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
    public class ModulesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModulesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("api/Modules/")]
        public IEnumerable<ListModuleDto> GetModules()
        {
            var modules = _unitOfWork.Modules.GetModules();

            return modules.Select(Mapper.Map<Module, ListModuleDto>);
        }

        [HttpPost]
        [Route("api/Modules/detail/")]
        public IHttpActionResult GetModule(int id)
        {

            var curModule = _unitOfWork.Modules.GetModule(id);

            if (curModule == null)
                return Content(HttpStatusCode.NotFound, "Module not found.");

            var module = Mapper.Map<Module, ModuleDto>(curModule);

            return Content(HttpStatusCode.OK, module);
        }

        [HttpPost]
        [Route("api/Modules/add/")]
        public IHttpActionResult Add(AddModuleDto dto)
        {
            List<string> errors = new List<string>();
            errors = ModelErrorChecker.Check(ModelState);

            if (errors.Count == 0)
            {
                var module = new Module()
                {
                    Controller = dto.controller,
                    Action = dto.action,
                };

                _unitOfWork.Modules.Add(module);
                _unitOfWork.Complete();

                // added the new module to each role 
                var roles = _unitOfWork.Roles.GetDefaultUserRoles();
                foreach (var role in roles)
                {
                    var roleModule = new RoleModule()
                    {
                        RoleId = role.Id,
                        ModuleId = module.ModuleId,
                        IsEnabled = false
                    };

                    _unitOfWork.RoleModules.Add(roleModule);
                }
                _unitOfWork.Complete();
            }
            else
                return Content(HttpStatusCode.NotAcceptable, errors.ToList());

            return Ok();
        }

        [HttpPost]
        [Route("api/Modules/update/")]
        public IHttpActionResult Update(EditModuleDto dto)
        {
            List<string> errors = new List<string>();
            errors = ModelErrorChecker.Check(ModelState);

            if (errors.Count == 0)
            {
                var module = _unitOfWork.Modules.GetModule(dto.moduleId);

                module.Controller = dto.controller;
                module.Action = dto.action;

                _unitOfWork.Complete();
            }
            else
                return Content(HttpStatusCode.NotAcceptable, errors.ToList());

            return Ok();
        }

    }
}