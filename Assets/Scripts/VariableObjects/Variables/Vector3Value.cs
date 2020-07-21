using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Vector3Value", menuName = "Variables/Vector3Value" )]
public class Vector3Value : VariableObject {

	[Header( "Value" )]
	[SerializeField] private Vector3 _value;
	public Vector3 Value {
		get {
			return _value;
		}
		set {
			if ( _value != value ) {
				if ( useLimits ) {
					_value = new Vector3( Mathf.Clamp( value.x, minValue.x, maxValue.x ), Mathf.Clamp( value.y, minValue.y, maxValue.y ),
                        Mathf.Clamp( value.z, minValue.z, maxValue.z ) );
				}
				else {
					_value = value;
				}
				_value = value;
				changedThisFrame = true;
				onValueChanged?.Invoke( _value );
			}
		}
	}

	[Space( 10 )]
	public Vector3 defaultValue;
    public bool useLimits;
    public Vector3 minValue;
	public Vector3 maxValue;

	public delegate void OnValueChanged( Vector3 value );
	public OnValueChanged onValueChanged;

	public override void ResetToDefault() {
		_value = defaultValue;
		onValueChanged?.Invoke( _value );
	}

	protected override void OnEnable() {
		base.OnEnable();
	}

}
