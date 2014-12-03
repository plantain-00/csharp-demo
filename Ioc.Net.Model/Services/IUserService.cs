using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;

using Ioc.Net.Model.Models;

namespace Ioc.Net.Model.Services
{
    [ServiceContract]
    public interface IUserService : IBaseService
    {
        [OperationContract]
        User[] GetUsers();
        IQueryable<User> GetUsers<T>(out int count, int skipped = 0, bool isAsc = true, Expression<Func<User, T>> order = null, params Expression<Func<User, bool>>[] conditions);
        [OperationContract]
        void Export(User[] users);
        [OperationContract]
        List<User> Import(string fileName);
        [OperationContract]
        void AddUser(User user);
    }
}