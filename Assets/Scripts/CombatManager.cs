using System.Collections;
using System.Collections.Generic;
using Unity.Entities.UniversalDelegates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour {

    [Header( "References" )]
    public BoolValue takeAction;
    public Action_RunTimeSet queuedActions;
    public IntValue whoseTurn;
    public Player[] players;
    public Dice d20;
    public CharacterStat armorClass;
    public CharacterStat profBonus;
    public IntValue p0Score;
    public IntValue p1Score;
    public IntValue winnerID;

    [Header( "Networking" )]
    public NetworkEvent_RunTimeSet serverToClientEventQueue;
    public NetworkEvent updateWorldStateEvent;
    public NetworkEvent winEvent;

    private int turnCount;

    private void OnEnable() {
        takeAction.onValueChanged += OnTakeAction;
    }

    private void OnDisable() {
        takeAction.onValueChanged -= OnTakeAction;
    }

    public void OnTakeAction( bool value ) {
        if ( value ) {
            turnCount++;
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
                            for ( int l = 0; l < queuedActions.Items[ i ].attackEffects[ j ].diceAmount[ k ]; l++ ) {
                                newDamage = queuedActions.Items[ i ].attackEffects[ j ].diceTypes[ k ].RollDice();

                                //Sort damage into the dictionary based on DamageType;
                                if ( damage.ContainsKey( queuedActions.Items[ i ].attackEffects[ j ].damageTypes[ k ] ) ) {
                                    damage[ queuedActions.Items[ i ].attackEffects[ j ].damageTypes[ k ] ] += newDamage;
                                }
                                else {
                                    damage.Add( queuedActions.Items[ i ].attackEffects[ j ].damageTypes[ k ], newDamage );
                                }
                            }
                        }

                        //Add static damage.
                        newDamage = queuedActions.Items[ i ].attackEffects[ j ].staticDamage;

                        if ( queuedActions.Items[ i ].attackEffects[ j ].staticDamageType != null ) {
                            if ( damage.ContainsKey( queuedActions.Items[ i ].attackEffects[ j ].staticDamageType ) ) {
                                damage.Add( queuedActions.Items[ i ].attackEffects[ j ].staticDamageType, newDamage );
                            }
                            else {
                                damage[ queuedActions.Items[ i ].attackEffects[ j ].staticDamageType ] += newDamage;
                            }
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

                        finalDamage = Mathf.RoundToInt( finalDamage );
                        players[ otherPlayerIndex ].hp.Value -= finalDamage;
                        if ( actingPlayerIndex == 0 ) {
                            p0Score.Value += finalDamage;
                        }
                        else if ( actingPlayerIndex == 1 ) {
                            p1Score.Value += finalDamage;
                        }
                    }
                    else {
                        Debug.Log( "Missed" );
                    }
                }

                for ( int j = 0; j < queuedActions.Items[ i ].healEffects.Length; j++ ) {
                    int healing = 0;

                    //Roll the dice and count up the healing.
                    for ( int k = 0; k < queuedActions.Items[ i ].healEffects[ j ].diceAmount.Length; k++ ) {
                        for ( int l = 0; l < queuedActions.Items[ i ].healEffects[ j ].diceAmount[ k ]; l++ ) {
                            healing += queuedActions.Items[ i ].healEffects[ j ].diceTypes[ k ].RollDice();
                        }
                    }
                    healing += queuedActions.Items[ i ].healEffects[ j ].staticHealing;

                    if ( queuedActions.Items[ i ].healEffects[ j ].addStatModifierToHealing ) {
                        players[ actingPlayerIndex ].statData.TryGetValue( players[ actingPlayerIndex ].character.spellStat, out IntValue stat );
                        healing += RPGMath.StatMath.ModifierFromStatValue( stat.Value );
                    }

                    players[ actingPlayerIndex ].hp.Value += healing;
                    Debug.Log( "healed: " + healing );

                    if ( actingPlayerIndex == 0 ) {
                        p0Score.Value += healing;
                    }
                    else if ( actingPlayerIndex == 1 ) {
                        p1Score.Value += healing;
                    }
                }
                for ( int j = 0; j < queuedActions.Items[ i ].defendEffects.Length; j++ ) {
                    for ( int k = 0; k < queuedActions.Items[ i ].defendEffects[ j ].defendsAgainst.Length; k++ ) {
                        players[ actingPlayerIndex ].statData.TryGetValue( queuedActions.Items[ i ].defendEffects[ j ].defendsAgainst[ k ].resistanceStat, out IntValue damageResistance );
                        damageResistance.Value -= Mathf.RoundToInt( queuedActions.Items[ i ].defendEffects[ j ].defenseIncrease[ k ] * 100 );
                        Debug.Log( "Changed: " + queuedActions.Items[ i ].defendEffects[ j ].defendsAgainst[ k ].resistanceStat.name
                            + " to " + damageResistance.Value );
                    }
                }
                for ( int j = 0; j < queuedActions.Items[ i ].savingThrowEffects.Length; j++ ) {
                    //Calculate save DC.
                    players[ actingPlayerIndex ].statData.TryGetValue( players[ actingPlayerIndex ].character.spellStat, out IntValue spellStat );
                    players[ actingPlayerIndex ].statData.TryGetValue( profBonus, out IntValue proficiencyBonus );
                    int saveDC = 8 + RPGMath.StatMath.ModifierFromStatValue( spellStat.Value ) + proficiencyBonus.Value;

                    float damageModifier = 1f;
                    players[ otherPlayerIndex ].statData.TryGetValue( queuedActions.Items[ i ].savingThrowEffects[ j ].savingThrowStat, out IntValue savingThrowStat );
                    int roll = d20.RollDice() + RPGMath.StatMath.ModifierFromStatValue( savingThrowStat.Value );
                    if ( roll > saveDC ) {
                        damageModifier = 0.5f;
                        Debug.Log( roll + " / " + saveDC + " Save Succeeded!" );
                    }
                    Debug.Log( roll + " / " + saveDC + " Save Failed" );

                    Dictionary<DamageType, int> damage = new Dictionary<DamageType, int>();
                    int newDamage;

                    //Roll all dice and count up the damage.
                    for ( int k = 0; k < queuedActions.Items[ i ].savingThrowEffects[ j ].diceAmount.Length; k++ ) {
                        for ( int l = 0; l < queuedActions.Items[ i ].savingThrowEffects[ j ].diceAmount[ k ]; l++ ) {
                            newDamage = queuedActions.Items[ i ].savingThrowEffects[ j ].diceTypes[ k ].RollDice();

                            //Sort damage into the dictionary based on DamageType;
                            if ( damage.ContainsKey( queuedActions.Items[ i ].savingThrowEffects[ j ].damageTypes[ k ] ) ) {
                                damage[ queuedActions.Items[ i ].savingThrowEffects[ j ].damageTypes[ k ] ] += newDamage;
                            }
                            else {
                                damage.Add( queuedActions.Items[ i ].savingThrowEffects[ j ].damageTypes[ k ], newDamage );
                            }
                        }
                    }

                    //Add static damage.
                    newDamage = queuedActions.Items[ i ].savingThrowEffects[ j ].staticDamage;

                    if ( queuedActions.Items[ i ].savingThrowEffects[ j ].staticDamageType != null ) {
                        if ( damage.ContainsKey( queuedActions.Items[ i ].savingThrowEffects[ j ].staticDamageType ) ) {
                            damage.Add( queuedActions.Items[ i ].savingThrowEffects[ j ].staticDamageType, newDamage );
                        }
                        else {
                            damage[ queuedActions.Items[ i ].savingThrowEffects[ j ].staticDamageType ] += newDamage;
                        }
                    }

                    int finalDamage = 0;

                    //Go through damageType, damage pair and account for damage resistances;
                    foreach ( KeyValuePair<DamageType, int> entry in damage ) {
                        players[ actingPlayerIndex ].statData.TryGetValue( entry.Key.resistanceStat, out IntValue damageResistance );
                        int damageOfType = damage[ entry.Key ] * ( damageResistance.Value / 100 );
                        finalDamage += damageOfType;
                    }

                    finalDamage = Mathf.RoundToInt( finalDamage * damageModifier );
                    players[ otherPlayerIndex ].hp.Value -= finalDamage;
                    Debug.Log( "dealt: " + finalDamage + " damage" );

                    if ( actingPlayerIndex == 0 ) {
                        p0Score.Value += finalDamage;
                        p1Score.Value -= finalDamage;
                    }
                    else if ( actingPlayerIndex == 1 ) {
                        p1Score.Value += finalDamage;
                        p0Score.Value -= finalDamage;
                    }
                }
            }
            queuedActions.Items.Clear();
            whoseTurn.Value += 1;
            if ( whoseTurn.Value > 1 ) {
                whoseTurn.Value = 0;
            }

            if ( players[ otherPlayerIndex ].hp.Value <= 0 ) {
                winnerID.Value = actingPlayerIndex;
                OnGameEnd();
            }
            else if ( players[ actingPlayerIndex ].hp.Value <= 0 ) {
                winnerID.Value = otherPlayerIndex;
                OnGameEnd();
            }
            serverToClientEventQueue.Add( updateWorldStateEvent );
            takeAction.Value = false;
        }
    }

    public void OnGameEnd() {
        p0Score.Value /= turnCount;
        p1Score.Value /= turnCount;
        serverToClientEventQueue.Add( winEvent );
    }
}
