using Entity;
using UnityEngine;

namespace Controller.BattleScene
{
    public class UnitController : MonoBehaviour, IClickable
    {
        [SerializeField] private Material allyMaterial;
        [SerializeField] private Material enemyMaterial;
        
        private Unit _unit;
        private int _currentHealth;
        private int _currentDamage;
        private int _currentAmor;

        public void InitUnit(Unit unit)
        {
            _unit = unit;
            _currentHealth = unit.Health;
            _currentDamage = unit.Damage;
            _currentAmor = unit.Armor;

            GetComponent<Renderer>().material = unit.IsAlly ? allyMaterial : enemyMaterial;
        }

        private void Update()
        {
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        public void OnClick()
        {
            Debug.Log("unit clicked");
        }
    }
}