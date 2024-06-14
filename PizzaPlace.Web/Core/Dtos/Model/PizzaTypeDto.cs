using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaPlace.Web.Core.Dtos.Model
{
    public class PizzaTypeDto
    {
        public int typeId { get; set; }
        public string code { get; set; }

        public string name { get; set; }
        public string ingredients { get; set; }
        public int categoryId { get; set; }
    }

    public class ViewPizzaTypeDto : PizzaTypeDto
    {
        public CategoryDto category { get; set; }
        public IEnumerable<CategoryDto> categories { get; set; }

    }

    public class ListPizzaTypeDto : PizzaTypeDto
    {
        public CategoryDto category { get; set; }

    }

    public class AddPizzaTypeDto : PizzaTypeDto
    {

    }

    public class EditPizzaTypeDto : PizzaTypeDto
    {

    }
}