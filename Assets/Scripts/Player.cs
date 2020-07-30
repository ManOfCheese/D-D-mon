using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [Header( "References" )]
    public Character_Set allCharacters;
    public IntValue characterID;
    public IntValue hp;

    [Header( "Stats Dictionary" )]
    public CharacterStat[] playerStats;
    public IntValue[] statValues;

    [Header( "Moves Dictionary" )]
    public Action[] moves;
    public IntValue[] moveUses;

    protected Dictionary<CharacterStat, IntValue> statData;
    protected Dictionary<Action, IntValue> moveData;
    protected Character character;

    private void Awake() {
        moves = new Action[ moveUses.Length ];

        statData = new Dictionary<CharacterStat, IntValue>();
        for ( int i = 0; i < playerStats.Length; i++ ) {
            statData.Add( playerStats[ i ], statValues[ i ] );
        }
        moveData = new Dictionary<Action, IntValue>();

        if ( characterID.Value >= 0 ) {
            PickCharacter( characterID.Value );
        }
    }

    private void OnEnable() {
        characterID.onValueChanged += PickCharacter;
    }

    public virtual void PickCharacter( int characterID ) {
        this.character = allCharacters.Items[ characterID ];
        for ( int i = 0; i < moves.Length; i++ ) {
            moves[ i ] = this.character.moveSet[ i ];
        }

        moveData.Clear();
        for ( int i = 0; i < moves.Length; i++ ) {
            moveData.Add( moves[ i ], moveUses[ i ] );
        }

        SetStatsToDefault();
    }

    public void SetStatsToDefault() {
        hp.Value = character.charHP;
        for ( int i = 0; i < statValues.Length; i++ ) {
            statValues[ i ].Value = character.stats[ i ];
        }
        for ( int i = 0; i < character.moveSet.Length; i++ ) {
            moves[ i ] = character.moveSet[ i ];
        }
        for ( int i = 0; i < moveUses.Length; i++ ) {
            moveUses[ i ].Value = moves[ i ].moveUses;
        }
    }
}
