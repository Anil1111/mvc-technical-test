using System.Data.Common;

namespace RoomBooking.Data
{
    public class CustomSqlExecution : ICustomSqlExecution
    {
        private readonly IAppDbContext _appDbContext;

        public CustomSqlExecution(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public DbConnection SqlConnection()
        {
            return _appDbContext.Database.Connection;
        }
    }
}