using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaPlace.Web.Core.Dtos.Model
{
    public class CategoryDto
    {
        public int categoryId { get; set; }

        public string name { get; set; }
    }

    public class ViewCategoryDto : CategoryDto
    {

    }

    public class ListCategoryDto : CategoryDto
    {

    }

    public class AddCategoryDto : CategoryDto
    {

    }

    public class EditCategoryDto : CategoryDto
    {

    }
}