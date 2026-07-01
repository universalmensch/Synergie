using System;
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
        [SerializeField] private GameObject effectParent;
        [SerializeField] private GameObject triggerParent;

        [SerializeField] private TextMeshProUGUI effectHeader;
        [SerializeField] private TextMeshProUGUI effectValue;
        [SerializeField] private TextMeshProUGUI effectDescription;
        [SerializeField] private TextMeshProUGUI effectLevel;
        [SerializeField] private Button upgradeButton;
        
        [SerializeField] private TMP_Dropdown effectDropdown;


        private ISynergieService _synergieService;
        private List<Synergie> _synergies;
        private List<SynergieEffect> _synergieEffects;
        private Synergie _selectedSynergie;

        private void Start()
        {
            _synergieService = ProjectInstaller.SynergieService;
            ShowDefaultEffectInfo();
            
            _synergies = _synergieService.GetSynergies();
            _synergieEffects = _synergieService.GetSynergieEffects();
            
            _synergieEffects.Add(new SynergieEffect(SynergieType.Mobility, 2, "mobility go brrr", "mobility orb"));
            _synergieEffects.Add(new SynergieEffect(SynergieType.Attacker, 1, "attacker go brrr", "attaker orb"));
            _synergieEffects.Add(new SynergieEffect(SynergieType.Defender, 3, "defender go brrr", "defender orb"));
            
            SetEffectDropdown();
            
            var synergie = new Synergie();
            synergie.Effects.Add(new SynergieEffect(SynergieType.Attacker, 2, "attacking go brr", "attacking orb"));
            synergie.Effects.Add(new SynergieEffect(SynergieType.Defender, 1, "defending go brr", "defending orb"));
            synergie.Triggers.Add(new SynergieTrigger(SynergieType.Attacker, "when unit attacks", "attacker"));
            _synergies.Add(synergie);

            if (0 == _synergies.Count)
            {
                ShowEmptyUI();
            }
            else
            {
                ShowSynergies();
            }
        }

        private void SetEffectDropdown()
        {
            effectDropdown.onValueChanged.RemoveAllListeners();
            effectDropdown.ClearOptions();
            effectDropdown.captionText.text = "add effect";
            
            if (_synergieEffects.Count == 0)
            {
                effectDropdown.interactable = false;
                return;
            }

            effectDropdown.interactable = true;
            
            var options = new List<TMP_Dropdown.OptionData> { new ("add effect") };
            options.AddRange(_synergieEffects.Select(effect => new TMP_Dropdown.OptionData(effect.Header)));

            effectDropdown.AddOptions(options);
            effectDropdown.SetValueWithoutNotify(0);
            
            effectDropdown.onValueChanged.AddListener(AddEffect);
        }

        private void AddEffect(int index)
        {
            // ignore placeholder
            if(0 == index)
                return;

            index--;
            
            _selectedSynergie.Effects.Add(_synergieEffects[index]);
            _synergieEffects.RemoveAt(index);
            
            SetEffectDropdown();
            ShowSynergie(_selectedSynergie.Effects.Count - 1);
        }

        private void ShowEmptyUI()
        {
        }

        private void ShowSynergies()
        {
            _selectedSynergie = _synergies[0];
            ShowSynergie();
        }

        private void ShowSynergie(int selectedEffect = 0)
        {
            ClearSynergie();
            
            for (var index = 0; index < _selectedSynergie.Effects.Count; index++)
            {
                ShowEffect(_selectedSynergie.Effects[index], index, selectedEffect);
            }

            for (var index = 0; index < _selectedSynergie.Triggers.Count; index++)
            {
                ShowTrigger(_selectedSynergie.Triggers[index], index);
            }
        }

        private void ClearSynergie()
        {
            ShowDefaultEffectInfo();
            foreach (Transform child in effectParent.transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void ShowEffect(SynergieEffect effect, int index, int selectedEffect)
        {
            var effectButton = Instantiate(componentPrefab, effectParent.transform);
            effectButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-40, -50 * (index + 2));
            effectButton.GetComponent<ButtonUIElement>().label.text = effect.Header;
            effectButton.GetComponent<ButtonUIElement>().button.onClick.AddListener(() => ShowEffectInfo(effect, index));
            if (index == selectedEffect)
            {
                StartCoroutine(SelectButtonNextFrame(effectButton.GetComponent<ButtonUIElement>().button, selectedEffect));
            }


            var removeButton = Instantiate(removePrefab, effectParent.transform);
            removeButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(40, -50 * (index + 2));
            removeButton.GetComponent<ButtonUIElement>().button.onClick.AddListener(() => RemoveEffect(effect));
        }
        
        private IEnumerator SelectButtonNextFrame(Button button, int selectedEffect)
        {
            yield return null;
            
            button.Select();
            ShowEffectInfo(_selectedSynergie.Effects[selectedEffect], selectedEffect);
        }

        private void RemoveEffect(SynergieEffect effect)
        {
            _selectedSynergie.Effects.Remove(effect);
            ShowSynergie();
        }

        private void ShowDefaultEffectInfo()
        {
            effectHeader.text = string.Empty;
            effectValue.text = string.Empty;
            effectDescription.text = string.Empty;
            effectLevel.text = string.Empty;
            upgradeButton.gameObject.SetActive(false);
        }

        private void UpgradeEffect(SynergieEffect effect, int selectedEffectIndex)
        {
            effect.Upgrade();
            ShowSynergie(selectedEffectIndex);
        }

        private void ShowEffectInfo(SynergieEffect effect, int selectedEffectIndex)
        {
            effectHeader.text = effect.Header;
            effectValue.text = "value: " + effect.Value;
            effectDescription.text = effect.Description;
            effectLevel.text = "level: " + effect.Level;
            
            upgradeButton.onClick.RemoveAllListeners();
            upgradeButton.onClick.AddListener(() => UpgradeEffect(effect, selectedEffectIndex));
            upgradeButton.GetComponent<ButtonUIElement>().label.text = "upgrade (" + effect.UpgradeCost + ")";
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
    }
}