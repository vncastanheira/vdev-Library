using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vnc.Attributes;
using vnc.Utilities;

public class RangeSlider : MonoBehaviour {

    [MinMaxRange(5f, 10f)]
    public MinMaxRange speed;
    
	void Update () {
		
	}
}
