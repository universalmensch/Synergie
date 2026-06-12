using System.Collections.Generic;
using Entity;

namespace Repository
{
    public class Repository : IRepository
    {
        private SQLiteConnection _dbConnection;

        private SQLiteConnection DbConnection
        {
            get
            {
                _dbConnection ??= new SQLiteConnection($"Data Source={DatabaseInitializer.DbPath}", true);
                return _dbConnection;
            }
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
    }
}