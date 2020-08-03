using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "AttackEffect", menuName = "Moves/AttackEffect" )]
public class AttackEffect : AEffect {

    [Header( "Variable Damage" )]
    public int[] diceAmount;
    public Dice[] diceTypes;
    public DamageType[] damageTypes;

    [Header( "Static Damage" )]
    public DamageType staticDamageType;
    public int staticDamage;
    public int weaponBonus;
    public DamageType statModifierDamageType;
    public bool addStatModifierToDamage;

}
