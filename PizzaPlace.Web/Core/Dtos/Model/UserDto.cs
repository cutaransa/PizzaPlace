using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaPlace.Web.Core.Dtos.Model
{
    public class UserDto
    {
        public string id { get; set; }

        public string email { get; set; }

        public string userName { get; set; }

        public bool isActive { get; set; }

        public bool isDefault { get; set; }

        public RoleDto role { get; set; }
    }

    public class ViewUserDto : UserDto
    {

    }

    public class ListUserDto : UserDto
    {

    }

    public class AddUserDto : UserDto
    {

    }

    public class EditUserDto : UserDto
    {

    }
}