using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Class", menuName = "PersistentData/Class" )]
public class Class : PersistentSetElement {

    public Class_Set allClasses;

    public string className;
    public Move_Set moveSet;


    private void Awake() {
        allClasses.Add( this );
    }
}
