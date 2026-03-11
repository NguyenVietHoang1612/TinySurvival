using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace GameRPG
{
    public class MenuUIEvents : MonoBehaviour
    {
        public UnityEvent onButtonClicked, onButtonHover;
        private Button _startButton;
        private Button _loadButton;


        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;

            _startButton = root.Q<Button>("startButton");
            _loadButton = root.Q<Button>("loadButton");

            // Register the button click event
            _startButton.clicked += () => OnButtonStartClicked();
            _loadButton.clicked += () => OnButtonContinuesClicked();

            // Register the button press event
            _startButton.RegisterCallback<PointerEnterEvent>(evt => OnButtonHover());
        }

        public void OnButtonStartClicked()
        {
            Debug.Log("Button clicked!");
            onButtonClicked?.Invoke();

            SaveGameManager.Instance.continueButtonActive = false;
            SaveGameManager.Instance.NewGame();

        }

        public void OnButtonContinuesClicked()
        {
            Debug.Log("Continue button clicked!");
            onButtonClicked?.Invoke();

            SaveGameManager.Instance.continueButtonActive = true;
            SaveGameManager.Instance.ContinueGame();
        }



        public void OnButtonHover()
        {
            Debug.Log("Button pressed!");
            onButtonHover?.Invoke();
        }
    }
}
