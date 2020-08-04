using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;

[CreateAssetMenu( fileName = "WorldStateUpdateEvent", menuName = "NetworkEvents/WorldStateUpdateEvent" )]
public class WorldStateUpdate_NetworkEvent : NetworkEvent {

    [Header( "Client Side" )]
    public IntValue clientWhoseTurn;
    public IntValue playerID;
    public IntValue_Set variablesToUpdate;

    [Header( "Server Side" )]
    public IntValue serverWhoseTurn;
    public IntValue_Set P0WorldStateVariables;
    public IntValue_Set P1WorldStateVariables;
    public IntValue_Set OtherWorldStateVariables;

    public override DataStreamWriter WritePacket( DataStreamWriter writer ) {
        writer.WriteInt( ID );
        for ( int i = 0; i < P0WorldStateVariables.Items.Count; i++ ) {
            writer.WriteInt( P0WorldStateVariables.Items[ i ].Value );
        }
        for ( int i = 0; i < P1WorldStateVariables.Items.Count; i++ ) {
            writer.WriteInt( P1WorldStateVariables.Items[ i ].Value );
        }
        for ( int i = 0; i < OtherWorldStateVariables.Items.Count; i++ ) {
            writer.WriteInt( OtherWorldStateVariables.Items[ i ].Value );
        }
        return writer;
    }

    public override void ReadPacket( DataStreamReader stream ) {
        if ( playerID.Value == 0 ) {
            int i = 0;

            for ( int j = 0; j < P0WorldStateVariables.Items.Count; j++ ) {
                variablesToUpdate.Items[ i ].Value = stream.ReadInt();
                i++;
            }
            for ( int j = 0; j < P1WorldStateVariables.Items.Count; j++ ) {
                variablesToUpdate.Items[ i ].Value = stream.ReadInt();
                i++;
            }
            for ( int j = 0; j < OtherWorldStateVariables.Items.Count; j++ ) {
                variablesToUpdate.Items[ i ].Value = stream.ReadInt();
                i++;
            }
        }
        else if ( playerID.Value == 1 ) {
            int i = P0WorldStateVariables.Items.Count;

            for ( int j = 0; j < P1WorldStateVariables.Items.Count; j++ ) {
                variablesToUpdate.Items[ i ].Value = stream.ReadInt();
                i++;
            }
            i -= P0WorldStateVariables.Items.Count + P1WorldStateVariables.Items.Count;
            for ( int j = 0; j < P0WorldStateVariables.Items.Count; j++ ) {
                variablesToUpdate.Items[ i ].Value = stream.ReadInt();
                i++;
            }
            i += P0WorldStateVariables.Items.Count + P1WorldStateVariables.Items.Count;
            for ( int j = 0; j < OtherWorldStateVariables.Items.Count; j++ ) {
                variablesToUpdate.Items[ i ].Value = stream.ReadInt();
                i++;
            }
        }
    }

}
