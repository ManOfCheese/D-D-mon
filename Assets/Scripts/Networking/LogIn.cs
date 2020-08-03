using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LogInInfo {
    public int id;
    public string username;
}

public class LogIn : MonoBehaviour {

    [Header( "References" )]
    public Text feedbackText;
    public IntValue userID;
    public StringValue sessionID;
    public StringValue playerName;

    [Header( "Settings" )]
    public bool isServerLogIn;
    public string URL;
    public string[] variableNames;
    public InputField[] inputFields;

    public void OnSubmit() {
        StartCoroutine( Submit() );
    }

    IEnumerator Submit() {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        for ( int i = 0; i < Mathf.Min( variableNames.Length, inputFields.Length ); i++ ) {
            formData.Add( new MultipartFormDataSection( variableNames[ i ], inputFields[ i ].text ) );
        }

        UnityWebRequest www = UnityWebRequest.Post( URL, formData );
        yield return www.SendWebRequest();

        if ( www.isNetworkError || www.isHttpError ) {
            Debug.Log( www.error );
        }
        else {
            Debug.Log( www.downloadHandler.text );
            if ( isServerLogIn ) {
                if ( www.downloadHandler.text != "0" ) {
                    sessionID.Value = www.downloadHandler.text;
                    feedbackText.text = "Logging in...";
                    SceneLoader.LoadNextScene();
                }
                else {
                    feedbackText.text = "Server does not exist or password is incorrect";
                }
            }
            else {
                if ( www.downloadHandler.text == "Invalid email" ) {
                    feedbackText.text = "Invalid e-mail";
                }
                if ( www.downloadHandler.text != "0" ) {
                    LogInInfo logInInfo = new LogInInfo();
                    logInInfo = JsonUtility.FromJson<LogInInfo>( www.downloadHandler.text );
                    userID.Value = logInInfo.id;
                    playerName.Value = logInInfo.username;
                    feedbackText.text = "Logging in...";
                    SceneLoader.LoadNextScene();
                }
            }
        }
    }
}
