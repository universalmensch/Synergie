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
            _units = _unitService.GetUnits();
            
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