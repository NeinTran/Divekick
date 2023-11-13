using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] public string ClassName;
    [SerializeField] public float DiveForce;
    [SerializeField] public float KickForceUp;
    [SerializeField] public float KickForceBack;
    [SerializeField] public float DivekickForceForward;
    [SerializeField] public float DivekickForceDown;
    [SerializeField] public float DashDuration;
    [SerializeField] public float DashForceUp;
    [SerializeField] public int MaxHealth;
    [SerializeField] public int Health;
    [SerializeField] public int Defense;
    [SerializeField] public float CurrentKickMeter;
    [SerializeField] public float FactorConsumption;
    [SerializeField] public float SpecialConsume;
    [SerializeField] public float MaxKickMeter;
    [SerializeField] public float KickMeterCharge;
    [SerializeField] public float DivekickMeterCharge;
    [SerializeField] public int Score;
}
