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

        public Unit GetUnit(int id)
        {
            return DbConnection.Find<Unit>(id);
        }

        public List<Unit> GetAlliedUnits()
        {
            var units = DbConnection.Query<Unit>("select * from Unit unit where unit.IsAlly == true");
            Debug.Log(units.Count);
            return units;
        }

        public int GetAlliedUnitsCount()
        {
            return DbConnection.Table<Unit>().Count(unit => unit.IsAlly == true);
        }

        public List<Task> GetTasks()
        {
            return DbConnection.Query<Task>("select * from Task");
        }

        public void AddUnit(Unit unit)
        {
            DbConnection.Insert(unit);
        }

        public void AddTask(Task task)
        {
            DbConnection.Insert(task);
            Debug.Log("Task added");
        }

        public void DeleteTask(int taskId)
        {
            DbConnection.Delete<Task>(taskId);
            Debug.Log("Task deleted");
        }

        public void UpdateUnit(Unit unit)
        {
            DbConnection.Update(unit);
        }

        public void DeleteUnit(Unit unit)
        {
            DbConnection.Delete(unit);
        }

        public SynergieEffect GetSynergieEffect()
        {
            return DbConnection.Get<SynergieEffect>(2);
        }

        public void AddSynergieEffect(SynergieEffect syn)
        {
            DbConnection.Insert(syn);
        }
    }
}