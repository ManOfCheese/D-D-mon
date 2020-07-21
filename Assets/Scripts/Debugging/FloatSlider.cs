using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatSlider : MonoBehaviour {

	public FloatValue floatValue;
	public Slider slider;
	public Text floatName;

	private void OnEnable() {
		if ( floatValue != null ) {
			floatValue.onValueChanged += OnValueChanged;
		}
	}

	private void OnDisable() {
		floatValue.onValueChanged -= OnValueChanged;
	}

	public void Initialize() {
		floatValue.onValueChanged += OnValueChanged;
		floatName.text = floatValue.displayName + ": " + floatValue.Value;
		slider.minValue = floatValue.minValue;
		slider.maxValue = floatValue.maxValue;
		OnValueChanged( floatValue.Value );
	}

	public void OnValueChanged( float value ) {
		if ( slider.value != value ){
			slider.value = value;
			floatName.text = floatValue.name + ": " + floatValue.Value;
		}
	}

	public void ChangeFloatValue() {
		floatValue.Value = slider.value;
	}

}
