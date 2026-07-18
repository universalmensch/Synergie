using Entity;
using UnityEngine;

namespace Controller.BattleScene
{
    public class UnitController : MonoBehaviour, IClickable
    {
        [SerializeField] private Material allyMaterial;
        [SerializeField] private Material enemyMaterial;
        [SerializeField] private Material selectedMaterial;

        public bool isSelected;
        public int currentMobility;
        private int _currentAmor;
        private int _currentDamage;
        private int _currentHealth;

        private Unit _unit;

        public bool IsAlly => _unit.IsAlly;
        public SynergieType SynergieType => _unit.SynergieType;

        private void Update()
        {
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
                if (_unit.IsAlly)
                {
                }
            }

            if (isSelected) GetComponent<Renderer>().material = selectedMaterial;
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
            currentMobility = unit.Mobility;

            GetComponent<Renderer>().material = unit.IsAlly ? allyMaterial : enemyMaterial;
        }

        public void GainMobility()
        {
            currentMobility += Random.Range(0, 2) + _unit.SynergieType switch
            {
                SynergieType.Attacker => 2,
                SynergieType.Mobility => 3,
                _ => 1
            };
        }

        public void LoseMobility()
        {
            currentMobility -= 15 + Random.Range(0, 5);
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
            currentMobility += mobility;
        }

        public void BuffDamage(int damage)
        {
            _currentDamage += damage;
        }
    }
}