using Entity;
using Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.SelectionUIScene
{
    public class ButtonController : MonoBehaviour
    {
        private IUnitService _unitService;
        
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TextMeshProUGUI heading;
        
        private ISelection _selection;

        private void Start()
        {
            _unitService = ProjectInstaller.UnitService;
            
            button.enabled = false;
            button.onClick.AddListener(Select);
        }

        private void Select()
        {
            if (_selection is Unit unit)
            {
                _unitService.Add(unit);
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