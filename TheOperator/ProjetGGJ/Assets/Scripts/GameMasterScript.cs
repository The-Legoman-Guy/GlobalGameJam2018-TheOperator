using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {PASSIVE, SENDER, RECEIVER, NONE};

public class GameMasterScript : MonoBehaviour {

    public GameObject actualCable = null;
    public List<Call> calls = new List<Call>();
    public float delayToLaunchCall = 10.0f;

    private float timer = 0.0f;
    private System.Random rnd = new System.Random();

	// Use this for initialization
	void Start () {
   
	}
	
    private void LaunchCall(Call callToLaunch)
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        GameObject sender = null;
        GameObject receiver = null;
        for (var i = 0; i < boxes.Length; ++i)
        {
            if (boxes[i].GetComponent<BoxScript>().boxInfos.idName == callToLaunch.id_sender)
            {
                sender = boxes[i];
            }
            else if (boxes[i].GetComponent<BoxScript>().boxInfos.idName == callToLaunch.id_receiver)
            {
                receiver = boxes[i];
            }
        }
        if (!sender|| !receiver)
        {
            if (!sender)
                Debug.Log("Sender not found !");
            else if (!receiver)
                Debug.Log("Receiver not found !");
            else
                Debug.Log("Wat ?");
        }
        else
        {
            sender.GetComponent<BoxScript>().informationsOfCall = callToLaunch.info_sender;
            sender.GetComponent<BoxScript>().ChangeState(State.SENDER);
            sender.GetComponent<BoxScript>().timeOfCall = callToLaunch.call_duration;

            receiver.GetComponent<BoxScript>().ChangeState(State.RECEIVER);
            receiver.GetComponent<BoxScript>().AddToSenderList(callToLaunch.id_sender);
            receiver.GetComponent<BoxScript>().timeOfCall = callToLaunch.call_duration;
            Debug.Log("Sender and Receiver correctly affected");
        }
    }

	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= delayToLaunchCall)
        {
            if (calls.Count > 0)
            {
                int index = rnd.Next(0, calls.Count);
                Debug.Log("Launching call n°" + index);
                LaunchCall(calls[index]);
                calls.Remove(calls[index]);
                timer = 0.0f;
            }
        }
	}
}
