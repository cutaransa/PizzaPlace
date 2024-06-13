using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PizzaPlace.Web.Core.Dtos.Model
{
    public class RoleDto
    {
        public string id { get; set; }

        [Required]
        public string name { get; set; }
    }

    public class ViewRoleDto : RoleDto
    {

    }

    public class ListRoleDto : RoleDto
    {

    }

    public class AddRoleDto : RoleDto
    {

    }

    public class EditRoleDto : RoleDto
    {

    }
}