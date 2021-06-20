using UnityEngine;
using UnityEngine.UI;

public class FillEnergyBar : MonoBehaviour {
    
    public Slider slider;
    
    private float currentValue = 0f;
    public float CurrentValue {
        get {
            return currentValue;
        }
        set {
            currentValue = value;
            slider.value = currentValue;
        }
    }

    void Start () {
        CurrentValue = 0f;
    }
}