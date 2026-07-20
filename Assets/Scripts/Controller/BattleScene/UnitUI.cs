using TMPro;
using UnityEngine;

namespace Controller.BattleScene
{
    public class UnitUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI amor;
        [SerializeField] private TextMeshProUGUI damage;
        [SerializeField] private TextMeshProUGUI health;
        [SerializeField] private TextMeshProUGUI identifier;

        public void UpdateUI(UnitController unit)
        {
            identifier.text = unit.GetIdentifier();
            damage.text = unit.GetDamage().ToString();
            health.text = unit.Health.ToString();
            amor.text = unit.Damage.ToString();
        }
    }
}