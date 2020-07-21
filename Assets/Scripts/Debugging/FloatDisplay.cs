using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatDisplay : MonoBehaviour {

    public FloatValue floatValue;
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
        OnValueChanged( floatValue.Value );
    }

    public void OnValueChanged( float value ) {
        floatName.text = floatValue.name + ": " + floatValue.Value;
    }
}
