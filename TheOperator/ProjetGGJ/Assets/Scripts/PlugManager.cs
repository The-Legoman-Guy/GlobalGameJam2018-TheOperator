using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugManager : MonoBehaviour {

    GameObject  cable_start;
    GameObject  cable_end;
    GameObject manager;
    bool        is_taken;

	// Use this for initialization
	void Start () {
        is_taken = false;
        manager = GameObject.Find("CableManager");
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnMouseDown()
    {
        if (is_taken == false)
        {
            //add cable
            bool is_set = manager.GetComponent<CableManagement>().getIsSet();
            is_taken = true;
            manager.GetComponent<CableManagement>().creteNewCable();
            if (is_set == false)
            {
                cable_start = manager.GetComponent<CableManagement>().getCableStart();
                cable_end = manager.GetComponent<CableManagement>().getCableEnd();
            }
            else if (is_set == true)
            {
                cable_end = manager.GetComponent<CableManagement>().getCableStart();
                cable_start = manager.GetComponent<CableManagement>().getCableEnd();
            }
        }
        else if (is_taken == true)
        {
            //detach cable
            bool is_set = manager.GetComponent<CableManagement>().getIsSet();
            if (is_set == true)
            {
                is_taken = false;
                manager.GetComponent<CableManagement>().detachCabe(cable_start, cable_end);
            }
        }
    }
}
