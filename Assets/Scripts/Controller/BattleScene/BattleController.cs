using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Entity;
using Service;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

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
        private List<UnitController> _enemyControllers;
        private ISceneService _sceneService;
        private UnitController _selectedUnit;
        private List<Synergie> _synergies;
        private ISynergieService _synergieService;
        private List<UnitController> _units;
        private IUnitService _unitService;

        private void Start()
        {
            _unitService = ProjectInstaller.UnitService;
            _synergieService = ProjectInstaller.SynergieService;
            _sceneService = ProjectInstaller.SceneService;

            _synergies = _synergieService.GetSynergies();

            _allies = _unitService.GetAlliedUnits();
            _enemies = _unitService.GetEnemyUnits();

            var units = new List<Unit>();
            units.AddRange(_allies);
            units.AddRange(_enemies);

            CreateUnits(units);
            HandleNextTurn();
        }

        private void CreateUnits(List<Unit> units)
        {
            _units = new List<UnitController>();
            _allyControllers = new List<UnitController>();
            _enemyControllers = new List<UnitController>();

            foreach (var unit in units)
            {
                var unitObject = Instantiate(unitPrefab, unit.Position, Quaternion.identity, transform);
                unitObject.GetComponent<UnitController>().InitUnit(unit);
                _units.Add(unitObject.GetComponent<UnitController>());
                if (unit.IsAlly)
                    _allyControllers.Add(unitObject.GetComponent<UnitController>());
                else
                    _enemyControllers.Add(unitObject.GetComponent<UnitController>());
            }
        }

        private IEnumerator EndBattle()
        {
            yield return new WaitForSeconds(0.5f);

            _sceneService.LoadScene(ISceneService.SceneName.Map);
            _sceneService.LoadScene(ISceneService.SceneName.MapUI, LoadSceneMode.Additive);
        }

        public override void Clicked(Object obj)
        {
            if (!_selectedUnit)
                return;

            _selectedUnit.DeselectUnit();

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

                Fight(_selectedUnit, (UnitController)obj, false, GetSynergieEffects());
            }

            HandleNextTurn();
        }

        private void HandleNextTurn()
        {
            if (_enemyControllers.Count == 0 || _allyControllers.Count == 0)
            {
                StartCoroutine(EndBattle());
                return;
            }

            foreach (var unitController in _units) unitController.GainMobility();

            var sortedByMobility = _units
                .OrderByDescending(unitController => unitController.Mobility)
                .ThenBy(unitController => unitController.GetIdentifier()).ToList();

            ShowNextUnitTurns(sortedByMobility);

            var firstUnit = sortedByMobility.First();
            firstUnit.LoseMobility();

            if (!firstUnit.IsAlly)
            {
                var ally = _allyControllers[Random.Range(0, _allyControllers.Count)];


                foreach (var synergie in _synergies.Where(synergy =>
                             synergy.Triggers.Find(trigger => trigger.SynergieType == SynergieType.Defender) !=
                             null))
                    TriggerSynergie(synergie, ally);

                Fight(ally, firstUnit, false, GetSynergieEffects());
                StartCoroutine(WaitForNextTurn());
            }
            else
            {
                _selectedUnit = firstUnit;
                firstUnit.SelectUnit();
            }
        }

        private List<SynergieEffect> GetSynergieEffects()
        {
            var effects = new List<SynergieEffect>();
            foreach (var synergy in _synergies)
                effects.AddRange(_synergieService.GetActiveSynergieEffects(_allies, synergy.Resources));
            return effects;
        }

        private IEnumerator WaitForNextTurn()
        {
            yield return new WaitForSeconds(0.5f);
            HandleNextTurn();
        }

        private void Fight(UnitController ally, UnitController enemy, bool allyIsAttacker,
            List<SynergieEffect> effects)
        {
            if (allyIsAttacker)
            {
                var damage = ally.GetDamage();

                if (effects.Find(effect => Effect.Attacker == effect.Effect && 1 == effect.Level) != null)
                    damage = Mathf.CeilToInt(damage * 1.2f);

                if (effects.Find(effect => Effect.Attacker == effect.Effect && 2 == effect.Level) != null)
                    damage = Mathf.CeilToInt(damage * 1.5f);

                enemy.TakeDamage(damage);

                if (effects.Find(effect => Effect.DoubleAttacker == effect.Effect && 1 == effect.Level) != null)
                    enemy.TakeDamage(damage);

                if (effects.Find(effect => Effect.DoubleAttacker == effect.Effect && 2 == effect.Level) != null)
                {
                    enemy.TakeDamage(damage);
                    enemy.TakeDamage(damage);
                }
            }
            else
            {
                var damage = enemy.GetDamage();

                if (effects.Find(effect => Effect.Defender == effect.Effect && 1 == effect.Level) != null)
                    damage = Mathf.CeilToInt(damage * 0.8f);

                if (effects.Find(effect => Effect.Defender == effect.Effect && 2 == effect.Level) != null)
                    damage = Mathf.CeilToInt(damage * 0.6f);

                if (effects.Find(effect => Effect.StrongDefender == effect.Effect && 1 == effect.Level) != null)
                    damage -= ally.GetDamage();

                if (effects.Find(effect => Effect.StrongDefender == effect.Effect && 2 == effect.Level) != null)
                    damage -= ally.GetDamage();

                ally.TakeDamage(damage);

                if (effects.Find(effect => Effect.CounterAttacker == effect.Effect && 1 == effect.Level) != null)
                    enemy.TakeDamage(ally.GetDamage());

                if (effects.Find(effect => Effect.CounterAttacker == effect.Effect && 2 == effect.Level) != null)
                    enemy.TakeDamage(ally.GetDamage());
            }

            if (effects.Find(effect => Effect.Runner == effect.Effect && 1 == effect.Level) != null)
                ally.GainMobility();

            if (effects.Find(effect => Effect.Runner == effect.Effect && 2 == effect.Level) != null)
            {
                ally.GainMobility();
                ally.GainMobility();
            }

            ally.UpdateUI();
            enemy.UpdateUI();

            if (ally.IsDead)
            {
                _units.Remove(ally);
                _allyControllers.Remove(ally);
                _selectedUnit = null;
                Destroy(ally.gameObject);
            }

            if (enemy.IsDead)
            {
                _units.Remove(enemy);
                _enemyControllers.Remove(enemy);
                Destroy(enemy.gameObject);
            }
        }

        private void ShowNextUnitTurns(List<UnitController> units)
        {
            firstUnitText.text = units.Count > 0 ? units[0].GetIdentifier() : "";
            secondUnitText.text = units.Count > 1 ? units[1].GetIdentifier() : "";
            thirdUnitText.text = units.Count > 2 ? units[2].GetIdentifier() : "";
            forthUnitText.text = units.Count > 3 ? units[3].GetIdentifier() : "";
            fifthUnitText.text = units.Count > 4 ? units[4].GetIdentifier() : "";
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

            ally.UpdateUI();
        }
    }
}