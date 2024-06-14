using Hangfire;
using PizzaPlace.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using PizzaPlace.Core.Utilities;
using PizzaPlace.Web.Core.Dtos.File;
using PizzaPlace.Core.Models;

namespace PizzaPlace.Web.Core.Utilities
{
    public class UploadUtility
    {

        [AutomaticRetry(Attempts = 0)]
        public static void ToPizzaTypes(int fileId)
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            var file = _context.Files.SingleOrDefault(x => x.FileId == fileId);
            if (file == null)
            {
                return;
            }

            var user = _context.Users.SingleOrDefault(u => u.Id == file.UploadedById);
            if (user == null)
            {
                file.Status = "Failed";
                file.Remarks = "This user is not allowed to access this resource.";
                _context.SaveChanges();

                return;
            }

            try
            {
                DataTable data = Excel.ImportExcelData(file.TempDestination);
                List<TypeFileDto> types = data.ConvertToList<TypeFileDto>();

                _context = new ApplicationDbContext();

                foreach (var type in types)
                {
                    _context = new ApplicationDbContext();

                    if (!type.objValid())
                    {
                        var errors = type.objErrors();
                        type.Remarks = String.Join("; ", errors);
                        continue;
                    }

                    try
                    {
                        var curPizzaType = _context.PizzaTypes.FirstOrDefault(x => x.Code == type.PizzaTypeId);
                        if (curPizzaType != null)
                        {
                            type.Remarks = "Pizza type already exists";
                            continue;
                        }

                        var curCategory = _context.Categories.FirstOrDefault(x => x.Name == type.Category);
                        if (curCategory == null)
                        {
                            var newCategory = new Category()
                            {
                                Name = type.Category,
                            };

                            _context.Categories.Add(newCategory);
                            _context.SaveChanges();  // Save changes to get the new category ID

                            curCategory = newCategory; // Update curCategory to the newly added category
                        }

                        var newPizzaType = new PizzaType()
                        {
                            Code = type.PizzaTypeId,
                            Name = type.Name,
                            Ingredients = type.Ingredients,
                            CategoryId = curCategory.CategoryId,
                            CreatedDate = DateTime.Now,
                            CreatedById = file.UploadedById,
                            ModifiedDate = DateTime.Now,
                            ModifiedById = file.UploadedById,
                        };

                        _context.PizzaTypes.Add(newPizzaType);
                        _context.SaveChanges();

                        type.Remarks = "Record has been successfully uploaded.";
                    }
                    catch (Exception ex)
                    {
                        // Optionally log the exception: log.Error(ex);
                        type.Remarks = $"An error occurred on this record: {ex.Message}";
                    }
                }

                DataTable table = MyExtensions.ConvertToDataTable(types);
                Excel.ExportDataTable(table, "Type", file.FileName);

                file.Status = "Done";
                _context.SaveChanges();
            }
            catch (Exception)
            {
                file.Status = "Failed";
                file.Remarks = "There has been an error on file.";
                _context.SaveChanges();
            }
        }


        [AutomaticRetry(Attempts = 0)]
        public static void ToPizzas(int fileId)
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            var file = _context.Files.SingleOrDefault(x => x.FileId == fileId);
            if (file == null)
            {
                return;
            }

            var user = _context.Users.SingleOrDefault(u => u.Id == file.UploadedById);
            if (user == null)
            {
                file.Status = "Failed";
                file.Remarks = "This user is not allowed to access this resource.";
                _context.SaveChanges();

                return;
            }

