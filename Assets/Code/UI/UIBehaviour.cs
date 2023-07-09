using Assets.Code.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public class UIBehaviour : MonoBehaviour
    {
        private const string activeButtonClass = "UnitButtonActive";
        private const string priceSuffix = "Price";
        private const string hiddenStyle = "Hidden";

        public SelectedUnits selectedUnits;
        public ButtonMapping[] UnitButtons;

        private Button activeButton;
        private Label moneyLabel;
        private VisualElement gameOverScreen;
        private VisualElement gameWinScreen;

        // Dirty hack to get around OnEnable ordering
        private int moneyBeforeStartHack = -1;

        private void OnEnable()
        {
            var uiDocument = GetComponent<UIDocument>();

            foreach (var buttonMapping in UnitButtons)
            {
                var button = uiDocument.rootVisualElement.Q<Button>(buttonMapping.buttonName);
                var text = uiDocument.rootVisualElement.Q<Label>(buttonMapping.buttonName + priceSuffix);
                text.text = buttonMapping.units.price.ToString();
                button.RegisterCallback<ClickEvent>((e) => OnButtonClick(button, buttonMapping.units));

                if (activeButton == null)
                {
                    OnButtonClick(button, buttonMapping.units);
                }
            }


            moneyLabel = uiDocument.rootVisualElement.Q<Label>("PlayerMoney");
            if (moneyBeforeStartHack >= 0)
            {
                SetMoney(moneyBeforeStartHack);
            }

            gameOverScreen = uiDocument.rootVisualElement.Q<VisualElement>("GameOver");
            gameWinScreen = uiDocument.rootVisualElement.Q<VisualElement>("GameWin");

            var tryAgainButton = uiDocument.rootVisualElement.Q<Button>("TryAgainButton");
            tryAgainButton.RegisterCallback<ClickEvent>(e => RestartScene());
            var winAgainButton = uiDocument.rootVisualElement.Q<Button>("WinAgainButton");
            winAgainButton.RegisterCallback<ClickEvent>(e => RestartScene());
        }

        private void RestartScene()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }

        public void SetMoney(int money)
        {
            if (moneyLabel == null)
            {
                moneyBeforeStartHack = money;
                return;
            }

            moneyLabel.text = money.ToString();
        }

        public void OnButtonClick(Button button, WaveInfo waveInfo)
        {
            DisableActiveButton();

            button.AddToClassList(activeButtonClass);
            activeButton = button;
            selectedUnits.waveInfo = waveInfo;
        }

        private void DisableActiveButton()
        {
            if (activeButton != null)
            {
                activeButton.RemoveFromClassList(activeButtonClass);
            }
        }

        public void ShowGameOver()
        {
            gameOverScreen.RemoveFromClassList(hiddenStyle);
        }

        internal void ShowWin()
        {
            gameWinScreen.RemoveFromClassList(hiddenStyle);
        }
    }
}
