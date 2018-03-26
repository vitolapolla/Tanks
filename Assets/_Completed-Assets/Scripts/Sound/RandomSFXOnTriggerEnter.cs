using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSFXOnTriggerEnter : MonoBehaviour {

    // An array of audio clips to choose from
    public AudioClip[] m_Clips;

    // Our audio source
    private AudioSource m_AudioSource;

	void Start () {
        // Store the audio source on start up
        m_AudioSource = GetComponent<AudioSource>();
	}

    // This is called when the object the script is attached to enters a trigger (duh)
    // In this case the trigger is on the tank and IT MOVES ONTO US
    private void OnTriggerEnter(Collider other)
    {
        // Choose a random index of a clip to play from the array
        int clipIndex = Mathf.FloorToInt(Random.value * m_Clips.Length);
        // Set the clip on the audio source
        m_AudioSource.clip = m_Clips[clipIndex];
        // Play it
        m_AudioSource.Play();
    }
}
