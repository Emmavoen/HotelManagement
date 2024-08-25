using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.GenericRepository;
using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Contracts.Repository
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        
    }
}