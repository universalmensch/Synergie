using System.Collections.Generic;
using Entity;
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

        public List<Synergie> GetSynergies()
        {
            return new List<Synergie>();
        }
        
        public List<SynergieEffect> GetSynergieEffects()
        {
            return new List<SynergieEffect>();
        }
    }
}