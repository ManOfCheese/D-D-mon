using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "HealEffect", menuName = "Moves/HealEffect" )]
public class HealEffect : AEffect {

    [Header( "Variable Healing" )]
    public int[] diceAmount;
    public Dice[] diceTypes;

    [Header( "Static Healinh" )]
    public int staticHealing;
    public bool addStatModifierToDamage;

}

