using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entity;
using Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.SynergieUIScene
{
    public class SynergieController : MonoBehaviour
    {
        [SerializeField] private GameObject componentPrefab;
        [SerializeField] private GameObject removePrefab;
        [SerializeField] private GameObject effectPrefab;
        [SerializeField] private GameObject resourceParent;
        [SerializeField] private GameObject effectParent;
        [SerializeField] private GameObject triggerParent;

        [SerializeField] private TextMeshProUGUI resourceHeader;
        [SerializeField] private TextMeshProUGUI resourceValue;
        [SerializeField] private TextMeshProUGUI resourceDescription;
        [SerializeField] private TextMeshProUGUI resourceLevel;
        [SerializeField] private Button upgradeButton;

        [SerializeField] private TMP_Dropdown resourceDropdown;
        private Synergie _selectedSynergie;
        private List<SynergieResource> _synergieResources;
        private List<Synergie> _synergies;

        private ISynergieService _synergieService;
        private IUnitService _unitService;

        private void Start()
        {
            _synergieService = ProjectInstaller.SynergieService;
            _unitService = ProjectInstaller.UnitService;
            ShowDefaultResourceInfo();

            _synergies = _synergieService.GetSynergies();
            _synergieResources = _synergieService.GetSynergieResources();

            SetResourceDropdown();
            ShowSynergies();
            ShowSynergieEffects();
        }

        private void SetResourceDropdown()
        {
            resourceDropdown.onValueChanged.RemoveAllListeners();
            resourceDropdown.ClearOptions();
            resourceDropdown.captionText.text = "add effect";

            if (_synergieResources.Count == 0)
            {
                resourceDropdown.interactable = false;
                return;
            }

            resourceDropdown.interactable = true;

            var options = new List<TMP_Dropdown.OptionData> { new("add effect") };
            options.AddRange(_synergieResources.Select(effect => new TMP_Dropdown.OptionData(effect.Header)));

            resourceDropdown.AddOptions(options);
            resourceDropdown.SetValueWithoutNotify(0);

            resourceDropdown.onValueChanged.AddListener(AddResource);
        }

        private void AddResource(int index)
        {
            // ignore placeholder
            if (0 == index)
                return;

            index--;

            var resource = _synergieResources[index];
            _selectedSynergie.Resources.Add(resource);
            _synergieService.UpdateSynergie(_selectedSynergie);

            resource.SynergieId = _selectedSynergie.Id;
            _synergieService.UpdateSynergieResource(resource);

            _synergieResources.RemoveAt(index);

            SetResourceDropdown();
            ShowSynergie(_selectedSynergie.Resources.Count - 1);
            ShowSynergieEffects();
        }

        private void ShowSynergies()
        {
            _selectedSynergie = _synergies[0];
            ShowSynergie();
        }

        private void ShowSynergie(int selectedResource = 0)
        {
            ClearSynergie();

            for (var index = 0; index < _selectedSynergie.Resources.Count; index++)
                ShowResource(_selectedSynergie.Resources[index], index, selectedResource);

            for (var index = 0; index < _selectedSynergie.Triggers.Count; index++)
                ShowTrigger(_selectedSynergie.Triggers[index], index);
        }

        private void ClearSynergie()
        {
            ShowDefaultResourceInfo();
            foreach (Transform child in resourceParent.transform) Destroy(child.gameObject);
        }

        private void ShowResource(SynergieResource resource, int index, int selectedResource)
        {
            var resourceButton = Instantiate(componentPrefab, resourceParent.transform);
            resourceButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-40, -50 * (index + 2));
            resourceButton.GetComponent<ButtonUIElement>().label.text = resource.Header;
            resourceButton.GetComponent<ButtonUIElement>().button.onClick
                .AddListener(() => ShowResourceInfo(resource, index));
            if (index == selectedResource)
                StartCoroutine(SelectButtonNextFrame(resourceButton.GetComponent<ButtonUIElement>().button,
                    selectedResource));


            var removeButton = Instantiate(removePrefab, resourceParent.transform);
            removeButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(40, -50 * (index + 2));
            removeButton.GetComponent<ButtonUIElement>().button.onClick.AddListener(() => RemoveResource(resource));
        }

        private IEnumerator SelectButtonNextFrame(Button button, int selectedResource)
        {
            yield return null;

            button.Select();
            ShowResourceInfo(_selectedSynergie.Resources[selectedResource], selectedResource);
        }

        private void RemoveResource(SynergieResource resource)
        {
            _selectedSynergie.Resources.Remove(resource);
            _synergieService.UpdateSynergie(_selectedSynergie);

            resource.SynergieId = null;
            _synergieService.UpdateSynergieResource(resource);

            _synergieResources.Add(resource);

            SetResourceDropdown();
            ShowSynergie();
            ShowSynergieEffects();
        }

        private void ShowDefaultResourceInfo()
        {
            resourceHeader.text = string.Empty;
            resourceValue.text = string.Empty;
            resourceDescription.text = string.Empty;
            resourceLevel.text = string.Empty;
            upgradeButton.gameObject.SetActive(false);
        }

        private void UpgradeResource(SynergieResource resource, int selectedResourceIndex)
        {
            resource.Upgrade();
            _synergieService.UpdateSynergieResource(resource);
            ShowSynergie(selectedResourceIndex);
        }

        private void ShowResourceInfo(SynergieResource resource, int selectedResourceIndex)
        {
            resourceHeader.text = resource.Header;
            resourceValue.text = "value: " + resource.Value;
            resourceDescription.text = resource.Description;
            resourceLevel.text = "level: " + resource.Level;

            upgradeButton.onClick.RemoveAllListeners();
            upgradeButton.onClick.AddListener(() => UpgradeResource(resource, selectedResourceIndex));
            upgradeButton.GetComponent<ButtonUIElement>().label.text = "upgrade (" + resource.UpgradeCost + ")";
            upgradeButton.gameObject.SetActive(true);
        }

        private void ShowTrigger(SynergieTrigger trigger, int index)
        {
            var effectButton = Instantiate(componentPrefab, triggerParent.transform);
            effectButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-40, -100 * (index + 1));
            effectButton.GetComponent<ButtonUIElement>().label.text = trigger.Header;

            var removeButton = Instantiate(removePrefab, triggerParent.transform);
            removeButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(40, -100 * (index + 1));
        }

        private void ShowTriggerInfo(SynergieTrigger trigger)
        {
        }

        private void ShowSynergieEffects()
        {
            foreach (Transform child in effectParent.transform) Destroy(child.gameObject);

            var effects =
                _synergieService.GetActiveSynergieEffects(_unitService.GetAlliedUnits(), _selectedSynergie.Resources);

            for (var index = 0; index < effects.Count; index++) ShowSynergieEffect(effects[index], index);
        }

        private void ShowSynergieEffect(SynergieEffect effect, int index)
        {
            var effectDescription = Instantiate(effectPrefab, effectParent.transform);
            effectDescription.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -60 * (index + 1));
            effectDescription.GetComponent<HeaderDescriptionUIElement>().header.text =
                effect.Effect + " level: " + effect.Level;
            effectDescription.GetComponent<HeaderDescriptionUIElement>().description.text =
                EffectDescription.Description[effect.Effect];
        }
    }
}