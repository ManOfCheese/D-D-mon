using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntUpdateEventSender : MonoBehaviour {

    public NetworkEvent_RunTimeSet eventQueue;
    public IntValue[] intValues;
    public NetworkEvent networkEvent;

    private void OnEnable() {
        for ( int i = 0; i < intValues.Length; i++ ) {
            intValues[ i ].onValueChanged += AddEventToQueue;
        }
    }

    public void AddEventToQueue( int newValue ) {
        eventQueue.Add( networkEvent );
    }
}
