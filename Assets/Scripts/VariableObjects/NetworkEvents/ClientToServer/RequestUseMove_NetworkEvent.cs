using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;

[CreateAssetMenu( fileName = "RequestUseMoveEvent", menuName = "NetworkEvents/RequestUseMoveEvent" )]
public class RequestUseMove_NetworkEvent : NetworkEvent {

    [Header( "References" )]
    public Action_Set allActions;

    [Header( "Client Side" )]
    public IntValue playerID;
    public IntValue_RunTimeSet queuedActionIDs;

    [Header( "Server Side" )]
    public IntValue whoseTurn;
    public Action_RunTimeSet queuedActions;

    public override DataStreamWriter WritePacket( DataStreamWriter writer ) {
        writer.WriteInt( ID );
        writer.WriteInt( playerID.Value );
        writer.WriteInt( queuedActionIDs.Items.Count );
        for ( int i = 0; i < queuedActionIDs.Items.Count; i++ ) {
            writer.WriteInt( queuedActionIDs.Items[ i ].Value );
        }
        return writer;
    }

    public override void ReadPacket( DataStreamReader stream ) {
        int attackingPlayerID = stream.ReadInt();

        if ( attackingPlayerID == whoseTurn.Value ) {
            int actionCount = stream.ReadInt();
            for ( int i = 0; i < actionCount; i++ ) {
                int actionIndex = stream.ReadInt();
                queuedActions.Add( allActions.Items[ actionIndex ] );
            }
        }
    }

}
