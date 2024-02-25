using AgroTech.DataAccessLayer.Models;

namespace AgroTech.DataAccessLayer.Contracts
{
    public interface IDataBaseDAL
    {
        Task<IEnumerable<DataBase>> GetAll();
    }
}
