using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "DebugSettingsCollection", menuName = "Debugging/DebugSettingsCollection" )]
public class DebugSettingsCollection : PersistentSetElement {

	public List<BoolValue> boolSettings = new List<BoolValue>();
	public List<FloatValue> floatSettings = new List<FloatValue>();
    public List<IntValue> intSettings = new List<IntValue>();

    public List<BoolValue> boolSettingsReadOnly = new List<BoolValue>();
    public List<FloatValue> floatSettingsReadOnly = new List<FloatValue>();
    public List<IntValue> intSettingsReadOnly = new List<IntValue>();
    public List<StringValue> stringSettingsReadOnly = new List<StringValue>();
}
