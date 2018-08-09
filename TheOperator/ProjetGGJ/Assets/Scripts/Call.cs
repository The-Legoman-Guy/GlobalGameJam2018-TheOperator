using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Call", menuName = "Call")]
public class Call : ScriptableObject {

    public string info_sender;
    public string id_sender;
    public string id_receiver;
    public float call_duration;
}
