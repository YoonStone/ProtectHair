using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountNumber : MonoBehaviour {

	void Start () {
        Destroy(this.gameObject, 1f);
    }

	void Update () {
        if(transform.localScale != new Vector3(0,0,0))
            transform.localScale += new Vector3(-0.01f, -0.01f, -0.01f);
        else
            Destroy(this.gameObject);
    }
}
