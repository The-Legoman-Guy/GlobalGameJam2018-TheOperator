using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : MonoBehaviour {

    GameObject cable_start;
    GameObject cable_end;
    GameObject manager;
    public bool is_taken;
    // Use this for initialization
    void Start () {
        is_taken = false;
        manager = GameObject.Find("CableManager");
    }

    // Update is called once per frame
    void Update () {

    }

    public void DeleteCable()
    {
        is_taken = false;
        if (cable_start.GetComponent<Cable_Procedural_Simple>())
            cable_start.GetComponent<Cable_Procedural_Simple>().DeleteCable();
        else if (cable_end.GetComponent<Cable_Procedural_Simple>())
            cable_end.GetComponent<Cable_Procedural_Simple>().DeleteCable();
    }
    void OnMouseDown()
    {
        Debug.Log("ON MOUSE DOWN");
        bool ret = transform.GetComponentInParent<BoxScript>().GetClicked();

        if (is_taken == false)
        {
            //add cable
            bool is_set = manager.GetComponent<CableManagement>().getIsSet();
            if (ret)
            {
                is_taken = true;
                manager.GetComponent<CableManagement>().creteNewCable();
            }
            if (is_set == false)
            {
                cable_start = manager.GetComponent<CableManagement>().getCableStart();
                cable_end = manager.GetComponent<CableManagement>().getCableEnd();
                cable_end.GetComponent<Cable_Simple_End>().holeId = transform.parent.gameObject.GetComponent<BoxScript>().boxInfos.idName;
            }
            else if (is_set == true)
            {
                cable_end = manager.GetComponent<CableManagement>().getCableStart();
                cable_end.GetComponent<Cable_Procedural_Simple>().holeId = transform.parent.gameObject.GetComponent<BoxScript>().boxInfos.idName;
                cable_start = manager.GetComponent<CableManagement>().getCableEnd();
            }
        }
    }
}
