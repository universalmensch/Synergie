using System.Collections.Generic;

namespace Service
{
    public class EnemyService : IEnemyService
    {
        public List<string> GetEnemies()
        {
            return new List<string>{"bla"};
        }
    }
}