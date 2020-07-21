using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoolToggle : MonoBehaviour {

	public BoolValue boolValue;
	public GameObject valueIndicator;
	public Text boolName;

	private void OnEnable() {
		if ( boolValue != null ) {
			boolValue.onValueChanged += OnValueChanged;
		}
	}

	private void OnDisable() {
		boolValue.onValueChanged -= OnValueChanged;
	}

	public void Initialize() {
		boolValue.onValueChanged += OnValueChanged;
		boolName.text = boolValue.displayName;
		OnValueChanged( boolValue.Value );
	}

	public void OnValueChanged( bool value ) {
		if ( value ) {
			valueIndicator.SetActive( true );
		}
		else {
			valueIndicator.SetActive( false );
		}
	}

	public void ToggleBoolValue() {
		boolValue.Value = !boolValue.Value;
	}
}
