using Controller.MapScene;
using Repository;
using Service;
using UnityEngine;

public class ProjectInstaller : MonoBehaviour
{
    private static readonly IRepository Repository = new Repository.Repository();
    public static readonly IUnitService UnitService = new UnitService(Repository);
    public static readonly ISynergieService SynergieService = new SynergieService(Repository);
    public static readonly ITaskService TaskService = new TaskService(Repository);
    public static readonly ISceneService SceneService = new SceneService();
    [SerializeField] private MapController mapController;

    private void Awake()
    {
        DatabaseInitializer.Initialize();
        mapController.GameStart();
    }

    private void OnApplicationQuit()
    {
        Repository.Dispose();
    }
}