using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public StringValue playerName;
    public IntValue hp;

    [Header( "Stats Dictionary" )]
    public CharacterStat[] playerStats;
    public IntValue[] statValues;

    [Header( "Moves Dictionary" )]
    public Action[] moves;
    public IntValue[] moveUses;

    public Dictionary<CharacterStat, IntValue> statData;
    public Dictionary<Action, IntValue> moveData;

    private void Awake() {
        statData = new Dictionary<CharacterStat, IntValue>();
        for ( int i = 0; i < playerStats.Length; i++ ) {
            statData.Add( playerStats[ i ], statValues[ i ] );
        }

        moveData = new Dictionary<Action, IntValue>();
        for ( int i = 0; i < moves.Length; i++ ) {
            moveData.Add( moves[ i ], moveUses[ i ] );
        }
    }

}
