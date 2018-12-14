using System.Data.Common;

namespace RoomBooking.Data
{
    public interface ICustomSqlExecution
    {
        DbConnection SqlConnection();
    }
}