            try
            {
                DataTable data = Excel.ImportExcelData(file.TempDestination);
                List<PizzaFileDto> pizzas = data.ConvertToList<PizzaFileDto>();

                _context = new ApplicationDbContext();

                foreach (var pizza in pizzas)
                {
                    _context = new ApplicationDbContext();

                    if (!pizza.objValid())
                    {
                        var errors = pizza.objErrors();
                        pizza.Remarks = String.Join("; ", errors);
                        continue;
                    }

                    try
                    {
                        var curPizza = _context.Pizzas.FirstOrDefault(x => x.Code == pizza.PizzaId);
                        if (curPizza != null)
                        {
                            pizza.Remarks = "Pizza already exists";
                            continue;
                        }

                        var curPizzaType = _context.PizzaTypes.FirstOrDefault(x => x.Code == pizza.PizzaTypeId);
                        if (curPizzaType == null)
                        {
                            pizza.Remarks = "Pizza type does exists";
                            continue;
                        }

                        decimal parsedPrice;
                        bool isPriceValid = decimal.TryParse(pizza.Price, out parsedPrice);

                        if (!isPriceValid)
                        {
                            // Handle the error if the price is not a valid decimal.
                            // For example, throw an exception or set a default value.
                            throw new ArgumentException("Invalid price value");
                        }

                        var newPizza = new Pizza()
                        {
                            Code = pizza.PizzaId,
                            Price = parsedPrice,
                            Size = pizza.Size,
                            TypeId = curPizzaType.TypeId,
                            CreatedDate = DateTime.Now,
                            CreatedById = file.UploadedById,
                            ModifiedDate = DateTime.Now,
                            ModifiedById = file.UploadedById,
                        };

                        _context.Pizzas.Add(newPizza);
                        _context.SaveChanges();

                        pizza.Remarks = "Record has been successfully uploaded.";
                    }
                    catch (Exception ex)
                    {
                        // Optionally log the exception: log.Error(ex);
                        pizza.Remarks = $"An error occurred on this record: {ex.Message}";
                    }
                }

                DataTable table = MyExtensions.ConvertToDataTable(pizzas);
                Excel.ExportDataTable(table, "Pizza", file.FileName);

                file.Status = "Done";
                _context.SaveChanges();
            }
            catch (Exception)
            {
                file.Status = "Failed";
                file.Remarks = "There has been an error on file.";
                _context.SaveChanges();
            }
        }


        [AutomaticRetry(Attempts = 0)]
        public static void ToOrders(int fileId)
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            var file = _context.Files.SingleOrDefault(x => x.FileId == fileId);
            if (file == null)
            {
                return;
            }

            var user = _context.Users.SingleOrDefault(u => u.Id == file.UploadedById);
            if (user == null)
            {
                file.Status = "Failed";
                file.Remarks = "This user is not allowed to access this resource.";
                _context.SaveChanges();

                return;
            }

            try
            {
                DataTable data = Excel.ImportExcelData(file.TempDestination);
                List<OrderFileDto> orders = data.ConvertToList<OrderFileDto>();

                _context = new ApplicationDbContext();

                foreach (var order in orders)
                {
                    if (!order.objValid())
                    {
                        var errors = order.objErrors();
                        order.Remarks = String.Join("; ", errors);
                        continue;
                    }

                    try
                    {
                        var curOrder = _context.Orders.FirstOrDefault(x => x.FileOrderId == order.OrderId);
                        if (curOrder != null)
                        {
                            order.Remarks = "Order already exists";
                            continue;
                        }

                        DateTime date;
                        TimeSpan time;

                        // Assuming the date is in "yyyy-MM-dd" format and time is in "HH:mm:ss" format
                        if (DateTime.TryParse(order.Date, out date) &&
                            TimeSpan.TryParse(order.Time, out time))
                        {
                            var createdDate = date.Date + time;

                            var newOrder = new Order()
                            {
                                FileOrderId = order.OrderId,
                                CreatedDate = createdDate,
                                CreatedById = file.UploadedById,
                                ModifiedDate = DateTime.Now,
                                ModifiedById = file.UploadedById,
                            };

                            _context.Orders.Add(newOrder);
                            _context.SaveChanges();
                        }
                        else
                        {
                            throw new ArgumentException("Invalid date or time format");
                        }

                        order.Remarks = "Record has been successfully uploaded.";
                    }
                    catch (Exception ex)
                    {
                        // Optionally log the exception: log.Error(ex);
                        order.Remarks = $"An error occurred on this record: {ex.Message}";
                    }
                }

                DataTable table = MyExtensions.ConvertToDataTable(orders);
                Excel.ExportDataTable(table, "Order", file.FileName);

                file.Status = "Done";
                _context.SaveChanges();
            }
            catch (Exception)
            {
                file.Status = "Failed";
                file.Remarks = "There has been an error on file.";
                _context.SaveChanges();
            }
        }



        [AutomaticRetry(Attempts = 0)]
        public static void ToOrderDetails(int fileId)
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            var file = _context.Files.SingleOrDefault(x => x.FileId == fileId);
            if (file == null)
            {
                return;
            }

            var user = _context.Users.SingleOrDefault(u => u.Id == file.UploadedById);
            if (user == null)
            {
                file.Status = "Failed";
                file.Remarks = "This user is not allowed to access this resource.";
                _context.SaveChanges();

                return;
            }

            try
            {
                DataTable data = Excel.ImportExcelData(file.TempDestination);
                List<OrderDetailFileDto> details = data.ConvertToList<OrderDetailFileDto>();

                _context = new ApplicationDbContext();

                foreach (var detail in details)
                {
                    if (!detail.objValid())
                    {
                        var errors = detail.objErrors();
                        detail.Remarks = String.Join("; ", errors);
                        continue;
                    }

                    try
                    {
                        var curDetail = _context.OrderDetails.FirstOrDefault(x => x.FileDetailId == detail.OrderDetailId);
                        if (curDetail != null)
                        {
                            detail.Remarks = "Detail already exists";
                            continue;
                        }

                        var curOrder = _context.Orders.FirstOrDefault(x => x.FileOrderId == detail.OrderId);
                        if (curOrder == null)
                        {
                            detail.Remarks = "Order does exists";
                            continue;
                        }

                        var curPizza = _context.Pizzas.FirstOrDefault(x => x.Code == detail.PizzaId);
                        if (curPizza == null)
                        {
                            detail.Remarks = "Pizza does exists";
                            continue;
                        }

                        var newDetail = new OrderDetail()
                        {
                            FileDetailId = detail.OrderDetailId,
                            OrderId = curOrder.OrderId,
                            PizzaId = curPizza.PizzaId,
                            Quantity = detail.Quantity,
                            CreatedDate = curOrder.CreatedDate,
                            CreatedById = file.UploadedById,
                            ModifiedDate = DateTime.Now,
                            ModifiedById = file.UploadedById,
                        };

                        _context.OrderDetails.Add(newDetail);
                        _context.SaveChanges();

                        detail.Remarks = "Record has been successfully uploaded.";
                    }
                    catch (Exception ex)
                    {
                        // Optionally log the exception: log.Error(ex);
                        detail.Remarks = $"An error occurred on this record: {ex.Message}";
                    }
                }

                DataTable table = MyExtensions.ConvertToDataTable(details);
                Excel.ExportDataTable(table, "Order", file.FileName);

                file.Status = "Done";
                _context.SaveChanges();
            }
            catch (Exception)
            {
                file.Status = "Failed";
                file.Remarks = "There has been an error on file.";
                _context.SaveChanges();
            }
        }

    }
}