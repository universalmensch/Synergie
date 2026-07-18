using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Entity;
using Service;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Controller.BattleScene
{
    public class BattleController : GameController
    {
        [SerializeField] private GameObject unitPrefab;

        [SerializeField] private TextMeshProUGUI firstUnitText;
        [SerializeField] private TextMeshProUGUI secondUnitText;
        [SerializeField] private TextMeshProUGUI thirdUnitText;
        [SerializeField] private TextMeshProUGUI forthUnitText;
        [SerializeField] private TextMeshProUGUI fifthUnitText;

        private List<Unit> _allies;
        private List<UnitController> _allyControllers;
        private List<Unit> _enemies;
        private UnitController _selectedUnit;
        private List<Synergie> _synergies;
        private ISynergieService _synergieService;
        private List<UnitController> _units;
        private IUnitService _unitService;

        private void Start()
        {
            _unitService = ProjectInstaller.UnitService;
            _synergieService = ProjectInstaller.SynergieService;

            _synergies = _synergieService.GetSynergies();

            _allies = _unitService.GetAlliedUnits();
            _enemies = _unitService.GetEnemyUnits();

            var units = new List<Unit>();
            units.AddRange(_allies);
            units.AddRange(_enemies);

            CreateUnits(units);
            HandleNextTurn();
        }

        private void Update()
        {
        }

        private void CreateUnits(List<Unit> units)
        {
            _units = new List<UnitController>();
            _allyControllers = new List<UnitController>();

            foreach (var unit in units)
            {
                var unitObject = Instantiate(unitPrefab, unit.Position, Quaternion.identity, transform);
                unitObject.GetComponent<UnitController>().InitUnit(unit);
                _units.Add(unitObject.GetComponent<UnitController>());
                if (unit.IsAlly)
                    _allyControllers.Add(unitObject.GetComponent<UnitController>());
            }
        }

        public override void Clicked(Object obj)
        {
            Debug.Log(_selectedUnit);
            if (!_selectedUnit)
                return;

            if (obj.GetType() != typeof(UnitController)) return;

            if (((UnitController)obj).IsAlly)
            {
                foreach (var synergy in _synergies.Where(synergy =>
                             synergy.Triggers.Find(trigger => trigger.SynergieType == SynergieType.Mobility) !=
                             null))
                    TriggerSynergie(synergy, (UnitController)obj);
            }
            else
            {
                foreach (var synergy in _synergies.Where(synergy =>
                             synergy.Triggers.Find(trigger => trigger.SynergieType == SynergieType.Attacker) !=
                             null))
                    TriggerSynergie(synergy, _selectedUnit);

                ((UnitController)obj).TakeDamage(_selectedUnit.GetDamage());
            }

            HandleNextTurn();
        }

        private void HandleNextTurn()
        {
            foreach (var unitController in _units) unitController.GainMobility();

            var sortedByMobility = _units
                .OrderByDescending(unitController => unitController.currentMobility)
                .ThenBy(unitController => unitController.GetIdentifier()).ToList();

            ShowNextUnitTurns(sortedByMobility);

            var firstUnit = sortedByMobility.First();
            firstUnit.LoseMobility();

            if (!firstUnit.IsAlly)
            {
                Debug.Log("enemy" + firstUnit.currentMobility);

                var ally = _allyControllers[Random.Range(0, _allyControllers.Count)];

                var effects = new List<SynergieEffect>();
                foreach (var synergie in _synergies.Where(synergy =>
                             synergy.Triggers.Find(trigger => trigger.SynergieType == SynergieType.Defender) !=
                             null))
                {
                    TriggerSynergie(synergie, ally);
                    effects.AddRange(_synergieService.GetActiveSynergieEffects(_allies, synergie.Resources));
                }

                ally.TakeDamage(firstUnit.GetDamage());
                HandleNextTurn();
            }
            else
            {
                _selectedUnit = firstUnit;
                Debug.Log("ally " + _selectedUnit.currentMobility);
            }
        }

        private void ShowNextUnitTurns(List<UnitController> units)
        {
            firstUnitText.text = units[0].GetIdentifier();
            secondUnitText.text = units[1].GetIdentifier();
            thirdUnitText.text = units[2].GetIdentifier();
            forthUnitText.text = units[3].GetIdentifier();
            fifthUnitText.text = units[4].GetIdentifier();
        }

        private static void TriggerSynergie(Synergie synergie, UnitController ally)
        {
            foreach (var synergieResource in synergie.Resources)
                switch (synergieResource.SynergieType)
                {
                    case SynergieType.Defender:
                        ally.BuffHealth(synergieResource.Value);

                        break;
                    case SynergieType.Attacker:
                        ally.BuffDamage(synergieResource.Value);
                        break;
                    case SynergieType.Mobility:
                        ally.BuffMobility(synergieResource.Value);
                        break;
                    default: throw new InvalidEnumArgumentException();
                }
        }
    }
}