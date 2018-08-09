using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxNameScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        string description = transform.parent.gameObject.GetComponent<BoxScript>().boxInfos.description;
        Sprite sprite = transform.parent.gameObject.GetComponent<BoxScript>().boxInfos.picture;
        GameObject.Find("LCDText").GetComponent<Text>().text = description;
        GameObject.Find("SpriteOnLCD").GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void OnMouseExit()
    {
        GameObject.Find("LCDText").GetComponent<Text>().text = "";
        GameObject.Find("SpriteOnLCD").GetComponent<SpriteRenderer>().sprite = null;
    }
}
