using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Action", menuName = "Moves/Action" )]
public class Action : PersistentSetElement {

    public string moveName;
    [TextArea]
    public string moveDescription;
    public int moveUses;
    public AttackEffect[] attackEffects;
    public HealEffect[] healEffects;
    public DefendEffect[] defendEffects;
    public SavingThrowEffect[] savingThrowEffects;

}
