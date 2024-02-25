using AgroTech.DataAccessLayer.Models;

namespace AgroTech.DataAccessLayer.Contracts
{
    public interface IAnimalDAL
    {
        Task<IEnumerable<Animal>> GetAnimalsByDataBase(string dataBase);
    }
}
