using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public static void LoadNextScene() {
        SceneManager.LoadSceneAsync( SceneManager.GetActiveScene().buildIndex + 1 );
    }

    public static void LoadScene( int sceneIndex ) {
        SceneManager.LoadSceneAsync( sceneIndex );
    }
}
