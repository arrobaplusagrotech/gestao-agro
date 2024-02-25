using AgroTech.DataAccessLayer.Contracts;
using AgroTech.DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace AgroTech.DataAccessLayer
{
    public class AnimalDAL : IAnimalDAL
    {
        private readonly string _connectionString;
        private readonly IMemoryCache _memoryCache;

        public AnimalDAL(IConfiguration configuration, IMemoryCache memoryCache)
        {
            _connectionString = configuration.GetConnectionString("SqlServer") ?? "";
            _memoryCache = memoryCache;

        }

        public async Task<IEnumerable<Animal>> GetAnimalsByDataBase(string dataBase)
        {
            var chacheData = await _memoryCache.GetOrCreateAsync($"animals-{dataBase}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                entry.SetPriority(CacheItemPriority.High);

                var animals = new List<Animal>();

                try
                {
                    using SqlConnection connection = new(_connectionString);

                    await connection.OpenAsync();

                    string sql = $"SELECT [IDFAZENDA] FARMID, [PESOINICIAL] STARTWEIGHT, [PESO] CURRENTWEIGHT, [SEXO] SEX, " +
                                 $"CASE WHEN [STATUSANIMAL] = 1 THEN 1 ELSE 0 END STATUS, [IDPASTO] PASTUREID, [IDFASE] PHASEID, " +
                                 $"[DATAULTIMAMUDANCAFASE] ULTIMAMUDANCAFASE " +
                                 $"FROM [{dataBase}].[DBO].[TBLANIMAL]";

                    using SqlCommand sqlCommand = new(sql, connection);

                    using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        animals.Add(new Animal(reader.GetInt32(0), reader.GetDecimal(1), reader.GetDecimal(2),
                            reader.GetString(3), Convert.ToBoolean(reader.GetInt32(4)), reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5),
                            reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6), reader.IsDBNull(7) ? null : reader.GetDateTime(7)));
                    }

                    return animals;
                }
                catch (SqlException ex)
                {
                    throw new Exception($"An error occurred when obtaining the data in the database {dataBase}. " +
                        $"SQL Error: {ex.Number}, Message: {ex.Message}", ex);
                }
            });

            return chacheData ?? new List<Animal>();
        }
    }
}
