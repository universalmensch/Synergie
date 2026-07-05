using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<SynergieResource> GetSynergieResources()
        {
            return _repository.GetSynergieResources();
        }

        public List<SynergieTrigger> GetSynergieTriggers()
        {
            return _repository.GetSynergieTriggers();
        }

        public void AddSynergieResource(SynergieResource synergieResource)
        {
            _repository.AddSynergieResource(synergieResource);
        }

        public void UpdateSynergieResource(SynergieResource synergieResource)
        {
            _repository.UpdateSynergieResource(synergieResource);
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

        public List<SynergieEffect> GetActiveSynergieEffects(List<Unit> units, List<SynergieResource> resources)
        {
            var typeCount = Enum.GetValues(typeof(SynergieType)).Cast<SynergieType>().ToDictionary(type => type,
                type => units.Count(unit => type == unit.Type) +
                        resources.Count(resource => type == resource.Type));

            return _repository.GetSynergieEffects().Where(effect =>
                    effect.GetRequirements().All(requirement => typeCount[requirement.Key] >= requirement.Value))
                .GroupBy(synergieEffect => synergieEffect.Effect)
                .Select(group => group.OrderByDescending(synergieEffect => synergieEffect.Level).First())
                .ToList();
        }
    }
}