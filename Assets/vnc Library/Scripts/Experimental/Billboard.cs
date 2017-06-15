using System;
using UnityEngine;

namespace vnc.Experimental
{
    [Serializable]
    public class Billboard : MonoBehaviour
    {
	    public void Update()
	    {
		    if (Camera.main != null)
		    {
			    Quaternion rotation = Camera.main.transform.rotation;
			    transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
		    }
	    }
    }
}

