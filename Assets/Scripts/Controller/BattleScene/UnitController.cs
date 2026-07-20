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

        private int _currentAmor;
        private int _currentDamage;
        private int _currentHealth;

        private bool _isSelected;
        private Unit _unit;

        private IUnitService _unitService;

        public bool IsAlly => _unit.IsAlly;
        public int Mobility { get; private set; }

        public SynergieType SynergieType => _unit.SynergieType;

        private void Start()
        {
            _unitService = ProjectInstaller.UnitService;
        }

        private void Update()
        {
            if (_currentHealth <= 0)
            {
                if (_unit.IsAlly) _unitService.Remove(_unit);
                Destroy(gameObject);
            }

            if (_isSelected) GetComponent<Renderer>().material = selectedMaterial;
        }

        public void OnClick()
        {
        }

        public void InitUnit(Unit unit)
        {
            _unit = unit;
            _currentHealth = unit.Health;
            _currentDamage = unit.Damage;
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

            _currentHealth -= damage - _currentAmor;
        }

        public int GetDamage()
        {
            return _currentDamage;
        }

        public void BuffHealth(int health)
        {
            _currentHealth += health;
        }

        public void BuffMobility(int mobility)
        {
            Mobility += mobility;
        }

        public void BuffDamage(int damage)
        {
            _currentDamage += damage;
        }

        public void SelectUnit()
        {
            _isSelected = true;
        }

        public void DeselectUnit()
        {
            _isSelected = false;
        }
    }
}