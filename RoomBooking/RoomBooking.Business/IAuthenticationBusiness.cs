using System.Threading.Tasks;
using RoomBooking.Data.Entities;

namespace RoomBooking.Business
{
    public interface IAuthenticationBusiness
    {
        Task<AppUser> TryLoginAsync(string email, string password);
    }
}