using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableScrip : MonoBehaviour {

    public string idSender = "";
    public string idReceiver = "";
    public State senderState = State.NONE;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void RemoveCable()
    {
        Debug.Log("Cable removed");
        idSender = "";
        idReceiver = "";
        senderState = State.NONE;
        //Remove graphically
    }
}
