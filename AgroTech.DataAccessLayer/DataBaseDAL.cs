using AgroTech.DataAccessLayer.Contracts;
using AgroTech.DataAccessLayer.Enums;
using AgroTech.DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace AgroTech.DataAccessLayer
{
    public class DataBaseDAL : IDataBaseDAL
    {
        private readonly string _connectionString;
        private readonly IMemoryCache _memoryCache;

        public DataBaseDAL(IConfiguration configuration, IMemoryCache memoryCache)
        {
            _connectionString = configuration.GetConnectionString("SqlServer") ?? "";
            _memoryCache = memoryCache;

        }

        public async Task<IEnumerable<DataBase>> GetAll()
        {
            var databases = new List<DataBase>();

            var chacheData = await _memoryCache.GetOrCreateAsync("databaseskey", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                entry.SetPriority(CacheItemPriority.High);

                try
                {
                    using SqlConnection connection = new(_connectionString);

                    await connection.OpenAsync();

                    string sql = $"SELECT [DATABASE_ID], [NAME] FROM SYS.DATABASES " +
                    $"WHERE [DATABASE_ID] NOT IN ({(int)NotDataBaseEnum.MASTER},{(int)NotDataBaseEnum.TEMPDB},{(int)NotDataBaseEnum.MODEL}," +
                    $"{(int)NotDataBaseEnum.MSDB},{(int)NotDataBaseEnum.DBSISTEMA},{(int)NotDataBaseEnum.DBBASE}, {(int)NotDataBaseEnum.DBFAZARROBAPLUS}," +
                    $"{(int)NotDataBaseEnum.DBFAZAADM},{(int)NotDataBaseEnum.ARROBAPLUS},{(int)NotDataBaseEnum.DBBACKUPCLIENTE}, {(int)NotDataBaseEnum.DBBACKUP}, {(int)NotDataBaseEnum.AGROTECH})";

                    SqlCommand sqlCommand = new(sql, connection);

                    using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);

                        databases.Add(new DataBase(id, name));
                    }

                    return databases;
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
            });

            return chacheData ?? new List<DataBase>();
        }
    }
}
