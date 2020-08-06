using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreInsert : MonoBehaviour {

    public BoolValue isHost;
    public StringValue sessionID;
    public Character_Set allCharacters;
    public string URL;

    [Header( "ThisPlayer Variables" )]
    public StringValue playerName;
    public IntValue characterID;
    public IntValue score;

    [Header( "OtherPlayer Variables" )]
    public StringValue otherName;
    public IntValue otherCharacterID;
    public IntValue otherScore;

    private void Start() {
        if ( isHost.Value ) {
            Debug.Log( playerName.Value + " | " + otherName.Value + " | " + sessionID.Value );
            if ( playerName.Value != "" && otherName.Value != "" && sessionID.Value != "" ) {
                InsertScores( 0 );
                InsertScores( 1 );
            }
        }
    }

    public void InsertScores( int playerID ) {
        if ( playerID == 0 ) {
            string charName = allCharacters.Items[ characterID.Value ].charName;
            StartCoroutine( Submit( playerName.Value, charName, score.Value ) );
        }
        else if ( playerID == 1 ) {
            string charName = allCharacters.Items[ otherCharacterID.Value ].charName;
            StartCoroutine( Submit( otherName.Value, charName, otherScore.Value ) );
        }
    }

    IEnumerator Submit( string playerName, string characterName, int score ) {
        Debug.Log( "Submitting scores" );
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add( new MultipartFormDataSection( "sessionID", sessionID.Value ) );
        formData.Add( new MultipartFormDataSection( "user", playerName ) );
        formData.Add( new MultipartFormDataSection( "pc", characterName ) );
        formData.Add( new MultipartFormDataSection( "score", score.ToString() ) );

        UnityWebRequest www = UnityWebRequest.Post( URL, formData );
        yield return www.SendWebRequest();

        if ( www.isNetworkError || www.isHttpError ) {
            Debug.Log( www.error );
        }
        else {
            if ( www.downloadHandler.text == "0" ) {
                Debug.Log( "Insert Failed" );
            }
            else if ( www.downloadHandler.text == "1" ) {
                Debug.Log( "Insert Succesful" );
            }
        }
    }
}
