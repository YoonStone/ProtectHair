using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimit : MonoBehaviour {
    public Image timeLimit;
    public float waitTime = 5.0f;

    private void Start()
    {
        Destroy(this.gameObject, waitTime);
        
    }

    void Update()
    {
        timeLimit.fillAmount += 1.0f / waitTime * Time.deltaTime;
    }
}
