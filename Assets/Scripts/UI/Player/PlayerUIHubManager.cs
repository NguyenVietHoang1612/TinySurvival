using GameRPG;
using UnityEngine;

public class PlayerUIHubManager : MonoBehaviour
{
    public HealthUI healthUI;

    public void Awake()
    {
        healthUI = GetComponentInChildren<HealthUI>();
    }


}
