using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGMath {
    public class StatMath : MonoBehaviour {
        public static int ModifierFromStatValue( int statValue ) {
            return Mathf.FloorToInt( ( statValue - 10 ) / 2 );
        }
    }
}
