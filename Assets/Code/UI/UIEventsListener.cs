using Assets.Code.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Code.UI
{
    [Serializable]
    public class ButtonMapping
    {
        public string buttonName;
        public WaveInfo units;
    }

    [RequireComponent(typeof(UIDocument))]
    public class UIEventsListener : MonoBehaviour
    {
        private const string activeButtonClass = "UnitButtonActive";

        public WaveInfo basicWaveInfo;
        public SelectedUnits selectedUnits;
        public ButtonMapping[] UnitButtons;

        private Button activeButton;

        private void OnEnable()
        {
            var uiDocument = GetComponent<UIDocument>();

            foreach (var buttonMapping in UnitButtons)
            {
                var button = uiDocument.rootVisualElement.Q<Button>(buttonMapping.buttonName);
                button.RegisterCallback<ClickEvent>((e) => OnButtonClick(button, buttonMapping.units));
            }
        }

        public void OnButtonClick(Button button, WaveInfo waveInfo)
        {
            if (activeButton == button)
            {
                DisableActiveButton();
                return;
            }

            DisableActiveButton();

            button.AddToClassList(activeButtonClass);
            activeButton = button;
            selectedUnits.waveInfo = waveInfo;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (activeButton != null)
                {
                    activeButton.RemoveFromClassList(activeButtonClass);
                    activeButton = null;
                    selectedUnits.waveInfo = null;
                }
            }
        }

        private void DisableActiveButton()
        {
            if (activeButton != null)
            {
                activeButton.RemoveFromClassList(activeButtonClass);
                activeButton = null;
                selectedUnits.waveInfo = null;
            }
        }
    }
}
