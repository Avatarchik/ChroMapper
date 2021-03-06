﻿using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dbValueText;
    [SerializeField] private Slider slider;
    
    public float value
    {
        get => slider.value;
        set => slider.value = value;
    }

    private void Start()
    {
        slider.onValueChanged.AddListener(OnHandleMove);
        value = (float?)GetComponent<SettingsBinder>()?.RetrieveValueFromSettings() ?? 0;
        UpdateDisplay();
    }

    private void OnHandleMove(float value)
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        dbValueText.text = value == 0f ? "Off" : (20.0f * Mathf.Log10(value)).ToString("F0") + " dB";
        SendMessage("SendValueToSettings", value);
    }
}