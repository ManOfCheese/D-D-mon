using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSender : MonoBehaviour {

    public NetworkEvent_RunTimeSet eventQueue;
    public NetworkEvent networkEvent;
    public bool sendOnStart;

    private void Start() {
        if ( sendOnStart ) {
            AddEventToQueue();
        }
    }

    public void AddEventToQueue() {
        eventQueue.Add( networkEvent );
    }
}
