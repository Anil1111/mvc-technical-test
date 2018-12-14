using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RoomBooking.Data;
using RoomBooking.Data.Entities;

namespace RoomBooking.Business
{
    public class AuthenticationBusiness : IAuthenticationBusiness
    {
        private readonly IAppDbContext _appDbContext;
        private readonly UserManager<AppUser, string> _userManager;

        public AuthenticationBusiness(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _userManager = new UserManager<AppUser, string>(new UserStore<AppUser>(_appDbContext as AppDbContext));
        }

        public async Task<AppUser> TryLoginAsync(string email, string password)
        {
            var user = await _userManager.FindAsync(email, password);
            return user;
        }
    }
}
