using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntValueListener : MonoBehaviour {

    public IntValue intValue;
    public Text text;

    private void OnEnable() {
        text.text = intValue.Value.ToString();
        intValue.onValueChanged += OnValueChanged;
    }

    private void OnDisable() {
        intValue.onValueChanged -= OnValueChanged;
    }

    public void OnValueChanged( int intValue ) {
        text.text = intValue.ToString();
    }
}
