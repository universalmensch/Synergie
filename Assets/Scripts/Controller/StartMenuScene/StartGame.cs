using Entity;
using Service;
using UnityEngine;

namespace Controller.StartMenuScene
{
    public class StartGame : MonoBehaviour
    {
        private ISceneService _sceneService;
        private ISynergieService _synergieService;
        private ITaskService _taskService;

        private void Start()
        {
            _synergieService = ProjectInstaller.SynergieService;
            _taskService = ProjectInstaller.TaskService;
            _sceneService = ProjectInstaller.SceneService;

            _synergieService.AddSynergie(new Synergie());
            _taskService.AddTask(new Task(TaskType.AddUnit));
            _taskService.AddTask(new Task(TaskType.AddUnit));
            _taskService.AddTask(new Task(TaskType.AddUnit));
            _taskService.AddTask(new Task(TaskType.AddSynergieTrigger));
            _taskService.AddTask(new Task(TaskType.AddSynergieEffect));
            _taskService.AddTask(new Task(TaskType.AddSynergieEffect));

            _sceneService.LoadScene(ISceneService.SceneName.Map);
        }
    }
}