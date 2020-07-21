using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntValueUpdater : MonoBehaviour {

    public IntValue intValue;
    public NetworkEvent networkEventToSend;
    public NetworkEvents_RunTimeSet serverToClientRunTimeSet;

    private void OnEnable() {
        intValue.onValueChanged += SendUpdateEvent;
    }

    private void OnDisable() {
        intValue.onValueChanged -= SendUpdateEvent;
    }

    public void SendUpdateEvent( int value ) {
        serverToClientRunTimeSet.Add( networkEventToSend );
    }
}
