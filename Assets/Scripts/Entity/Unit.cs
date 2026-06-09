using UnityEngine;

namespace Entity
{
    public class Unit
    {
        private bool _isAlly;

        public bool IsAlly
        {
            get => _isAlly;
        }
        
        private Vector3 _position;

        public Vector3 Position
        {
            get => _position;
        }

        private UnitType _unitType;

        public UnitType UnitType
        {
            get => _unitType;
        }

        private int _health;

        public int Health
        {
            get => _health;
        }

        private int _damage;

        public int Damage
        {
            get => _damage;
        }

        private int _armor;

        public int Armor
        {
            get => _armor;
        }

        public Unit(Vector3 position, bool isAlly, UnitType unitType, int health, int damage, int armor)
        {
            _position = position;
            _isAlly = isAlly;
            _unitType = unitType;
            _health = health;
            _damage = damage;
            _armor = armor;
        }
    }
}