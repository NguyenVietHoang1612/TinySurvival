using UnityEngine;

namespace GameRPG
{
    public class ToggleOpenCloseCraft : MonoBehaviour
    {

        [SerializeField] private RectTransform menuRectTransform;
        [SerializeField] private RectTransform startPosition;
        [SerializeField] private RectTransform endPosition;

        [SerializeField] private UI_CraftingManager ui_craftManager;


        [SerializeField] float speedCloseOpen = 500f;
        [SerializeField] private bool isOpen = false;

        private Vector2 targetPosition;
        private bool isMoving = false;

        private void Start()
        {
            if (menuRectTransform == null)
            {
                menuRectTransform = GetComponent<RectTransform>();
            }
        }

        private void Update()
        {
            if (isMoving)
            {
                menuRectTransform.anchoredPosition = Vector2.MoveTowards(menuRectTransform.anchoredPosition, targetPosition, Time.deltaTime * speedCloseOpen);

                if (Vector2.Distance(menuRectTransform.anchoredPosition, targetPosition) < 0.01f)
                {
                    menuRectTransform.anchoredPosition = targetPosition;
                    isMoving = false;
                    isOpen = !isOpen;
                }
            }

        }

        public void OnOpenCloseCraftIV()
        {
            if (!isMoving)
            {
                targetPosition = isOpen ? startPosition.anchoredPosition : endPosition.anchoredPosition;
                isMoving = true;

                if (!isOpen) 
                {
                    PlayerInputManager.Instance.DisableMouseAttackInput();
                    ui_craftManager.RefreshUI();
                }
                else 
                {
                    PlayerInputManager.Instance.EnableMouseAttackInput();
                }
            }

        }
    }
}
