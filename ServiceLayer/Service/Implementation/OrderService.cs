using DomainLayer.Models;
using RepositoryLayer;

namespace ServiceLayer.Service.Implementation
{
    public class OrderService : Repository<Order>
    {
        public OrderService(AppDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return Set.AsEnumerable();
        }

        public Order GetOrderById(long id)
        {
            return Set.AsEnumerable().Where(o => o.OrderId == id).FirstOrDefault();
        }

        public string AddOrder(Order order)
        {
            try
            {
                Set.Add(order);

                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //public string UpdateOrder(Order order)
        //{
        //    try
        //    {
        //        var orderValue = Set.Find(order.BookId);

        //        if (orderValue != null)
        //        {
        //            orderValue.Book = order.Book;
        //            Set.Update(orderValue);
        //            return "Successfully Updated";
        //        }
        //        else
        //        {
        //            return "No Record(s) Found";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //}

        //public string RemoveOrder(long id)
        //{
        //    try
        //    {
        //        var order = Set.Where(o => o.OrderId == id).FirstOrDefault();
        //        Set.Remove(order);

        //        return "Successfully Removed";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
    }
}
