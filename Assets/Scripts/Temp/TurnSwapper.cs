using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSwapper : MonoBehaviour {

    public IntValue playerID;
    public IntValue whoseTurn;
    public Text text;

    private void OnEnable() {
        OnDataChange( whoseTurn.Value );
        whoseTurn.onValueChanged += OnDataChange;
        playerID.onValueChanged += OnDataChange;
    }

    private void OnDisable() {
        whoseTurn.onValueChanged -= OnDataChange;
        playerID.onValueChanged -= OnDataChange;
    }

    public void OnDataChange( int value ) {
        if ( whoseTurn.Value != playerID.Value ) {
            text.text = "It's not your turn";
        }
        else if ( whoseTurn.Value == playerID.Value ) {
            text.text = "It's your turn";
        }
    }
}
