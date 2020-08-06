using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIManager : MonoBehaviour {

    [Header( "References" )]
    public IntValue winnerID;
    public IntValue playerID;
    public StringValue playerName;
    public IntValue score;
    public StringValue otherName;
    public IntValue otherScore;

    [Header( "UI References" )]
    public Text winnerName;
    public Text winnerScore;
    public Text loserName;
    public Text loserScore;

    private void Start() {
        InitializeUI();
    }

    private void OnEnable() {
        playerName.onValueChanged += OnStringUpdate;
        score.onValueChanged += OnIntUpdate;
        otherName.onValueChanged += OnStringUpdate;
        otherScore.onValueChanged += OnIntUpdate;
    }

    private void OnDisable() {
        playerName.onValueChanged -= OnStringUpdate;
        score.onValueChanged -= OnIntUpdate;
        otherName.onValueChanged -= OnStringUpdate;
        otherScore.onValueChanged -= OnIntUpdate;
    }

    public void OnIntUpdate( int value ) {
        InitializeUI();
    }

    public void OnStringUpdate( string value ) {
        InitializeUI();
    }

    public void InitializeUI() {
        if ( winnerID.Value == playerID.Value ) {
            winnerName.text = playerName.Value;
            winnerScore.text = score.Value.ToString();
            loserName.text = otherName.Value;
            loserScore.text = otherScore.Value.ToString();
        }
        else if ( winnerID.Value != playerID.Value ) {
            winnerName.text = otherName.Value;
            winnerScore.text = otherScore.Value.ToString();
            loserName.text = playerName.Value;
            loserScore.text = score.Value.ToString();
        }
    }
}
