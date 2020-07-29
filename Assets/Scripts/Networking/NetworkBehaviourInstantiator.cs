using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkBehaviourInstantiator : MonoBehaviour {

    public GameObject clientBehaviour;
    public GameObject hostBehaviour;

    public void InstantiateHostBehaviour() {
        Instantiate( hostBehaviour );
    }

    public void InstantiateClientBehaviour() {
        Instantiate( clientBehaviour );
    }
}
