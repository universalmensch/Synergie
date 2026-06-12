using System.IO;
using Entity;
using UnityEngine;

namespace Repository
{
    public static class DatabaseInitializer
    {
         public static string DbPath =>
            Path.Combine(Application.persistentDataPath, "synergie.db");

        public static void Initialize()
        {
            var conn = new SQLiteConnection(DbPath, true);
            
            conn.BeginTransaction();
            conn.CreateTable<SynergieEffect>();
            conn.CreateTable<Unit>();
            conn.Commit();
            conn.Close();
        }
    }
}