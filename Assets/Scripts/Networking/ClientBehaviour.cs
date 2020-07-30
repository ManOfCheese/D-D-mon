using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Unity.Networking.Transport;
using Unity.Collections;
using UnityEngine.SceneManagement;

public class ClientBehaviour : MonoBehaviour {

    [Header( "References" )]
    public NetworkEvent_Set allEvents;
    public NetworkEvents_RunTimeSet eventsToSend;
    public StringValue lastRecievedPacket;
    public BoolValue isConnected;
    public int sceneToLoadOnDisconnect;

    public NetworkDriver driver;
    public NetworkConnection connection;

    public static bool initialized;

    void Start() {
        DontDestroyOnLoad( this );

        allEvents.AssignIDs();
        driver = NetworkDriver.Create();
        connection = default( NetworkConnection );

        var endpoint = NetworkEndPoint.LoopbackIpv4;
        endpoint.Port = 9000;
        connection = driver.Connect( endpoint );
        initialized = true;
    }

    private void OnDestroy() {
        driver.Dispose();
    }

    void Update() {
        driver.ScheduleUpdate().Complete();

        if ( !connection.IsCreated ) {
            //Debug.Log( "Client | Something went wrong during connect" );
            return;
        }

        DataStreamReader stream;
        Unity.Networking.Transport.NetworkEvent.Type cmd;
        while ( ( cmd = connection.PopEvent( driver, out stream ) ) != Unity.Networking.Transport.NetworkEvent.Type.Empty ) {
            if ( cmd == Unity.Networking.Transport.NetworkEvent.Type.Connect ) {
                isConnected.Value = true;
                Debug.Log( "Client | We are now connected to the server" );
            }
            else if ( cmd == Unity.Networking.Transport.NetworkEvent.Type.Data ) {
                int packetID = stream.ReadInt();
                Debug.Log( "Client | Recieved packet: " + allEvents.Items[ packetID ].displayName );

                //Handle recieved events.
                allEvents.Items[ packetID ].ReadPacket( stream );
                lastRecievedPacket.Value = allEvents.Items[ packetID ].displayName;
            }
            else if ( cmd == Unity.Networking.Transport.NetworkEvent.Type.Disconnect ) {
                Debug.Log( "Client | Got disconnected from server" );
                isConnected.Value = false;
                SceneManager.LoadSceneAsync( sceneToLoadOnDisconnect );
                if ( GameObject.Find( "ClientBehaviour" ) ) {
                    Destroy( GameObject.Find( "ClientBehaviour" ) );
                }
                if ( GameObject.Find( "HostBehaviour" ) ) {
                    Destroy( GameObject.Find( "HostBehaviour" ) );
                }
                connection = default( NetworkConnection );
            }
        }
        //Send queued events.
        for ( int i = 0; i < eventsToSend.Items.Count; i++ ) {
            Debug.Log( "Client | Sending packet: " + eventsToSend.Items[ i ].displayName );
            var writer = driver.BeginSend( NetworkPipeline.Null, connection );
            writer = eventsToSend.Items[ i ].WritePacket( writer );
            driver.EndSend( writer );
        }
        eventsToSend.Items.Clear();
    }
}
