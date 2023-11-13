using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactorBarScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public PlayerData playerData;
    void Start() {
        slider.maxValue = playerData.MaxKickMeter;
    }
    void Update() {
        slider.value = playerData.CurrentKickMeter;
    }
}
