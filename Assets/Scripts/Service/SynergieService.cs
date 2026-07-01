using System.Collections.Generic;
using Entity;
using Repository;

namespace Service
{
    public class SynergieService : ISynergieService
    {
        private readonly IRepository _repository;

        public SynergieService(IRepository repository)
        {
            _repository = repository;
        }

        public List<Synergie> GetSynergies()
        {
            return _repository.GetSynergies();
        }

        public List<SynergieEffect> GetSynergieEffects()
        {
            return _repository.GetSynergieEffects();
        }

        public List<SynergieTrigger> GetSynergieTriggers()
        {
            return _repository.GetSynergieTriggers();
        }

        public void AddSynergieEffect(SynergieEffect synergieEffect)
        {
            _repository.AddSynergieEffect(synergieEffect);
        }

        public void UpdateSynergieEffect(SynergieEffect synergieEffect)
        {
            _repository.UpdateSynergieEffect(synergieEffect);
        }

        public void AddSynergieTrigger(SynergieTrigger synergieTrigger)
        {
            _repository.AddSynergieTrigger(synergieTrigger);
        }

        public void UpdateSynergieTrigger(SynergieTrigger synergieTrigger)
        {
            _repository.UpdateSynergieTrigger(synergieTrigger);
        }

        public void UpdateSynergie(Synergie synergie)
        {
            _repository.UpdateSynergie(synergie);
        }

        public void AddSynergie(Synergie synergie)
        {
            _repository.AddSynergie(synergie);
        }
    }
}