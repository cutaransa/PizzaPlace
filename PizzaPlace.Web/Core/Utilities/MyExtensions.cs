using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PizzaPlace.Web.Core.Utilities
{
    public static class MyExtensions
    {
        public static List<T> ConvertToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);

                            var name = (prop.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute) as DisplayAttribute;
                            //Setting column names as Property names
                            if (name != null)
                            {
                                propertyInfo.SetValue(obj, Convert.ChangeType(row[name.Name], propertyInfo.PropertyType), null);
                            }
                            else
                            {
                                propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable ConvertToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                var name = (prop.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute) as DisplayAttribute;
                //Setting column names as Property names
                if (name != null)
                {
                    dataTable.Columns.Add(name.Name);
                }
                else
                {
                    dataTable.Columns.Add(prop.Name);
                }
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}