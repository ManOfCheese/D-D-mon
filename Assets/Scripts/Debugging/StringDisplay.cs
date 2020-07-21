using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StringDisplay : MonoBehaviour {

    public StringValue stringValue;
    public Text stringName;

    private void OnEnable() {
        if ( stringValue != null ) {
            stringValue.onValueChanged += OnValueChanged;
        }
    }

    private void OnDisable() {
        stringValue.onValueChanged -= OnValueChanged;
    }

    public void Initialize() {
        stringValue.onValueChanged += OnValueChanged;
        stringName.text = stringValue.displayName + ": " + stringValue.Value;
        OnValueChanged( stringValue.Value );
    }

    public void OnValueChanged( string value ) {
        stringName.text = stringValue.name + ": " + stringValue.Value;
    }

}
