using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSender : MonoBehaviour {

    public NetworkEvent_RunTimeSet eventQueue;
    public NetworkEvent networkEvent;

    public void AddEventToQueue() {
        eventQueue.Add( networkEvent );
    }
}
