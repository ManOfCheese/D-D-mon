using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneLoader : MonoBehaviour {

    public IntValue clientWinnderID;

    private void OnEnable() {
        clientWinnderID.onValueChanged += LoadNextScene;
    }

    private void OnDisable() {
        clientWinnderID.onValueChanged -= LoadNextScene;
    }

    public void LoadNextScene( int newValue ) {
        if ( newValue > -1 ) {
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1 );
        }
    }
}
