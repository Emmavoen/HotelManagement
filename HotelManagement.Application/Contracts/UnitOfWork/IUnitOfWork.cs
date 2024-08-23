using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.Repository;

namespace HotelManagement.Application.Contracts.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        //IPostRepository PostRepository{ get; }
        //IBlogRepository BlogRepository{ get; }
        IUserRepository UserRepository{ get; }

        Task<int> Save();
    }
}