using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GameRPG
{
    public class HungerUI : MonoBehaviour
    {
        [Header("UI Components")]
        private Slider hungerSlide;
        [SerializeField] private Image hungerImage;

        [Header("Sprites Settings")]
        [SerializeField] private Sprite fullIcon, hungerIcon;
        public Animator anim { get; private set; }

        private void Start()
        {
            hungerSlide = GetComponentInChildren<Slider>();
            anim = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            UpdateIconStatus();
        }

        private void UpdateIconStatus()
        {
            float threshold = hungerSlide.maxValue * 0.3f;

            if (hungerSlide.value < threshold)
            {
                if (hungerImage.sprite != hungerIcon) 
                {
                    hungerImage.sprite = hungerIcon;
                }
            }
            else
            {
                if (hungerImage.sprite != fullIcon)
                {
                    hungerImage.sprite = fullIcon;
                }
            }
        }

        public void SetCurrentHungerValue(float currentValue)
        {
            hungerSlide.value = currentValue;
        }

        public void SetMaxHungerValue(int maxValue)
        {
            hungerSlide.maxValue = maxValue;
        }

        public void PlayHungerAnimation()
        {
            if (anim != null)
            {
                anim.Play("HungerIncrease");
            }
        }
    }
}
