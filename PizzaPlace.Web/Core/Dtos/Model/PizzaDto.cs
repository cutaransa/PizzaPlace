using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaPlace.Web.Core.Dtos.Model
{
    public class PizzaDto
    {
        public int pizzaId { get; set; }
        public string code { get; set; }

        public decimal price { get; set; }
        public string size { get; set; }
        public int typeId { get; set; }
    }

    public class ViewPizzaDto : PizzaDto
    {
        public IEnumerable<PizzaTypeDto> pizzaTypes { get; set; }

    }

    public class ListPizzaDto : PizzaDto
    {
        public PizzaTypeDto pizzaType { get; set; }

    }

    public class AddPizzaDto : PizzaDto
    {

    }

    public class EditPizzaDto : PizzaDto
    {

    }
}