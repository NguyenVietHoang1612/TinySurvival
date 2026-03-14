using TMPro;
using UnityEngine;

namespace GameRPG
{
    public class HealthUI : MonoBehaviour
    {
        private TMP_Text healthText;
        public Animator anim { get; private set; }


        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            healthText = GetComponentInChildren<TMP_Text>();
        }

        public void SetHealthValue(int currentValue, int maxValue)
        {
            healthText.text = "HP: " + currentValue + " / " + maxValue;
        }


    }
}
