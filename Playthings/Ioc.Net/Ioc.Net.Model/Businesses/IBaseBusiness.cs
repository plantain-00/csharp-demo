using System;

using Ioc.Net.Model.Services;

namespace Ioc.Net.Model.Businesses
{
    public interface IBaseBusiness : IDisposable
    {
        IUserService UserService { get; }
        void Close();
    }
}