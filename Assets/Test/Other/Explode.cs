using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vnc.Effects;

public class Explode : MonoBehaviour
{
				public void FuckingExplode()
				{
								var meshexploder = GetComponent<MeshExplosion>();
								meshexploder.Explode(false, minForce: 5, maxForce: 10, radius: 10);
				}

}
