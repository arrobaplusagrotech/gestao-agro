namespace AgroTech.DataAccessLayer.Contracts
{
    public interface IQuery<T> where T : class
    {
        Task<T> Get(int id);

        Task<IEnumerable<T>> GetAll();
    }
}
