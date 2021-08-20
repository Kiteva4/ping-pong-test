using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorChannelPicker : MonoBehaviour
{
    public event Action<float> PickerValueUpdate;
    
    [SerializeField] 
    private TMP_Text channelValueText;

    [SerializeField]
    private Slider slider;

    public float Value
    {
        get => slider.value;
        set
        {
            slider.value = value;
            channelValueText.text = value.ToString("F");
        }
    }

    private void OnEnable() => slider.onValueChanged.AddListener(OnSliderValueChanged);

    private void OnDisable() => slider.onValueChanged.RemoveListener(OnSliderValueChanged);

    private void OnSliderValueChanged(float value)
    {
        channelValueText.text = value.ToString("F");
        PickerValueUpdate?.Invoke(value);
    }
}
