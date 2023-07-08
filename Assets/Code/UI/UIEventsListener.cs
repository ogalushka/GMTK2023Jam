using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Code.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class UIEventsListener : MonoBehaviour
    {
        private const string activeButtonClass = "UnitButtonActive";

        private Button activeButton;
        private Button basicUnitButton;

        private void OnEnable()
        {
            var uiDocument = GetComponent<UIDocument>();
            basicUnitButton = uiDocument.rootVisualElement.Q<Button>("BaseUnit");
            basicUnitButton.RegisterCallback<ClickEvent>(OnBasicUnitButtonClicked);
        }

        public void OnBasicUnitButtonClicked(ClickEvent e)
        {
            
            if (activeButton == basicUnitButton)
            {
                DisableActiveButton();
                return;
            }

            DisableActiveButton();

            basicUnitButton.AddToClassList(activeButtonClass);
            activeButton = basicUnitButton;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (activeButton != null)
                {
                    activeButton.RemoveFromClassList(activeButtonClass);
                    activeButton = null;
                }
            }
        }

        private void DisableActiveButton()
        {
            if (activeButton != null)
            {
                activeButton.RemoveFromClassList(activeButtonClass);
                activeButton = null;
            }
        }
    }
}
