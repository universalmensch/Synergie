using System.Collections.Generic;
using Entity;
using Repository;
using UnityEngine;

namespace Service
{
    public class UnitService : IUnitService
    {
        private readonly IRepository _repository;

        public UnitService(IRepository repository)
        {
            _repository = repository;
        }

        public List<Unit> GetAlliedUnits()
        {
            return _repository.GetAlliedUnits();
        }

        public int GetAlliedUnitsCount()
        {
            return _repository.GetAlliedUnitsCount();
        }

        public List<Unit> GetEnemyUnits()
        {
            var enemy1 = new Unit(new Vector3(-6, 1, 5), false, SynergieType.Defender, 40, 2, 10, 5, "Manfred");
            var enemy2 = new Unit(new Vector3(-3, 1, 7), false, SynergieType.Defender, 40, 2, 10, 5, "Olaf");
            var enemy3 = new Unit(new Vector3(-4, 1, 5), false, SynergieType.Mobility, 40, 2, 10, 10, "Günther");
            var enemy4 = new Unit(new Vector3(5, 1, 6), false, SynergieType.Attacker, 30, 4, 5, 8, "Sebastian");
            var enemy5 = new Unit(new Vector3(1, 1, 5), false, SynergieType.Attacker, 30, 4, 5, 8, "Rudolf");
            var enemy6 = new Unit(new Vector3(3, 1, 6), false, SynergieType.Mobility, 30, 4, 5, 10, "Albert");

            return new List<Unit> { enemy1, enemy2, enemy3, enemy4, enemy5, enemy6 };
        }

        public void Add(Unit unit)
        {
            _repository.AddUnit(unit);
        }
    }
}