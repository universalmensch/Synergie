using Entity;
using Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.SelectionUIScene
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TextMeshProUGUI heading;

        private ISelection _selection;
        private ISynergieService _synergieService;
        private IUnitService _unitService;

        private void Start()
        {
            _unitService = ProjectInstaller.UnitService;
            _synergieService = ProjectInstaller.SynergieService;

            button.enabled = false;
            button.onClick.AddListener(Select);
        }

        private void Select()
        {
            switch (_selection)
            {
                case Unit unit:
                    _unitService.Add(unit);
                    break;
                case SynergieResource resource:
                    _synergieService.AddSynergieResource(resource);
                    break;
                case SynergieTrigger trigger:
                    _synergieService.AddSynergieTrigger(trigger);
                    break;
            }
        }

        public void SetSelection(ISelection selection)
        {
            _selection = selection;
            heading.text = selection.GetSelectionHeadingText();
            text.text = selection.GetSelectionText();

            button.enabled = true;
        }
    }
}