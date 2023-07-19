using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down : MonoBehaviour {

    public float minSpeed, maxSpeed;
    float speed;

    private void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update () {
        transform.Translate(0, speed * Time.deltaTime * -1, 0);
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Bottom")
        {
            Destroy(this.gameObject);
        }
    }
}
