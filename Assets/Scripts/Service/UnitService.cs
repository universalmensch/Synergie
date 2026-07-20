using System.Collections.Generic;
using Entity;
using Repository;
using UnityEngine;
using Random = UnityEngine.Random;

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
            var enemy1 = new Unit(new Vector3(-12 + Random.Range(-2, 2), 1, 8 + Random.Range(-2, 2)), false,
                SynergieType.Defender, 40, 7, 3, 5, "Manfred");
            var enemy2 = new Unit(new Vector3(-6 + Random.Range(-2, 2), 1, 9 + Random.Range(-2, 2)), false,
                SynergieType.Defender, 40, 6, 3, 5, "Olaf");
            var enemy3 = new Unit(new Vector3(-8 + Random.Range(-2, 2), 1, 7 + Random.Range(-2, 2)), false,
                SynergieType.Mobility, 40, 6, 3, 10, "Günther");
            var enemy4 = new Unit(new Vector3(10 + Random.Range(-2, 2), 1, 6 + Random.Range(-2, 2)), false,
                SynergieType.Attacker, 30, 7, 2, 8, "Sebastian");
            var enemy5 = new Unit(new Vector3(2 + Random.Range(-2, 2), 1, 8 + Random.Range(-2, 2)), false,
                SynergieType.Attacker, 30, 7, 1, 8, "Rudolf");
            var enemy6 = new Unit(new Vector3(6 + Random.Range(-2, 2), 1, 7 + Random.Range(-2, 2)), false,
                SynergieType.Mobility, 30, 7, 0, 10, "Albert");

            return new List<Unit> { enemy1, enemy2, enemy3, enemy4, enemy5, enemy6 };
        }

        public void Add(Unit unit)
        {
            _repository.AddUnit(unit);
        }

        public void Remove(Unit unit)
        {
            _repository.DeleteUnit(unit);
        }
    }
}