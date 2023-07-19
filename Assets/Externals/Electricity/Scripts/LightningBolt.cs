using UnityEngine;
using System.Collections;

public class LightningBolt : MonoBehaviour
{
	void Start ()
	{
        Renderer renderer = GetComponent<Renderer>();
		Material newMat = renderer.material;
		newMat.SetFloat("_StartSeed",Random.value* 1000);
		renderer.material = newMat;
		
	}
}

