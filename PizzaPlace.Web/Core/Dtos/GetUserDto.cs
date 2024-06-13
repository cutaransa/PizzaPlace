using PizzaPlace.Web.Core.Dtos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaPlace.Web.Core.Dtos
{
    public class GetUserDto
    {
        public string id { get; set; }

        public string email { get; set; }

        public string username { get; set; }

        public string name { get; set; }

        public ViewRoleDto role { get; set; }

        public bool isDefault { get; set; }
    }
}