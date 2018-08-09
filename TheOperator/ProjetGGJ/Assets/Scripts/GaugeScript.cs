using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeScript : MonoBehaviour {

    static float minAngle = -80.0f;
    static float maxAngle = 80.0f;
    public float angle = .0f;
    static GaugeScript thisGauge;
    public float angleValue = 0.5f;

	void Start () {
        thisGauge = this;
        AdjustAngle(angleValue);
    }
	
    void Update()
    {
        float ang = Mathf.Lerp(minAngle, maxAngle, Mathf.InverseLerp(0, 100, angle));
        thisGauge.transform.eulerAngles = new Vector3(0, 0, ang);
    }

    public void ModifyAngleValue(float toAdd)
    {
        angleValue += toAdd;
        AdjustAngle(angleValue);
    }
    public void AdjustAngle(float newAngle)
    {
        angle = (1 - newAngle) * 100;
    }



	
}
