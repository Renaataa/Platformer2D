using UnityEngine;
using UnityEngine.UI;

public class FillHealthBar : MonoBehaviour {
    
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
        CurrentValue = 1f;
    }
}