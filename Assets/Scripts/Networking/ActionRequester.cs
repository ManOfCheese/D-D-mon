using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRequester : MonoBehaviour {

    public Player thisPlayer;
    public Action_Set allActions;
    public Int_RunTimeSet queuedActionIDs;
    public NetworkEvent_RunTimeSet clientToServerEventQueue;
    public NetworkEvent requestUseMoveEvent;

    private void Awake() {
        allActions.AssignIDs();
    }

    public void AddActionToQueue( int id ) {
        Action actionToQueue = thisPlayer.actions[ id ];
        queuedActionIDs.Add( actionToQueue.ID );
    }

    public void RequestUseMove() {
        clientToServerEventQueue.Add( requestUseMoveEvent );
    }
}
