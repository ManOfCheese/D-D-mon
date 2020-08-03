using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionChecker : MonoBehaviour {

    public BoolValue isHost;
    public BoolValue isConnected;
    public IntValue connectionCount;

    private void Update() {
        if ( isHost.Value ) {
            if ( connectionCount.Value == 2 ) {
                SceneManager.LoadSceneAsync( SceneManager.GetActiveScene().buildIndex + 1 );
            }
        }
        else if ( isConnected.Value ) {
            SceneManager.LoadSceneAsync( SceneManager.GetActiveScene().buildIndex + 2 );
        }
    }
}
