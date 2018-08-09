using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable_Simple_End : MonoBehaviour {

    // Use this for initialization
    public string holeId;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void deleteEnd()
    {
        GameObject[] tab = GameObject.FindGameObjectsWithTag("Box");
        for (var i = 0; i < tab.Length; ++i)
        {
            if (tab[i].GetComponent<BoxScript>().boxInfos.idName == holeId)
            {
                if (!tab[i].GetComponent<BoxScript>().isCalling)
                {
                    tab[i].GetComponent<BoxScript>().RemoveCable();
                    tab[i].GetComponent<BoxScript>().hole.GetComponent<HoleScript>().is_taken = false;
                    Destroy(gameObject);
                    //gameObject.transform.position = new Vector3(100, 100, 0);

                }
            }
        }
    }
}
