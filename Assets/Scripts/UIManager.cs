using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    public Image hpBar;
    public Text[] moveNamesText;
    public Text[] moveUsesText;
    public Image[] statIcons;
    public Text[] statValueTexts;

    [Header( "OtherPlayer References" )]
    public IntValue otherHp;
    public IntValue otherCharacterID;

    [Space( 10 )]
    public Text otherCharNameText;
    public Text otherCharLevelText;
    public Image otherHpBar;
    public Image otherCharImage;

    private Character thisCharacter;
    private Character otherCharacter;
    private float hpBarStartWidth;
    private float otherHpBarStartWidth;

    private void Start() {
        InitializeUI();
    }

    public void OnEnable() {
        hp.onValueChanged += OnHPChange;
        otherHp.onValueChanged += OnOtherHPChange;
        otherCharacterID.onValueChanged += OtherCharacterIDChanged;
        actionList.OnAdded += UpdateActionUI;
    }

    public void OnDisable() {
        hp.onValueChanged -= OnHPChange;
        otherHp.onValueChanged -= OnOtherHPChange;
        otherCharacterID.onValueChanged -= OtherCharacterIDChanged;
        actionList.OnAdded -= UpdateActionUI;
    }

    public virtual void InitializeUI() {
        if ( characterID.Value > -1 ) {
            thisCharacter = allCharacters.Items[ characterID.Value ];
        }

        if ( thisCharacter != null ) {
            charNameText.text = thisCharacter.charName;
            charLevelText.text = "Lvl. " + thisCharacter.charLevel.ToString();
            charImage.sprite = thisCharacter.charBackSprite;
            charHP.text = hp.Value + " / " + thisCharacter.charHP;
        }

        hpBarStartWidth = hpBar.rectTransform.sizeDelta.x;
        otherHpBarStartWidth = otherHpBar.rectTransform.sizeDelta.x;

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
        if ( thisCharacter != null && charHP != null ) {
            charHP.text = hp.Value + " / " + thisCharacter.charHP;
            UpdateHPBar( false );
        }
    }

    public void OnOtherHPChange( int newValue ) {
        if ( otherCharacter != null ) {
            UpdateHPBar( true );
        }
    }

    public void UpdateHPBar( bool isOther ) {
        if ( isOther && otherHpBar != null ) {
            otherHpBar.rectTransform.sizeDelta = new Vector2( otherHpBarStartWidth * ( (float)otherHp.Value / (float)otherCharacter.charHP ),
                otherHpBar.rectTransform.sizeDelta.y );
        }
        else if ( hpBar != null ) {
            hpBar.rectTransform.sizeDelta = new Vector2( hpBarStartWidth * ( (float)hp.Value / (float)thisCharacter.charHP ),
                hpBar.rectTransform.sizeDelta.y );
        }
    }
}
