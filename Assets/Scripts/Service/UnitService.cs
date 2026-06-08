using Repository;

namespace Service
{
    public class UnitService :  IUnitService
    {
        private IRepository _repository;
        
        public UnitService(IRepository repository)
        {
            _repository = repository;
        }
    }
}