using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkBehaviourInstantiator : MonoBehaviour {

    public GameObject clientBehaviour;
    public GameObject serverBehaviour;
    public BoolValue isHost;

    public void InstantiateHostBehaviour() {
        Instantiate( clientBehaviour );
        Instantiate( serverBehaviour );
        isHost.Value = true;
    }

    public void InstantiateClientBehaviour() {
        Instantiate( clientBehaviour );
    }
}
