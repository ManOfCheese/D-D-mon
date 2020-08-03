using System.Collections;
using System.Collections.Generic;
using Unity.Entities.UniversalDelegates;
using UnityEngine;

public class CombatManager : MonoBehaviour {

    public BoolValue takeAction;
    public Action_RunTimeSet queuedActions;
    public IntValue whoseTurn;
    public Player[] players;
    public Dice d20;
    public CharacterStat armorClass;
    public CharacterStat profBonus;

    private void OnEnable() {
        takeAction.onValueChanged += OnTakeAction;
    }

    public void OnTakeAction( bool value ) {
        if ( value ) {
            int actingPlayerIndex = whoseTurn.Value;
            int otherPlayerIndex;
            if ( whoseTurn.Value + 1 > players.Length - 1 ) {
                otherPlayerIndex = 0;
            }
            else {
                otherPlayerIndex = 1;
            }

            for ( int i = 0; i < queuedActions.Items.Count; i++ ) {

                for ( int j = 0; j < queuedActions.Items[ i ].attackEffects.Length; j++ ) {
                    players[ actingPlayerIndex ].statData.TryGetValue( players[ actingPlayerIndex ].character.attackStat, out IntValue attackStat );
                    int attackModifier = RPGMath.StatMath.ModifierFromStatValue( attackStat.Value );
                    players[ otherPlayerIndex ].statData.TryGetValue( armorClass, out IntValue AC );

                    if ( d20.RollDice() + attackModifier + queuedActions.Items[ i ].attackEffects[ j ].weaponBonus > AC.Value ) {
                        Dictionary<DamageType, int> damage = new Dictionary<DamageType, int>();
                        int newDamage;

                        //Roll all dice and count up the damage.
                        for ( int k = 0; k < queuedActions.Items[ i ].attackEffects[ j ].diceAmount.Length; k++ ) {
                            newDamage = queuedActions.Items[ i ].attackEffects[ j ].diceTypes[ k ].RollDice();

                            //Sort damage into the dictionary based on DamageType;
                            if ( damage.ContainsKey( queuedActions.Items[ i ].attackEffects[ j ].damageTypes[ k ] ) ) {
                                damage.Add( queuedActions.Items[ i ].attackEffects[ j ].damageTypes[ k ], newDamage );
                            }
                            else {
                                damage[ queuedActions.Items[ i ].attackEffects[ j ].damageTypes[ k ] ] += newDamage;
                            }
                        }

                        //Add static damage.
                        newDamage = queuedActions.Items[ i ].attackEffects[ j ].staticDamage;

                        if ( damage.ContainsKey( queuedActions.Items[ i ].attackEffects[ j ].staticDamageType ) ) {
                            damage.Add( queuedActions.Items[ i ].attackEffects[ j ].staticDamageType, newDamage );
                        }
                        else {
                            damage[ queuedActions.Items[ i ].attackEffects[ j ].staticDamageType ] += newDamage;
                        }

                        //If necessary add the attack stat modifier.
                        if ( queuedActions.Items[ i ].attackEffects[ j ].addStatModifierToDamage ) {
                            newDamage = attackModifier;

                            if ( damage.ContainsKey( queuedActions.Items[ i ].attackEffects[ j ].statModifierDamageType ) ) {
                                damage.Add( queuedActions.Items[ i ].attackEffects[ j ].statModifierDamageType, newDamage );
                            }
                            else {
                                damage[ queuedActions.Items[ i ].attackEffects[ j ].statModifierDamageType ] += newDamage;
                            }
                        }

                        int finalDamage = 0;

                        //Go through damageType, damage pair and account for damage resistances;
                        foreach ( KeyValuePair<DamageType, int> entry in damage ) {
                            players[ actingPlayerIndex ].statData.TryGetValue( entry.Key.resistanceStat, out IntValue damageResistance );
                            damage[ entry.Key ] *= ( damageResistance.Value / 100 );
                            finalDamage += damage[ entry.Key ];
                        }

                        players[ otherPlayerIndex ].hp.Value -= Mathf.RoundToInt( finalDamage );
                    }
                }

                for ( int j = 0; j < queuedActions.Items[ j ].healEffects.Length; j++ ) {
                    int healing = 0;

                    //Roll the dice and count up the healing.
                    for ( int k = 0; k < queuedActions.Items[ i ].healEffects[ j ].diceAmount.Length; k++ ) {
                        healing += queuedActions.Items[ i ].healEffects[ j ].diceTypes[ k ].RollDice();
                    }
                    healing += queuedActions.Items[ i ].healEffects[ j ].staticHealing;

                    if ( queuedActions.Items[ i ].healEffects[ j ].addStatModifierToHealing ) {
                        players[ actingPlayerIndex ].statData.TryGetValue( players[ actingPlayerIndex ].character.spellStat, out IntValue stat );
                        healing += RPGMath.StatMath.ModifierFromStatValue( stat.Value );
                    }

                    players[ actingPlayerIndex ].hp.Value += healing;
                }
                for ( int j = 0; j < queuedActions.Items[ i ].defendEffects.Length; j++ ) {
                    for ( int k = 0; k < queuedActions.Items[ i ].defendEffects[ j ].defendsAgainst.Length; k++ ) {
                        players[ actingPlayerIndex ].statData.TryGetValue( queuedActions.Items[ i ].defendEffects[ j ].defendsAgainst[ k ].resistanceStat, out IntValue damageResistance );
                        damageResistance.Value -= Mathf.RoundToInt( queuedActions.Items[ i ].defendEffects[ j ].defenseIncrease[ k ] * 100 );
                    }
                }
                for ( int j = 0; j < queuedActions.Items[ j ].savingThrowEffects.Length; j++ ) {
                    //Calculate save DC.
                    players[ actingPlayerIndex ].statData.TryGetValue( players[ actingPlayerIndex ].character.spellStat, out IntValue spellStat );
                    players[ actingPlayerIndex ].statData.TryGetValue( profBonus, out IntValue proficiencyBonus );
                    int saveDC = 8 + RPGMath.StatMath.ModifierFromStatValue( spellStat.Value ) + proficiencyBonus.Value;

                    float damageModifier = 1f;
                    if ( d20.RollDice() > saveDC ) {
                        damageModifier = 0.5f;
                    }

                    Dictionary<DamageType, int> damage = new Dictionary<DamageType, int>();
                    int newDamage;

                    //Roll all dice and count up the damage.
                    for ( int k = 0; k < queuedActions.Items[ i ].savingThrowEffects[ j ].diceAmount.Length; k++ ) {
                        newDamage = queuedActions.Items[ i ].savingThrowEffects[ j ].diceTypes[ k ].RollDice();

                        //Sort damage into the dictionary based on DamageType;
                        if ( damage.ContainsKey( queuedActions.Items[ i ].savingThrowEffects[ j ].damageTypes[ k ] ) ) {
                            damage.Add( queuedActions.Items[ i ].savingThrowEffects[ j ].damageTypes[ k ], newDamage );
                        }
                        else {
                            damage[ queuedActions.Items[ i ].savingThrowEffects[ j ].damageTypes[ k ] ] += newDamage;
                        }
                    }

                    //Add static damage.
                    newDamage = queuedActions.Items[ i ].savingThrowEffects[ j ].staticDamage;

                    if ( damage.ContainsKey( queuedActions.Items[ i ].savingThrowEffects[ j ].staticDamageType ) ) {
                        damage.Add( queuedActions.Items[ i ].savingThrowEffects[ j ].staticDamageType, newDamage );
                    }
                    else {
                        damage[ queuedActions.Items[ i ].savingThrowEffects[ j ].staticDamageType ] += newDamage;
                    }

                    int finalDamage = 0;

                    //Go through damageType, damage pair and account for damage resistances;
                    foreach ( KeyValuePair<DamageType, int> entry in damage ) {
                        players[ actingPlayerIndex ].statData.TryGetValue( entry.Key.resistanceStat, out IntValue damageResistance );
                        damage[ entry.Key ] *= ( damageResistance.Value / 100 );
                        finalDamage += damage[ entry.Key ];
                    }

                    players[ otherPlayerIndex ].hp.Value -= Mathf.RoundToInt( finalDamage * damageModifier );
                }
            }
            queuedActions.Items.Clear();
        }
    }

}
