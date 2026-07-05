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
        }

        public void DeleteTask(int taskId)
        {
            DbConnection.Delete<Task>(taskId);
        }

        public void UpdateUnit(Unit unit)
        {
            DbConnection.Update(unit);
        }

        public void DeleteUnit(Unit unit)
        {
            DbConnection.Delete(unit);
        }

        public void AddSynergieTrigger(SynergieTrigger synergieTrigger)
        {
            DbConnection.Insert(synergieTrigger);
        }

        public List<SynergieResource> GetSynergieResources()
        {
            var synergieEffects = DbConnection.Query<SynergieResource>(
                "select * from SynergieResource");
            return synergieEffects.FindAll(effect => !effect.IsSelected);
        }

        public List<SynergieTrigger> GetSynergieTriggers()
        {
            var synergieTriggers = DbConnection.Query<SynergieTrigger>(
                "select * from SynergieTrigger");
            return synergieTriggers.FindAll(effect => !effect.IsSelected);
        }

        public List<Synergie> GetSynergies()
        {
            var synergies = DbConnection.Query<Synergie>("select * from Synergie");

            foreach (var synergie in synergies)
            {
                synergie.Resources =
                    DbConnection.Query<SynergieResource>("select * from SynergieResource where SynergieId = ?",
                        synergie.Id);
                synergie.Triggers =
                    DbConnection.Query<SynergieTrigger>("select * from SynergieTrigger where SynergieId = ?",
                        synergie.Id);
            }

            return synergies;
        }

        public void UpdateSynergie(Synergie synergie)
        {
            DbConnection.Update(synergie);
        }

        public void AddSynergie(Synergie synergie)
        {
            DbConnection.Insert(synergie);
        }

        public void UpdateSynergieResource(SynergieResource synergieResource)
        {
            DbConnection.Update(synergieResource);
        }

        public void UpdateSynergieTrigger(SynergieTrigger synergieTrigger)
        {
            DbConnection.Update(synergieTrigger);
        }

        public void AddSynergieResource(SynergieResource synergieResource)
        {
            DbConnection.Insert(synergieResource);
        }

        public void Dispose()
        {
            _dbConnection?.Close();
            _dbConnection?.Dispose();
            _dbConnection = null;
        }

        public Unit GetUnit(int id)
        {
            return DbConnection.Find<Unit>(id);
        }

        public List<SynergieEffect> GetSynergieEffects()
        {
            return DbConnection.Query<SynergieEffect>("select * from SynergieEffect");
        }
    }
}