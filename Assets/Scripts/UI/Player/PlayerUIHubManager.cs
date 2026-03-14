using GameRPG;
using UnityEngine;

public class PlayerUIHubManager : MonoBehaviour
{
    public HealthUI HealthUI { get; private set; }
    public HungerUI HungerUI { get; private set; }

    public void Awake()
    {
        HealthUI = GetComponentInChildren<HealthUI>();
        HungerUI = GetComponentInChildren<HungerUI>();
    }


}
