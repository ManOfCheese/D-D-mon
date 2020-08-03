using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Unity.Networking.Transport;
using Unity.Collections;
using System.Linq;

public class ServerBehaviour : MonoBehaviour {

    [Header( "References" )]
    public NetworkEvent_Set allEvents;
    public NetworkEvent_RunTimeSet eventsToSend;
    public IntValue connectionCount;

    [Header( "Events" )]
    public AssignPlayerID_NetworkEvent assignPlayerIDEvent;
    public SwitchTurn_NetworkEvent switchTurnEvent;
    public IntValue playerID;
    public IntValue assignedPlayerID;

    [HideInInspector] public NetworkDriver driver;
    public NativeList<NetworkConnection> connections;

    void Start() {
        DontDestroyOnLoad( this );

        allEvents.AssignIDs();
        driver = NetworkDriver.Create();
        var endpoint = NetworkEndPoint.AnyIpv4;
        endpoint.Port = 9000;
        if ( driver.Bind( endpoint ) != 0 ) {
            Debug.Log( "Server | Failed to bind to port 9000" );
        }
        else {
            driver.Listen();
        }

        connections = new NativeList<NetworkConnection>( 16, Allocator.Persistent );
    }

    private void OnDestroy() {
        driver.Dispose();
        connections.Dispose();
    }

    void Update() {
        driver.ScheduleUpdate().Complete();

        //Clean up connections
        for ( int i = 0; i < connections.Length; i++ ) {
            if ( !connections[ i ].IsCreated ) {
                connections.RemoveAtSwapBack( i );
                Debug.Log( "Removed connection" );
                --i;
                connectionCount.Value -= 1;
            }
        }

        //Accept new connections
        NetworkConnection connection;
        while ( ( connection = driver.Accept() ) != default( NetworkConnection ) ) {
            connections.Add( connection );
            connectionCount.Value += 1;

            if ( connections.Length == 1 ) {
                assignedPlayerID.Value = 0;
            }
            else if ( connections.Length > 1 && playerID.Value == 1 ) {
                assignedPlayerID.Value = 0;
            }
            else if ( connections.Length > 1 && playerID.Value == 0 ) {
                assignedPlayerID.Value = 1;
            }
            Debug.Log( "Server | Accepted a connection assigned playerID " + assignedPlayerID.Value + " - " + connections.Length +
                " connections." );

            var playerIDWriter = driver.BeginSend( NetworkPipeline.Null, connections[ connections.Length - 1 ] );
            playerIDWriter = assignPlayerIDEvent.WritePacket( playerIDWriter );
            driver.EndSend( playerIDWriter );

            var switchTurnWriter = driver.BeginSend( NetworkPipeline.Null, connections[ connections.Length - 1 ] );
            switchTurnWriter = switchTurnEvent.WritePacket( switchTurnWriter );
            driver.EndSend( switchTurnWriter );
        }

        DataStreamReader stream;
        for ( int i = 0; i < connections.Length; i++ ) {
            //if ( !connections[ i ].IsCreated ) {
            //    Debug.Log( "Ja dat is kut" );
            //    continue;
            //}

            Unity.Networking.Transport.NetworkEvent.Type eventType;
            while ( ( eventType = driver.PopEventForConnection( connections[ i ], out stream ) ) !=
                Unity.Networking.Transport.NetworkEvent.Type.Empty ) {
                if ( eventType == Unity.Networking.Transport.NetworkEvent.Type.Data ) {
                    int packetID = stream.ReadInt();
                    Debug.Log( "Server | Recieved packet: " + allEvents.Items[ packetID ].displayName + " | Connections: " + i +
                        "-" + ( connections.Length - 1 ) );

                    //Handle recieved events.
                    allEvents.Items[ packetID ].ReadPacket( stream );
                }
                else if ( eventType == Unity.Networking.Transport.NetworkEvent.Type.Disconnect ) {
                    Debug.Log( "Server | Client disconnected from server" + " | Connections: " + i + "-" + ( connections.Length - 1 ) );
                    connections[ i ] = default( NetworkConnection );
                }
            }
        }

        for ( int i = 0; i < connections.Length; i++ ) {
            //Send queued events.
            for ( int j = 0; j < eventsToSend.Items.Count; j++ ) {
                Debug.Log( "Server | Sending packet: " + eventsToSend.Items[ j ].displayName + " | Connections: " + i + "-"
                    + ( connections.Length - 1 ) );
                var writer = driver.BeginSend( NetworkPipeline.Null, connections[ i ] );
                writer = eventsToSend.Items[ j ].WritePacket( writer );
                driver.EndSend( writer );
            }
        }
        eventsToSend.Items.Clear();
    }
}
