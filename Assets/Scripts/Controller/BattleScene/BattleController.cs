using System.Collections.Generic;
using Entity;
using Service;
using UnityEngine;

namespace Controller.BattleScene
{
    public class BattleController : GameController
    {
        private IUnitService _unitService;
        private List<Unit> _units;
        [SerializeField] private GameObject unitPrefab;

        private void Start()
        {
            _unitService = ProjectInstaller.UnitService;
            
            _units = new List<Unit>();
            var allies = _unitService.GetAlliedUnits();
            var enemies = _unitService.GetEnemyUnits();
            _units.AddRange(allies);
            _units.AddRange(enemies);

            Debug.Log(allies.Count);
            Debug.Log(enemies.Count);
            Debug.Log(_units.Count);
            
            CreateUnits(_units);
        }

        private void CreateUnits(List<Unit> units)
        {
            foreach (var unit in units)
            {
                var unitObject = Instantiate(unitPrefab, unit.Position, Quaternion.identity, transform);
                unitObject.GetComponent<UnitController>().InitUnit(unit);
            }
        }

        private void Update()
        {
        }

        public override void Clicked(Object obj)
        {
            throw new System.NotImplementedException();
        }
    }
}