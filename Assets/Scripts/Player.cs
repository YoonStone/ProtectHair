using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //닿았을 때
    private void OnTriggerEnter2D(Collider2D coll)
    {
        switch (coll.gameObject.tag)
        {
            case "Blue":
            case "Black":
            case "Yellow":
            case "Spark":
                Destroy(coll.gameObject, 0.1f);
                this.gameObject.SendMessage("Damage", coll.gameObject.tag);
                break;
            case "Umbrella_o":
            case "Umbrella_x":
            case "Hamburger":
            case "Almond":
            case "Balmo":
            case "Soju":
                Destroy(coll.gameObject, 0.1f);
                this.gameObject.SendMessage("Recovery", coll.gameObject.tag);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Bottom")
            this.gameObject.SendMessage("Ground");

    }
}
