using Repository;
using Service;
using UnityEngine;

public class ProjectInstaller : MonoBehaviour
{
    private static readonly IRepository Repository = new Repository.Repository();
    public static readonly IUnitService UnitService = new UnitService(Repository);
    public static readonly IEnemyService EnemyService = new EnemyService();
    public static readonly ISceneService SceneService = new SceneService();
}