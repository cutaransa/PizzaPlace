
using AutoMapper;
using PizzaPlace.Core;
using PizzaPlace.Core.Models;
using PizzaPlace.Core.Utilities;
using PizzaPlace.Web.Core.Dtos.Model;
using PizzaPlace.Web.Core.Dtos.Report;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace PizzaPlace.Web.Controllers.WebApi
{
    [Authorize]
    public class DashboardController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("api/Dashboard/get-all-transactions/")]
        public IEnumerable<TransactionDto> GetDashboards()
        {
            var details = _unitOfWork.OrderDetails.GetOrderDetails();

            return details.Select(Mapper.Map<OrderDetail, TransactionDto>);
        }

        [HttpPost]
        [Route("api/Dashboard/get-transaction/")]
        public IHttpActionResult GetTransaction(int id)
        {

            var curDetail = _unitOfWork.OrderDetails.GetOrderDetail(id);

            if (curDetail == null)
                return Content(HttpStatusCode.NotFound, "Category not found.");

            var detail = Mapper.Map<OrderDetail, TransactionDto>(curDetail);

            return Content(HttpStatusCode.OK, detail);
        }

    }
}