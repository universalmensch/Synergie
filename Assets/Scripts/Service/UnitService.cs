using System.Collections.Generic;
using Entity;
using Repository;
using UnityEngine;

namespace Service
{
    public class UnitService : IUnitService
    {
        private IRepository _repository;

        public UnitService(IRepository repository)
        {
            _repository = repository;
        }

        public List<Unit> GetUnits()
        {
            var ally1 = new Unit(new Vector3(-2, 1, -3), true, UnitType.Defender, 50, 3, 10);
            var ally2 = new Unit(new Vector3(3, 1, -2), true, UnitType.Defender, 50, 3, 10);
            var ally3 = new Unit(new Vector3(0, 1, -3), true, UnitType.Attacker, 30, 5, 5);
            var ally4 = new Unit(new Vector3(-4, 1, -4), true, UnitType.Attacker, 30, 5, 5);

            var enemy1 = new Unit(new Vector3(-6, 1, 5), false, UnitType.Defender, 40, 2, 10);
            var enemy2 = new Unit(new Vector3(-3, 1, 7), false, UnitType.Defender, 40, 2, 10);
            var enemy3 = new Unit(new Vector3(-4, 1, 5), false, UnitType.Defender, 40, 2, 10);
            var enemy4 = new Unit(new Vector3(5, 1, 6), false, UnitType.Attacker, 30, 4, 5);
            var enemy5 = new Unit(new Vector3(1, 1, 5), false, UnitType.Attacker, 30, 4, 5);
            var enemy6 = new Unit(new Vector3(3, 1, 6), false, UnitType.Attacker, 30, 4, 5);

            return new List<Unit> { ally1, ally2, ally3, ally4, enemy1, enemy2, enemy3, enemy4, enemy5, enemy6 };
        }
    }
}