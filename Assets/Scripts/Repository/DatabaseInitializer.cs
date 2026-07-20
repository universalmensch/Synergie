using System.Collections.Generic;
using System.IO;
using Entity;
using SQLite;
using UnityEngine;
using Resources = Entity.Resources;

namespace Repository
{
    public static class DatabaseInitializer
    {
        public static string DbPath =>
            Path.Combine(Application.persistentDataPath, "synergie.db");

        public static void Initialize()
        {
            // TODO: master table for static data
            // TODO: 1 table for each safe game
            // TODO: Drop table only in specific cases

            var connection = new SQLiteConnection(DbPath);

            connection.BeginTransaction();
            connection.DropTable<Unit>();
            connection.DropTable<SynergieResource>();
            connection.DropTable<SynergieTrigger>();
            connection.DropTable<Synergie>();
            connection.DropTable<SynergieEffect>();
            connection.DropTable<Resources>();
            connection.DropTable<Task>();

            connection.CreateTable<Unit>();
            connection.CreateTable<SynergieResource>();
            connection.CreateTable<SynergieTrigger>();
            connection.CreateTable<Synergie>();
            connection.CreateTable<SynergieEffect>();
            connection.CreateTable<Task>();
            connection.CreateTable<Resources>();
            connection.Commit();

            AddSynergieEffects(connection);

            connection.Insert(new Resources());

            connection.Close();
            connection.Dispose();
        }

        private static void AddSynergieEffects(SQLiteConnection connection)
        {
            // TODO add data directly to static database

            // Level 1 effects
            connection.Insert(new SynergieEffect(new Dictionary<SynergieType, int>
            {
                { SynergieType.Attacker, 2 }
            }, Effect.Attacker, 1));

            connection.Insert(new SynergieEffect(new Dictionary<SynergieType, int>
            {
                { SynergieType.Defender, 2 }
            }, Effect.Defender, 1));

            connection.Insert(new SynergieEffect(new Dictionary<SynergieType, int>
            {
                { SynergieType.Mobility, 2 }
            }, Effect.Runner, 1));

            connection.Insert(new SynergieEffect(new Dictionary<SynergieType, int>
            {
                { SynergieType.Attacker, 1 },
                { SynergieType.Defender, 1 }
            }, Effect.StrongDefender, 1));

            connection.Insert(new SynergieEffect(new Dictionary<SynergieType, int>
            {
                { SynergieType.Attacker, 1 },
                { SynergieType.Mobility, 1 }
            }, Effect.DoubleAttacker, 1));

            connection.Insert(new SynergieEffect(new Dictionary<SynergieType, int>
            {
                { SynergieType.Mobility, 1 },
                { SynergieType.Defender, 1 }
            }, Effect.CounterAttacker, 1));

            // Level 2 effects
            connection.Insert(new SynergieEffect(new Dictionary<SynergieType, int>
            {
                { SynergieType.Attacker, 4 }
            }, Effect.Attacker, 2));

            connection.Insert(new SynergieEffect(new Dictionary<SynergieType, int>
            {
                { SynergieType.Defender, 4 }
            }, Effect.Defender, 2));

            connection.Insert(new SynergieEffect(new Dictionary<SynergieType, int>
            {
                { SynergieType.Mobility, 4 }
            }, Effect.Runner, 2));

            connection.Insert(new SynergieEffect(new Dictionary<SynergieType, int>
            {
                { SynergieType.Attacker, 2 },
                { SynergieType.Defender, 2 }
            }, Effect.StrongDefender, 2));

            connection.Insert(new SynergieEffect(new Dictionary<SynergieType, int>
            {
                { SynergieType.Attacker, 2 },
                { SynergieType.Mobility, 2 }
            }, Effect.DoubleAttacker, 2));

            connection.Insert(new SynergieEffect(new Dictionary<SynergieType, int>
            {
                { SynergieType.Mobility, 2 },
                { SynergieType.Defender, 2 }
            }, Effect.CounterAttacker, 2));
        }
    }
}