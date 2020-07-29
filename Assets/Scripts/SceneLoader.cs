using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void LoadNextScene() {
        SceneManager.LoadSceneAsync( SceneManager.GetActiveScene().buildIndex + 1 );
    }

    public void LoadScene( int sceneIndex ) {
        SceneManager.LoadSceneAsync( sceneIndex );
    }
}
