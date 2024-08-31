using HotelManagement.Application.Contracts.GenericRepository;
using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Contracts.Repository
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        
    }
}