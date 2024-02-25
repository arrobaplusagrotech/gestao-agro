using AgroTech.BusinessLogicLayer.Contracts;
using AgroTech.DataAccessLayer.Contracts;
using AgroTech.DataAccessLayer.Models;

namespace AgroTech.BusinessLogicLayer
{
    public class ControlPanelBLL : IControlPanelBLL
    {
        private readonly IDataBaseDAL _databaseRepository;
        private readonly IAnimalDAL _animalRepository;
        public ControlPanelBLL(IDataBaseDAL databaseRepository, IAnimalDAL animalRepository)
        {
            _databaseRepository = databaseRepository;
            _animalRepository = animalRepository;
        }

        public async Task<GlobalFarmDataReporter> Get()
            {
            List<Task<IEnumerable<Animal>>> animalsTasks = new();

            var dataBases = await _databaseRepository.GetAll();

            var animalsTaks = dataBases
                .Select(clientDb => _animalRepository.GetAnimalsByDataBase(clientDb.Name))
                .ToList();

            await Task.WhenAll(animalsTasks);

            var globalFarmDataReporter = new GlobalFarmDataReporter();

            for (int i = 0; i < dataBases.ToList().Count; i++)
            {
                try
                {
                    var clientDb = dataBases.ToList()[i];
                    var animals = animalsTaks[i].Result;

                    clientDb.Animals = animals.ToList();
                    globalFarmDataReporter.Clients.Add(clientDb);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return globalFarmDataReporter;
        }
    }
}
