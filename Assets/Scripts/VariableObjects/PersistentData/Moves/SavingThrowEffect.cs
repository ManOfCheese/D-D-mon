using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "SavingThrowEffect", menuName = "Moves/SavingThrowEffect" )]
public class SavingThrowEffect : AEffect {

    public CharacterStat savingThrowStat;

    [Header( "Variable Damage" )]
    public int[] diceAmount;
    public Dice[] diceTypes;
    public DamageType[] damageTypes;

    [Header( "Static Damage" )]
    public DamageType staticDamageType;
    public int staticDamage;

}
