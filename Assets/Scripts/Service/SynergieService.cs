using Repository;

namespace Service
{
    public class SynergieService : ISynergieService
    {
        private IRepository _repository;
        
        public  SynergieService(IRepository repository)
        {
            _repository = repository;
        }
    }
}