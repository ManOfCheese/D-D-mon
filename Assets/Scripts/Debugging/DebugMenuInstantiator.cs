using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenuInstantiator : MonoBehaviour {

	public GameObject boolToggle;
	public GameObject floatSlider;
	public GameObject intSlider;
    public GameObject boolDisplayer;
    public GameObject floatDisplayer;
    public GameObject intDisplayer;
    public GameObject stringDisplayer;

    public DebugSettingsCollection settingsCollection;

	private void Awake() {
		for ( int i = 0; i < settingsCollection.boolSettings.Count; i++ ) {
			BoolToggle newBoolButton = Instantiate( boolToggle, this.transform ).GetComponent<BoolToggle>();
			newBoolButton.boolValue = settingsCollection.boolSettings[ i ];
			newBoolButton.Initialize();
		}
		for ( int i = 0; i < settingsCollection.floatSettings.Count; i++ ) {
			FloatSlider newFloatSlider = Instantiate( floatSlider, this.transform ).GetComponent<FloatSlider>();
			newFloatSlider.floatValue = settingsCollection.floatSettings[ i ];
			newFloatSlider.Initialize();
		}
		for ( int i = 0; i < settingsCollection.intSettings.Count; i++ ) {
			IntSlider newRoomPartDisplayer = Instantiate( intSlider, this.transform ).GetComponent<IntSlider>();
			newRoomPartDisplayer.intValue = settingsCollection.intSettings[ i ];
			newRoomPartDisplayer.Initialize();
		}
        for ( int i = 0; i < settingsCollection.boolSettingsReadOnly.Count; i++ ) {
            BoolToggle newBoolDisplayer = Instantiate( boolDisplayer, this.transform ).GetComponent<BoolToggle>();
            newBoolDisplayer.boolValue = settingsCollection.boolSettingsReadOnly[ i ];
            newBoolDisplayer.Initialize();
        }
        for ( int i = 0; i < settingsCollection.floatSettingsReadOnly.Count; i++ ) {
            FloatDisplay newFloatDisplayer = Instantiate( floatDisplayer, this.transform ).GetComponent<FloatDisplay>();
            newFloatDisplayer.floatValue = settingsCollection.floatSettingsReadOnly[ i ];
            newFloatDisplayer.Initialize();
        }
        for ( int i = 0; i < settingsCollection.intSettingsReadOnly.Count; i++ ) {
            IntDisplay newIntDisplayer = Instantiate( intDisplayer, this.transform ).GetComponent<IntDisplay>();
            newIntDisplayer.intValue = settingsCollection.intSettingsReadOnly[ i ];
            newIntDisplayer.Initialize();
        }
        for ( int i = 0; i < settingsCollection.stringSettingsReadOnly.Count; i++ ) {
            StringDisplay newStringDisplayer = Instantiate( stringDisplayer, this.transform ).GetComponent<StringDisplay>();
            newStringDisplayer.stringValue = settingsCollection.stringSettingsReadOnly[ i ];
            newStringDisplayer.Initialize();
        }
    }

}
