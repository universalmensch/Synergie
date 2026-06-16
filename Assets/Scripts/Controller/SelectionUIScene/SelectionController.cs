using Entity;
using Service;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.SelectionUIScene
{
    public class SelectionController : MonoBehaviour
    {
        private ISceneService _sceneService;
        private IUnitService _unitService;

        [SerializeField] private ButtonController button0;
        [SerializeField] private ButtonController button1;
        [SerializeField] private ButtonController button2;
        [SerializeField] private ButtonController button3;
        [SerializeField] private ButtonController button4;
        [SerializeField] private ButtonController button5;
        
        private void Start()
        {
            _sceneService = ProjectInstaller.SceneService;
            _unitService = ProjectInstaller.UnitService;
            
            button0.GetComponent<Button>().onClick.AddListener(ButtonClick);
            button1.GetComponent<Button>().onClick.AddListener(ButtonClick);
            button2.GetComponent<Button>().onClick.AddListener(ButtonClick);
            button3.GetComponent<Button>().onClick.AddListener(ButtonClick);
            button4.GetComponent<Button>().onClick.AddListener(ButtonClick);
            button5.GetComponent<Button>().onClick.AddListener(ButtonClick);
            
            StartUnitSelection();
        }

        private void ButtonClick()
        {
            button0.GetComponent<Button>().enabled = false;
            button1.GetComponent<Button>().enabled = false;
            button2.GetComponent<Button>().enabled = false;
            button3.GetComponent<Button>().enabled = false;
            button4.GetComponent<Button>().enabled = false;
            button5.GetComponent<Button>().enabled = false;
            
            if (_unitService.GetAlliedUnitsCount() < 5)
            {
                StartUnitSelection();
            }
            else
            {
                _sceneService.UnloadScene(ISceneService.SceneName.SelectionUI);
            }
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

        private void StartSynergieSelection()
        {
            
        }
    }
}