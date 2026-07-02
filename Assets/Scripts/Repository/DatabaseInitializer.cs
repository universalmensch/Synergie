using System.IO;
using Entity;
using SQLite;
using UnityEngine;

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

            var dbConnection = new SQLiteConnection(DbPath);

            dbConnection.BeginTransaction();
            dbConnection.DropTable<Unit>();
            dbConnection.DropTable<SynergieResource>();
            dbConnection.DropTable<SynergieTrigger>();
            dbConnection.DropTable<Synergie>();
            dbConnection.DropTable<Task>();

            dbConnection.CreateTable<Unit>();
            dbConnection.CreateTable<SynergieResource>();
            dbConnection.CreateTable<SynergieTrigger>();
            dbConnection.CreateTable<Synergie>();
            dbConnection.CreateTable<Task>();
            dbConnection.Commit();

            dbConnection.Close();
            dbConnection.Dispose();
        }
    }
}