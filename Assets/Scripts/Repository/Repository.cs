using System.Collections.Generic;
using Entity;
using SQLite;
using UnityEngine;

namespace Repository
{
    public class Repository : IRepository
    {
        private SQLiteConnection _dbConnection;

        private SQLiteConnection DbConnection
        {
            get
            {
                _dbConnection ??= new SQLiteConnection(DatabaseInitializer.DbPath);
                return _dbConnection;
            }
        }

        private void OnApplicationQuit()
        {
            _dbConnection?.Close();
            _dbConnection?.Dispose();
            _dbConnection = null;
        }
        
        public List<Unit> GetUnits()
        {
            DbConnection.Find<Unit>("1");
            DbConnection.Query<Unit>("select * from Unit unit where unit._isAlly == true");
            return new List<Unit>();
        }

        public void AddUnit(Unit unit)
        {
            
        }

        public SynergieEffect GetSynergieEffect()
        {
            var syn = DbConnection.Get<SynergieEffect>(2);
            Debug.Log(syn.Type);
            return syn;
        }

        public void AddSynergieEffect(SynergieEffect syn)
        {
            DbConnection.Insert(syn);
            Debug.Log("Rows: " + DbConnection.Table<SynergieEffect>().Count());
        }
    }
}