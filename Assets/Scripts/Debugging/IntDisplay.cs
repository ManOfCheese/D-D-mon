using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntDisplay : MonoBehaviour {

    public IntValue intValue;
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
        OnValueChanged( intValue.Value );
    }

    public void OnValueChanged( int value ) {
        intName.text = intValue.name + ": " + intValue.Value;
    }
}
