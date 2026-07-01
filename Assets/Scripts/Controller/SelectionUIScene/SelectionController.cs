using System.Collections.Generic;
using System.Linq;
using Entity;
using Service;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.SelectionUIScene
{
    public class SelectionController : MonoBehaviour
    {
        [SerializeField] private ButtonController button0;
        [SerializeField] private ButtonController button1;
        [SerializeField] private ButtonController button2;
        [SerializeField] private ButtonController button3;
        [SerializeField] private ButtonController button4;
        [SerializeField] private ButtonController button5;
        private ISceneService _sceneService;

        private List<Task> _tasks;
        private ITaskService _taskService;

        private void Start()
        {
            _sceneService = ProjectInstaller.SceneService;
            _taskService = ProjectInstaller.TaskService;

            button0.GetComponent<Button>().onClick.AddListener(ButtonClick);
            button1.GetComponent<Button>().onClick.AddListener(ButtonClick);
            button2.GetComponent<Button>().onClick.AddListener(ButtonClick);
            button3.GetComponent<Button>().onClick.AddListener(ButtonClick);
            button4.GetComponent<Button>().onClick.AddListener(ButtonClick);
            button5.GetComponent<Button>().onClick.AddListener(ButtonClick);

            _tasks = _taskService.GetTasks();
            HandleTasks();
        }

        private void HandleTasks()
        {
            var currentTask = _tasks.First();
            _tasks.Remove(currentTask);
            _taskService.DeleteTask(currentTask.ID);

            switch (currentTask.TaskType)
            {
                case TaskType.AddUnit: StartUnitSelection(); break;
                case TaskType.AddSynergieEffect: StartSynergieResourceSelection(); break;
                case TaskType.AddSynergieTrigger: StartSynergieTriggerSelection(); break;
            }
        }

        private void ButtonClick()
        {
            button0.GetComponent<Button>().enabled = false;
            button1.GetComponent<Button>().enabled = false;
            button2.GetComponent<Button>().enabled = false;
            button3.GetComponent<Button>().enabled = false;
            button4.GetComponent<Button>().enabled = false;
            button5.GetComponent<Button>().enabled = false;

            if (_tasks.Any())
                HandleTasks();
            else
                _sceneService.UnloadScene(ISceneService.SceneName.SelectionUI);
        }

        private void StartUnitSelection()
        {
            var ally0 = new Unit(new Vector3(-4, 1, -4), true, SynergieType.Attacker, 10, 5, 5);
            button0.SetSelection(ally0);

            var ally1 = new Unit(new Vector3(-2, 1, -3), true, SynergieType.Defender, 20, 3, 10);
            button1.SetSelection(ally1);

            var ally2 = new Unit(new Vector3(3, 1, -2), true, SynergieType.Defender, 30, 3, 10);
            button2.SetSelection(ally2);

            var ally3 = new Unit(new Vector3(0, 1, -3), true, SynergieType.Attacker, 40, 5, 5);
            button3.SetSelection(ally3);

            var ally4 = new Unit(new Vector3(-4, 1, -4), true, SynergieType.Attacker, 50, 5, 5);
            button4.SetSelection(ally4);

            var ally5 = new Unit(new Vector3(0, 1, -3), true, SynergieType.Attacker, 60, 5, 5);
            button5.SetSelection(ally5);
        }

        private void StartSynergieTriggerSelection()
        {
            var trigger0 = new SynergieTrigger(SynergieType.Attacker, "triggers the synergie on an allied attack",
                "OnAttack");
            button0.SetSelection(trigger0);

            var trigger1 = new SynergieTrigger(SynergieType.Attacker, "triggers the synergie on an allied attack",
                "OnAttack");
            button1.SetSelection(trigger1);

            var trigger2 = new SynergieTrigger(SynergieType.Defender, "triggers the synergie on an allied defense",
                "OnDefense");
            button2.SetSelection(trigger2);

            var trigger3 = new SynergieTrigger(SynergieType.Defender, "triggers the synergie on an allied defense",
                "OnDefense");
            button3.SetSelection(trigger3);

            var trigger4 = new SynergieTrigger(SynergieType.Mobility,
                "triggers the synergie when an ally buffs another ally",
                "OnAttack");
            button4.SetSelection(trigger4);

            var trigger5 = new SynergieTrigger(SynergieType.Mobility,
                "triggers the synergie when an ally buffs another ally",
                "OnAttack");
            button5.SetSelection(trigger5);
        }

        private void StartSynergieResourceSelection()
        {
            var resource0 = new SynergieEffect(SynergieType.Attacker, 1,
                "grants bonus attack when synergie is triggered", "Attacking orb");
            button0.SetSelection(resource0);

            var resource1 = new SynergieEffect(SynergieType.Attacker, 1,
                "grants bonus attack when synergie is triggered", "Attacking orb");
            button1.SetSelection(resource1);

            var resource2 = new SynergieEffect(SynergieType.Defender, 1,
                "grants bonus health when synergie is triggered", "Defending orb");
            button2.SetSelection(resource2);

            var resource3 = new SynergieEffect(SynergieType.Defender, 1,
                "grants bonus health when synergie is triggered", "Defending orb");
            button3.SetSelection(resource3);

            var resource4 = new SynergieEffect(SynergieType.Mobility, 1,
                "grants bonus mobility when synergie is triggered", "Mobility orb");
            button4.SetSelection(resource4);

            var resource5 = new SynergieEffect(SynergieType.Mobility, 1,
                "grants bonus mobility when synergie is triggered", "Mobility orb");
            button5.SetSelection(resource5);
        }
    }
}