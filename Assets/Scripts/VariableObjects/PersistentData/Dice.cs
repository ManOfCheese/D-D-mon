using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Dice", menuName = "PersistentData/Dice" )]
public class Dice : PersistentSetElement {

    public Dice_Set allDice;

    public string diceName;
    public int sides;

    private void Awake() {
        allDice.Add( this );
    }

    public int RollDice() {
        return Random.Range( 1, sides );
    }
}
