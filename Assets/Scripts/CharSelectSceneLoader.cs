using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharSelectSceneLoader : MonoBehaviour {

    public GameObject waitingForOtherPlayerUI;
    public IntValue thisCharacterID;
    public IntValue otherCharacterID;
    public BoolValue isHost;

    private void OnEnable() {
        thisCharacterID.onValueChanged += OnCharacterIDChange;
        otherCharacterID.onValueChanged += OnCharacterIDChange;
    }

    public void LoadSceneBasedOnHostStatus() {
        if ( isHost.Value ) {
            SceneManager.LoadSceneAsync( SceneManager.GetActiveScene().buildIndex + 2 );
        }
        else if ( !isHost.Value ) {
            SceneManager.LoadSceneAsync( SceneManager.GetActiveScene().buildIndex + 1 );
        }
    }

    public void OnCharacterIDChange( int newValue ) {
        if ( thisCharacterID.Value >= 0 && otherCharacterID.Value >= 0 ) {
            LoadSceneBasedOnHostStatus();
        }
        else if ( thisCharacterID.Value >= 0 && otherCharacterID.Value < 0 ) {
            waitingForOtherPlayerUI.SetActive( true );
        }
    }
}
