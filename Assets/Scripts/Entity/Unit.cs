using SQLite;
using UnityEngine;

namespace Entity
{
    public class Unit : ISelection
    {
        public Unit(Vector3 position, bool isAlly, SynergieType type, int health, int damage, int armor)
        {
            XCoordinate = position.x;
            YCoordinate = position.z;
            IsAlly = isAlly;
            Type = type;
            Health = health;
            Damage = damage;
            Armor = armor;
        }

        public Unit()
        {
            // only for ORM, use parameterized constructor instead
        }

        [PrimaryKey] [AutoIncrement] public int ID { get; set; }

        public bool IsAlly { get; set; }

        public Vector3 Position => new(XCoordinate, 1, YCoordinate);

        public float XCoordinate { get; set; }

        public float YCoordinate { get; set; }

        public SynergieType Type { get; set; }

        public int Health { get; set; }

        public int Damage { get; set; }

        public int Armor { get; set; }

        public string GetSelectionText()
        {
            return "Health: " + Health + " \n Damage: " + Damage + " \n Armor: " + Armor;
        }

        public string GetSelectionHeadingText()
        {
            return Type.ToString();
        }
    }
}