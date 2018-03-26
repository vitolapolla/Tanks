using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXOnTriggerEnter : MonoBehaviour {

    // Our audio source
    private AudioSource m_AudioSource;

	// Use this for initialization
	void Start () {
        // Store the audio source on start up
        m_AudioSource = GetComponent<AudioSource>();
	}

    // This is called when the object the script is attached to enters a trigger (duh)
    // In this case the trigger is on the tank and IT MOVES ONTO US
    private void OnTriggerEnter(Collider other)
    {
        // Play the clip already set on the audio source
        m_AudioSource.Play();
    }
}
