using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedScript : MonoBehaviour {

    bool isUsed = false;
    public bool mustBlink;

    private float cntSec = 0.0f;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (isUsed == false)
        {
            this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        {
            this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 0f, 0f, 1.0f);
        }
        if (mustBlink)
        {
            cntSec += Time.deltaTime;
            if (cntSec >= 0.5f)
            {
                cntSec = 0f;
                isUsed = !isUsed;
            }
        }
    }
    public void AdjustUsed(bool newUsed)
    {
        isUsed = newUsed;
    }
}
