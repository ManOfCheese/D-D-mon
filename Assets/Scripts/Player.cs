using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [Header( "References" )]
    public Character_Set allCharacters;
    public Action_RunTimeSet actionList;
    public IntValue characterID;
    public IntValue hp;

    [Header( "Stats Dictionary" )]
    public CharacterStat[] playerStats;
    public IntValue[] statValues;

    [Header( "Moves Dictionary" )]
    public Action[] actions;
    public IntValue[] actionUses;

    public Dictionary<CharacterStat, IntValue> statData;
    public Dictionary<Action, IntValue> moveData;
    [HideInInspector] public Character character;

    private void Awake() {
        actions = new Action[ actionUses.Length ];

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

        if ( actionList != null ) {
            for ( int i = 0; i < this.character.moveSet.Length; i++ ) {
                actionList.Add( this.character.moveSet[ i ] );
            }
        }

        for ( int i = 0; i < actions.Length; i++ ) {
            actions[ i ] = this.character.moveSet[ i ];
        }

        moveData.Clear();
        for ( int i = 0; i < actions.Length; i++ ) {
            moveData.Add( actions[ i ], actionUses[ i ] );
        }

        SetStatsToDefault();
    }

    public void SetStatsToDefault() {
        hp.Value = character.charHP;
        for ( int i = 0; i < statValues.Length; i++ ) {
            statValues[ i ].Value = character.stats[ i ];
        }
        for ( int i = 0; i < character.moveSet.Length; i++ ) {
            actions[ i ] = character.moveSet[ i ];
        }
        for ( int i = 0; i < actionUses.Length; i++ ) {
            actionUses[ i ].Value = actions[ i ].moveUses;
        }
    }
}
