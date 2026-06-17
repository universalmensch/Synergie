using System.Collections.Generic;
using System.Linq;
using Entity;
using Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controller.MapScene
{
    public class MapController : GameController
    {
        [SerializeField] private PlayerController player;
        private List<Task> _tasks;
        private ITaskService _taskService;
        private ISceneService _sceneService;

        public override void Clicked(Object obj)
        {
            // TODO: is there a different solution instead of making a db query with each mouse click.
            _tasks = _taskService.GetTasks();
            
            if (obj.GetType() != typeof(TileController)) return;

            if (player.isSelected && !_tasks.Any())
            {
                player.MoveTo(((TileController)obj).tileCenter);
            }
        }

        private void Start()
        {
            _taskService = ProjectInstaller.TaskService;
            _sceneService = ProjectInstaller.SceneService;
            _tasks = _taskService.GetTasks();

            if (!_tasks.Any()) return;
            
            _sceneService.LoadScene(ISceneService.SceneName.SelectionUI, LoadSceneMode.Additive);
        }

        public void GameStart()
        {
            _taskService = ProjectInstaller.TaskService;
            _taskService.AddTask(new  Task(TaskType.AddUnit, 1));
            _taskService.AddTask(new  Task(TaskType.AddUnit, 2));
            _taskService.AddTask(new  Task(TaskType.AddUnit, 3));
        }
    }
}