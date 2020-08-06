using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Username {
    public string username;
}

public class UserNameListener : MonoBehaviour {

    public IntValue userID;
    public StringValue username;
    public IntValue otherUserID;
    public StringValue otherUsername;

    private void OnEnable() {
        userID.onValueChanged += UpdateP0UserName;
        otherUserID.onValueChanged += UpdateP1UserName;
    }

    private void OnDisable() {
        userID.onValueChanged -= UpdateP0UserName;
        otherUserID.onValueChanged -= UpdateP1UserName;
    }

    public void UpdateP0UserName( int value ) {
        StartCoroutine( Submit( 0, value ) );
    }

    public void UpdateP1UserName( int value ) {
        StartCoroutine( Submit( 1, value ) );
    }

    IEnumerator Submit( int playerID, int newID ) {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add( new MultipartFormDataSection( "userID", newID.ToString() ) );

        UnityWebRequest www = UnityWebRequest.Post( "https://studenthome.hku.nl/~sam.walet/Database/UserNameByID", formData );
        yield return www.SendWebRequest();

        if ( www.isNetworkError || www.isHttpError ) {
            Debug.Log( www.error );
        }
        else {
            if ( playerID == 0 ) {
                if ( www.downloadHandler.text != "0" ) {
                    Username newUsername = new Username();
                    newUsername = JsonUtility.FromJson<Username>( www.downloadHandler.text );
                    username.Value = newUsername.username;
                }
                else {
                    Debug.Log( "Could not find user" );
                }
            }
            else if ( playerID == 1 ) {
                if ( www.downloadHandler.text != "0" ) {
                    Username newUsername = new Username();
                    newUsername = JsonUtility.FromJson<Username>( www.downloadHandler.text );
                    otherUsername.Value = newUsername.username;
                }
                else {
                    Debug.Log( "Could not find user" );
                }
            }
        }
    }
}
