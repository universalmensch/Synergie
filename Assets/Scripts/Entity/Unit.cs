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

        private SynergieType _synergieType;

        public SynergieType SynergieType
        {
            get => _synergieType;
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

        public Unit(Vector3 position, bool isAlly, SynergieType synergieType, int health, int damage, int armor)
        {
            _position = position;
            _isAlly = isAlly;
            _synergieType = synergieType;
            _health = health;
            _damage = damage;
            _armor = armor;
        }
        
        public Unit(){}
    }
}