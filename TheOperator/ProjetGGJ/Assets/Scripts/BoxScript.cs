using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoxScript : MonoBehaviour {

    private GameObject led;
    public GameObject hole;
    private GameObject boxName;
    private State actualState = State.PASSIVE;
    private bool hasACable = false;
    private float cntTime = 0.0f;
    private System.Random rnd = new System.Random();
    public float timeOfCall = 0.0f;
    private List<string> senderList = new List<string>();
    private float cntTimeDeleteCable = 0.0f;
    private bool mustCountdownDelete = false;
    private GameObject soundManager;
    private string[] voiceTab = new string[5];
    private float cntTimeDeleteCall = 0.0f;

    public string informationsOfCall; //a afficher dans le rectangle à gauche
    public BoxInfos boxInfos; //bosinfos.infos à afficher dans le rectangle en haut quand survolé
    public bool isCalling = false;
    public float timeOfDispearingCall = 20.0f;

    private float tmpTime = 0.0f;
	// Use this for initialization
	void Start () {
        led = gameObject.transform.GetChild(0).gameObject;
        hole = gameObject.transform.GetChild(1).gameObject;
        boxName = gameObject.transform.GetChild(2).gameObject;
        GameObject.Find("GaugeSon").GetComponent<GaugeScript>().AdjustAngle(5.0f);
        soundManager = GameObject.Find("SoundManager");
        voiceTab[0] = "aigu";
        voiceTab[1] = "grave";
        voiceTab[2] = "moyen";
        voiceTab[3] = "moyen_aigu";
        voiceTab[4] = "moyen_grave";
        cntTime = 0.0f;
    }

    public void AddToSenderList(string toAdd)
    {
        senderList.Add(toAdd);
    }
    private GameObject SearchCable(string name)
    {
        GameObject[] cables = GameObject.FindGameObjectsWithTag("Cable");
        for (var i = 0; i < cables.Length; ++i)
        {
            if (cables[i].GetComponent<CableManagement>().idSender == name)
                return (cables[i]);
        }
        return (null);
    }

    private GameObject SearchBox(string name)
    {
        GameObject[] cables = GameObject.FindGameObjectsWithTag("Box");
        for (var i = 0; i < cables.Length; ++i)
        {
            if (cables[i].GetComponent<BoxScript>().boxInfos.idName == name)
                return (cables[i]);
        }
        return (null);
    }

    public void RemoveCable()
    {
        isCalling = false;
        hasACable = false;
        led.GetComponent<LedScript>().AdjustUsed(false);
        if (actualState == State.SENDER)
        {
            GameObject cableToRemove = SearchCable(boxInfos.idName);
            if (cableToRemove != null)
            {
                cableToRemove.GetComponent<CableManagement>().RemoveCable();
                hole.GetComponent<HoleScript>().DeleteCable();
            }
            else
                Debug.Log("CABLE TO REMOVE NOT FOUND");
        }
        actualState = State.PASSIVE;
    }

    private void ModifyPoints(float value)
    {
        GameObject.Find("GaugeSon").GetComponent<GaugeScript>().ModifyAngleValue(value * -1);
    }
    // Update is called once per frame
    void Update () {
        if (GameObject.Find("GaugeSon").GetComponent<GaugeScript>().angleValue <= 0.1)
        {
            //WIN
            SceneManager.LoadScene("Winner");
        }
        if (GameObject.Find("GaugeSon").GetComponent<GaugeScript>().angleValue >= 0.9)
        {
            //LOSE
            SceneManager.LoadScene("Looser");
        }
        if (isCalling)
        {
            cntTimeDeleteCall += Time.deltaTime;
            if (cntTimeDeleteCall >= timeOfCall)
            {
                RemoveCable();
                cntTimeDeleteCall = 0.0f;
            }
        }
        else if (!isCalling && actualState == State.SENDER)
        {
            cntTime += Time.deltaTime;
            if (cntTime >= (timeOfDispearingCall / 2))
            {
                led.GetComponent<LedScript>().mustBlink = true;
            }
            if (cntTime >= timeOfDispearingCall)
            {
                isCalling = false;
                hasACable = false;
                led.GetComponent<LedScript>().AdjustUsed(false);
                led.GetComponent<LedScript>().mustBlink = false;
                actualState = State.PASSIVE;
                ModifyPoints(-0.1f);
                cntTime = 0.0f;
            }
        }
        if (!isCalling && mustCountdownDelete)
        {
            cntTimeDeleteCable += Time.deltaTime;
            if (cntTimeDeleteCable >= 5.0f)
            {
                cntTimeDeleteCable = 0.0f;
                mustCountdownDelete = false;
                hasACable = false;
                GameObject cableToRemove = SearchCable(boxInfos.idName);
                if (cableToRemove != null)
                {
                    cableToRemove.GetComponent<CableManagement>().RemoveCable();
                    hole.GetComponent<HoleScript>().DeleteCable();
                }
            }
        }
	}

    public void ChangeState(State newState)
    {
        actualState = newState;
        if (actualState == State.SENDER)
        {
            led.GetComponent<LedScript>().AdjustUsed(true);
        }
        else if (actualState == State.PASSIVE)
        {
            led.GetComponent<LedScript>().AdjustUsed(false);
        }
        GameObject cableToUpdate = SearchCable(boxInfos.idName);
        if (cableToUpdate != null)
        {
            if (newState == State.SENDER)
            {
                GameObject.Find("InformationText").GetComponent<Text>().text = ("Informations\n" + informationsOfCall);
            }
            cableToUpdate.GetComponent<CableManagement>().senderState = newState;
        }
    }

    public bool GetClicked()
    {
        GameObject actualCable = GameObject.Find("GameMaster").GetComponent<GameMasterScript>().actualCable;
        if (hasACable)
        {
            Debug.Log("This hole is not empty");
        }
        else if (!actualCable || actualCable.GetComponent<CableManagement>().senderState == State.NONE)
        {
            GameObject cable = SearchCable("");
            if (cable == null)
            {
                Debug.Log("NO CABLE AVAILABLE");
                return (false);
            }
            else
            {
                if (actualState == State.SENDER)
                {
                    GameObject.Find("InformationText").GetComponent<Text>().text = ("Informations\n" + informationsOfCall);
                }
                cable.GetComponent<CableManagement>().idSender = boxInfos.idName;
                cable.GetComponent<CableManagement>().senderState = actualState;
                GameObject.Find("GameMaster").GetComponent<GameMasterScript>().actualCable = cable;
                hasACable = true;
                Debug.Log("Cable correctly attached");
            }
        }
        else if (actualState == State.RECEIVER)
        {
            State cableSenderState = actualCable.GetComponent<CableManagement>().senderState;
            //Debug.Log("Clicked on a Receiver");
            hasACable = true;
            if (cableSenderState == State.SENDER && 
                senderList.Contains(actualCable.GetComponent<CableManagement>().idSender))
            {
                //Launch call
                Debug.Log("Correct ! Call launching");
                GameObject caller = SearchBox(actualCable.GetComponent<CableManagement>().idSender);
                caller.GetComponent<BoxScript>().led.GetComponent<LedScript>().mustBlink = false;
                caller.GetComponent<BoxScript>().isCalling = true;
                isCalling = true;
                GameObject.Find("GameMaster").GetComponent<GameMasterScript>().actualCable = null;
                //led.GetComponent<LedScript>().AdjustUsed(false);
                ModifyPoints(0.1f);
                GameObject.Find("InformationText").GetComponent<Text>().text = "Informations\n\nOK !";
                soundManager.GetComponent<SoundManagerScript>().PlaySound(voiceTab[rnd.Next(0, 5)]);
            }
            else if (cableSenderState == State.SENDER)
            {
                //error();
                GameObject caller = SearchBox(actualCable.GetComponent<CableManagement>().idSender);
                caller.GetComponent<BoxScript>().led.GetComponent<LedScript>().AdjustUsed(false);
                caller.GetComponent<BoxScript>().actualState = State.PASSIVE;
                caller.GetComponent<BoxScript>().isCalling = false;
                caller.GetComponent<BoxScript>().mustCountdownDelete = true;
                Debug.Log("ERROR TUTUT !");
                ModifyPoints(-0.1f);
                GameObject.Find("GameMaster").GetComponent<GameMasterScript>().actualCable = null;
                mustCountdownDelete = true;
                GameObject.Find("InformationText").GetComponent<Text>().text = "Informations\n\nKO !";
                soundManager.GetComponent<SoundManagerScript>().PlaySound("wrongBip");
            }
            else
            {
                Debug.Log("Attaching cable to Receiver but nothing happen");
                GameObject caller = SearchBox(actualCable.GetComponent<CableManagement>().idSender);
                caller.GetComponent<BoxScript>().mustCountdownDelete = true;
                GameObject.Find("GameMaster").GetComponent<GameMasterScript>().actualCable = null;
                mustCountdownDelete = true;
                GameObject.Find("InformationText").GetComponent<Text>().text = "Informations";
            }
        }
        else
        {
            State cableSenderState = actualCable.GetComponent<CableManagement>().senderState;
            //Debug.Log("Clicked on a Passive");
            hasACable = true;
            GameObject.Find("GameMaster").GetComponent<GameMasterScript>().actualCable = null;

            if (cableSenderState == State.SENDER)
            {
                //error();
                GameObject caller = SearchBox(actualCable.GetComponent<CableManagement>().idSender);
                caller.GetComponent<BoxScript>().led.GetComponent<LedScript>().AdjustUsed(false);
                caller.GetComponent<BoxScript>().actualState = State.PASSIVE;
                caller.GetComponent<BoxScript>().isCalling = false;
                caller.GetComponent<BoxScript>().mustCountdownDelete = true;
                Debug.Log("ERROR TUTUT !");
                ModifyPoints(-0.1f);
                mustCountdownDelete = true;
                GameObject.Find("InformationText").GetComponent<Text>().text = "Informations\n\nKO !";
                soundManager.GetComponent<SoundManagerScript>().PlaySound("wrongBip");
            }
            else
            {
                GameObject caller = SearchBox(actualCable.GetComponent<CableManagement>().idSender);
                caller.GetComponent<BoxScript>().mustCountdownDelete = true;
                mustCountdownDelete = true;
                Debug.Log("Attaching cable to Receiver but nothing happen");
                GameObject.Find("InformationText").GetComponent<Text>().text = "Informations";
            }
        }
        return (true);
    }
}
