using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntSlider : MonoBehaviour {

	public IntValue intValue;
	public Slider slider;
	public Text intName;

	private void OnEnable() {
		if ( intValue != null ) {
            intValue.onValueChanged += OnValueChanged;
		}
	}

	private void OnDisable() {
        intValue.onValueChanged -= OnValueChanged;
	}

	public void Initialize() {
        intValue.onValueChanged += OnValueChanged;
        intName.text = intValue.displayName + ": " + intValue.Value;
		slider.minValue = intValue.minValue;
		slider.maxValue = intValue.maxValue;
		OnValueChanged( intValue.Value );
	}

	public void OnValueChanged( int value ) {
		if ( slider.value != value ){
			slider.value = value;
			intName.text = intValue.name + ": " + intValue.Value;
		}
	}

	public void ChangeIntValue() {
        intValue.Value = Mathf.RoundToInt( slider.value );
	}

}
