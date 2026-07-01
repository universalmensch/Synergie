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
        private ISceneService _sceneService;
        private ISynergieService _synergieService;
        private List<Task> _tasks;
        private ITaskService _taskService;

        private void Start()
        {
            _taskService = ProjectInstaller.TaskService;
            _sceneService = ProjectInstaller.SceneService;
            _synergieService = ProjectInstaller.SynergieService;
            _tasks = _taskService.GetTasks();

            if (!_tasks.Any()) return;

            _sceneService.LoadScene(ISceneService.SceneName.SelectionUI, LoadSceneMode.Additive);
            _sceneService.LoadScene(ISceneService.SceneName.MapUI, LoadSceneMode.Additive);
        }

        public override void Clicked(Object obj)
        {
            // TODO: is there a different solution instead of making a db query with each mouse click.
            _tasks = _taskService.GetTasks();

            if (obj.GetType() != typeof(TileController)) return;

            if (player.isSelected && !_tasks.Any()) player.MoveTo(((TileController)obj).tileCenter);
        }

        public void GameStart()
        {
            _synergieService = ProjectInstaller.SynergieService;
            _taskService = ProjectInstaller.TaskService;

            _synergieService.AddSynergie(new Synergie());
            _taskService.AddTask(new Task(TaskType.AddUnit, 1));
            _taskService.AddTask(new Task(TaskType.AddUnit, 2));
            _taskService.AddTask(new Task(TaskType.AddUnit, 3));
            _taskService.AddTask(new Task(TaskType.AddSynergieTrigger, 4));
            _taskService.AddTask(new Task(TaskType.AddSynergieEffect, 5));
        }
    }
}