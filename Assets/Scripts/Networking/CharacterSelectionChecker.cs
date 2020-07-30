using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionChecker : MonoBehaviour {

    [Header( "Server Side" )]
    public IntValue p0CharacterID;
    public IntValue p1CharacterID;

    [Header( "Client Side" )]
    public BoolValue isHost;
    public IntValue characterID;
    public IntValue otherCharacterID;
    public GameObject selectionUI;
    public GameObject waitingForOtherPlayerUI;

    private void OnEnable() {
        characterID.onValueChanged += CharacterSelected;
        otherCharacterID.onValueChanged += CharacterSelected;
    }

    public void CharacterSelected( int newValue ) {
        if ( otherCharacterID.Value < 0 && characterID.Value > -1 ) {
            if ( selectionUI != null && waitingForOtherPlayerUI != null ) {
                selectionUI.SetActive( false );
                waitingForOtherPlayerUI.SetActive( true );
            }
        }
        else if ( characterID.Value > -1 && otherCharacterID.Value > -1 ) {
            if ( isHost.Value ) {
                SceneManager.LoadSceneAsync( SceneManager.GetActiveScene().buildIndex + 2 );
            }
            else {
                SceneManager.LoadSceneAsync( SceneManager.GetActiveScene().buildIndex + 1 );
            }
        }
    }

}
