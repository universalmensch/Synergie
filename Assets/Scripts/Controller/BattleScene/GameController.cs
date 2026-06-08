using System.Collections.Generic;
using Entity;
using Service;
using UnityEngine;

namespace Controller.BattleScene
{
    public class GameController : MonoBehaviour
    {
        private IEnemyService _enemyService;
        private IUnitService _unitService;

        private List<Enemy> _enemies;
        private List<Unit> _units;

        private void Start()
        {
            _enemyService = ProjectInstaller.EnemyService;
            _unitService = ProjectInstaller.UnitService;
        }

        private void Update()
        {
        }
    }
}