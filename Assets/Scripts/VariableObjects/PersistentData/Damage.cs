using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Damage", menuName = "PersistentData/Damage" )]
public class Damage : PersistentSetElement {

    public DamageType damageType;
    public Dice diceType;
    public int diceAmount;

}
