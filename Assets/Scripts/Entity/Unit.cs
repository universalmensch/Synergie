using SQLite;
using UnityEngine;

namespace Entity
{
    public class Unit : ISelection
    {
        public Unit(Vector3 position, bool isAlly, SynergieType synergieType, int health, int damage, int armor,
            int mobility, string name)
        {
            XCoordinate = position.x;
            YCoordinate = position.z;
            IsAlly = isAlly;
            SynergieType = synergieType;
            Health = health;
            Damage = damage;
            Armor = armor;
            Mobility = mobility;
            Name = name;
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

        public SynergieType SynergieType { get; set; }

        public int Health { get; set; }

        public int Damage { get; set; }

        public int Armor { get; set; }

        public int Mobility { get; set; }

        public string Name { get; set; }

        public string GetSelectionText()
        {
            return "Health: " + Health + " \n Damage: " + Damage + " \n Armor: " + Armor + " \n Mobility: " + Mobility;
        }

        public string GetSelectionHeadingText()
        {
            return SynergieType.ToString();
        }
    }
}