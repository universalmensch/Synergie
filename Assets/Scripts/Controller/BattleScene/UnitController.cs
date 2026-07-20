using Entity;
using Service;
using UnityEngine;

namespace Controller.BattleScene
{
    public class UnitController : MonoBehaviour, IClickable
    {
        [SerializeField] private Material allyMaterial;
        [SerializeField] private Material enemyMaterial;
        [SerializeField] private Material selectedMaterial;
        [SerializeField] private UnitUI ui;

        private int _currentAmor;

        private bool _isSelected;
        private Renderer _renderer;
        private Unit _unit;

        private IUnitService _unitService;

        public bool IsAlly => _unit.IsAlly;

        public int Mobility { get; private set; }

        public int Health { get; private set; }

        public int Damage { get; private set; }

        public bool IsDead { get; private set; }

        public SynergieType SynergieType => _unit.SynergieType;

        private void Start()
        {
            _unitService = ProjectInstaller.UnitService;
            _renderer = GetComponent<Renderer>();

            ui.UpdateUI(this);
        }

        private void Update()
        {
            _renderer.material = _isSelected ? selectedMaterial : allyMaterial;
        }

        public void OnClick()
        {
        }

        public void InitUnit(Unit unit)
        {
            _unit = unit;
            Health = unit.Health;
            Damage = unit.Damage;
            _currentAmor = unit.Armor;
            Mobility = unit.Mobility;

            GetComponent<Renderer>().material = unit.IsAlly ? allyMaterial : enemyMaterial;
        }

        public void GainMobility()
        {
            Mobility += Random.Range(0, 2) + _unit.SynergieType switch
            {
                SynergieType.Attacker => 2,
                SynergieType.Mobility => 3,
                _ => 1
            };
        }

        public void LoseMobility()
        {
            Mobility -= 15 + Random.Range(0, 5);
        }

        public string GetIdentifier()
        {
            return _unit.Name + " " + _unit.ID;
        }

        public void TakeDamage(int damage)
        {
            if (damage - _currentAmor < 0)
                return;

            Health -= damage - _currentAmor;

            if (Health > 0) return;

            if (_unit.IsAlly) _unitService.Remove(_unit);
            IsDead = true;
        }

        public int GetDamage()
        {
            return Damage;
        }

        public void BuffHealth(int health)
        {
            Health += health;
        }

        public void BuffMobility(int mobility)
        {
            Mobility += mobility;
        }

        public void BuffDamage(int damage)
        {
            Damage += damage;
        }

        public void SelectUnit()
        {
            _isSelected = true;
        }

        public void DeselectUnit()
        {
            _isSelected = false;
        }

        public void UpdateUI()
        {
            ui.UpdateUI(this);
        }
    }
}