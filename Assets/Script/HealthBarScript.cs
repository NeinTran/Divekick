using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public PlayerData playerData;
    void Start() {
        slider.maxValue = playerData.MaxHealth;
    }
    void Update() {
        slider.value = playerData.Health;
    }
}
