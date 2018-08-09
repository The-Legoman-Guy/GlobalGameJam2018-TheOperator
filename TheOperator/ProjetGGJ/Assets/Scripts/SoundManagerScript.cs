using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManagerScript : MonoBehaviour {

    public Dictionary<string, AudioClip> audios = new Dictionary<string, AudioClip>();

    float cntTime = 0.0f;
    AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        audios["aigu"] = Resources.Load<AudioClip>("Sounds/aigu");
        audios["berlin"] = Resources.Load<AudioClip>("Sounds/berlin");
        audios["Bip"] = Resources.Load<AudioClip>("Sounds/Bip");
        audios["Connect"] = Resources.Load<AudioClip>("Sounds/Connect");
        audios["decrocher"] = Resources.Load<AudioClip>("Sounds/decrocher");
        audios["finDeJournée"] = Resources.Load<AudioClip>("Sounds/finDeJournée");
        audios["grave"] = Resources.Load<AudioClip>("Sounds/grave");
        audios["London"] = Resources.Load<AudioClip>("Sounds/London");
        audios["moyen"] = Resources.Load<AudioClip>("Sounds/moyen");
        audios["moyen_aigu"] = Resources.Load<AudioClip>("Sounds/moyen_aigu");
        audios["moyen_grave"] = Resources.Load<AudioClip>("Sounds/moyen_grave");
        audios["paris"] = Resources.Load<AudioClip>("Sounds/paris");
        audios["wrongBip"] = Resources.Load<AudioClip>("Sounds/wrongBip");
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void PlaySound(string soundName)
    {
        if (soundName == "wrongBip")
            audioSource.PlayOneShot(audios[soundName], 0.1F);
        else
            audioSource.PlayOneShot(audios[soundName], 0.7F);
    }
}
