using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaPlace.Web.Core.Dtos.Model
{
    public class ModuleDto
    {
        public int moduleId { get; set; }

        public string controller { get; set; }

        public string action { get; set; }
    }

    public class ViewModuleDto : ModuleDto
    {

    }

    public class ListModuleDto : ModuleDto
    {

    }

    public class AddModuleDto : ModuleDto
    {

    }

    public class EditModuleDto : ModuleDto
    {

    }
}