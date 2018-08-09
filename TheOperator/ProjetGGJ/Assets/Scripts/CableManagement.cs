using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableManagement : MonoBehaviour
{
    public bool is_set;
    GameObject current_cable;
    GameObject current_cable_fin;

    public string idSender = "";
    public string idReceiver = "";
    public State senderState = State.NONE;

    // Use this for initialization
    void Start()
    {
        current_cable = GameObject.Find("Cable_Simple_Start");
        current_cable = Instantiate(current_cable);
        current_cable_fin = GameObject.Find("Cable_Simple_End");
        current_cable_fin = Instantiate(current_cable_fin);
        is_set = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCable();
    }

    void UpdateCable()
    {

        if (!is_set)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = current_cable_fin.transform.position.z - Camera.main.transform.position.z;
            current_cable_fin.transform.position = Camera.main.ScreenToWorldPoint(pos);
            //Debug.Log("Je passe par la");
        }
    }

    public void getCable(GameObject start, GameObject end)
    {   
        is_set = false;
        current_cable = start;
        current_cable_fin = end;
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

    public void creteNewCable()
    {
        if (is_set == false)
        {
            is_set = true;
        }
        else if (is_set == true)
        {
            is_set = false;
            current_cable = GameObject.Find("Cable_Simple_Start");
            current_cable = Instantiate(current_cable);
            current_cable_fin = GameObject.Find("Cable_Simple_End");
            current_cable_fin = Instantiate(current_cable_fin);
            current_cable.GetComponent<Cable_Procedural_Simple>().setEndPoint(current_cable_fin);
            Vector3 pos = Input.mousePosition;
            pos.z = current_cable_fin.transform.position.z - Camera.main.transform.position.z;
            current_cable.transform.position = Camera.main.ScreenToWorldPoint(pos);
            Debug.Log(pos);
        }
    }

    public void detachCabe(GameObject start, GameObject end)
    {
        is_set = false;
        current_cable = start;
        current_cable_fin    = end;
    }

    public GameObject getCableStart()
    {
        return (current_cable);
    }

    public GameObject getCableEnd()
    {
        return (current_cable_fin);
    }

    public bool getIsSet()
    {
        return (is_set);
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
    