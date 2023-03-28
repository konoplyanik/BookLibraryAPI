using DomainLayer.Models;
using RepositoryLayer;
using ServiceLayer.Service.Contract;
using System.Collections.Generic;

namespace ServiceLayer.Service.Implementation
{
    public class UserService : Repository<ApplicationUser>
    {
        public UserService(AppDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return Set.AsEnumerable();
        }

        //public ApplicationUser GetUserById(long id)
        //{
        //    return Set.AsEnumerable().Where(u => u.Id == id).FirstOrDefault();
        //}

        //public string AddUser(ApplicationUser user)
        //{
        //    try
        //    {
        //        Set.Add(user);

        //        return "Success";
        //    }
        //    catch (Exception e)
        //    {
        //        return e.Message;
        //    }
        //}

        //public string UpdateUser(ApplicationUser user)
        //{
        //    try
        //    {
        //        var userValue = Set.Find(user.UserId);

        //        if (userValue != null)
        //        {
        //            userValue.UserName = user.UserName;
        //            userValue.UserPhone = user.UserPhone;
        //            userValue.UserEmail = user.UserEmail;
        //            Set.Update(userValue);
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

        //public string RemoveUser(long id)
        //{
        //    try
        //    {
        //        var user = Set.Where(u => u.UserId == id).FirstOrDefault();
        //        Set.Remove(user);

        //        return "Successfully Removed";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
    }
}
