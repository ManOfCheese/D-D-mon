using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [Header( "References" )]
    public Character_Set allCharacters;
    public Action_RunTimeSet actionList;

    [Header( "ThisPlayer References" )]
    public IntValue hp;
    public IntValue characterID;
    public IntValue[] moveUses;
    public IntValue[] statValues;
    public CharacterStat[] characterStats;

    [Space(10)]
    public Text charNameText;
    public Text charLevelText;
    public Image charImage;
    public Text charHP;
    public Text[] moveNamesText;
    public Text[] moveUsesText;
    public Image[] statIcons;
    public Text[] statValueTexts;

    [Header( "OtherPlayer References" )]
    public IntValue otherHP;
    public IntValue otherCharacterID;

    [Space( 10 )]
    public Text otherCharNameText;
    public Text otherCharLevelText;
    public Image otherCharImage;

    private Character thisCharacter;
    private Character otherCharacter;

    private void Start() {
        InitializeUI();
    }

    public virtual void OnEnable() {
        hp.onValueChanged += OnHPChange;
        otherCharacterID.onValueChanged += OtherCharacterIDChanged;
        actionList.OnAdded += UpdateActionUI;
    }

    public virtual void InitializeUI() {
        thisCharacter = allCharacters.Items[ characterID.Value ];

        charNameText.text = thisCharacter.charName;
        charLevelText.text = "Lvl. " + thisCharacter.charLevel.ToString();
        charImage.sprite = thisCharacter.charBackSprite;
        charHP.text = hp.Value + " / " + thisCharacter.charHP;

        for ( int i = 0; i < statIcons.Length; i++ ) {
            statIcons[ i ].sprite = characterStats[ i ].statIcon;
        }
        for ( int i = 0; i < statValueTexts.Length; i++ ) {
            statValueTexts[ i ].text = statValues[ i ].Value.ToString();
        }

        if ( otherCharacterID.Value >= 0 ) {
            otherCharacter = allCharacters.Items[ otherCharacterID.Value ];
            InitializeOtherCharacterUI();
        }
        UpdateActionUI( null );
    }

    public void UpdateActionUI( Action action ) {
        for ( int i = 0; i < actionList.Items.Count; i++ ) {
            moveNamesText[ i ].text = actionList.Items[ i ].moveName;
        }
        for ( int i = 0; i < actionList.Items.Count; i++ ) {
            moveUsesText[ i ].text = moveUses[ i ].Value.ToString() + " / " + actionList.Items[ i ].moveUses.ToString();
        }
    }

    public void InitializeOtherCharacterUI() {
        otherCharNameText.text = otherCharacter.charName;
        otherCharLevelText.text = "Lvl. " + otherCharacter.charLevel.ToString();
        otherCharImage.sprite = otherCharacter.charFrontSprite;
    }

    public void OtherCharacterIDChanged( int newValue ) {
        otherCharacter = allCharacters.Items[ newValue ];
        InitializeOtherCharacterUI();
    }

    public void OnMoveUsesChange( int newValue ) {
        for ( int i = 0; i < moveUsesText.Length; i++ ) {
            moveUsesText[ i ].text = moveUses[ i ].ToString() + " / " + thisCharacter.moveSet[ i ].moveUses.ToString();
        }
    }

    public void OnHPChange( int newValue ) {
        charHP.text = hp.Value + " / " + thisCharacter.charHP;
    }
}
