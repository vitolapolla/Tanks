using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

	// User Inputs
	public float degreesPerSecond = 15.0f;
	public float amplitude = 0.5f;
	public float frequency = 1f;

	public float duration = 5f;

	private Light myLight;

	public GameObject Light1;


	public float maxTime = 10f;
	float timeLeft;

	// Position Storage Variables
	Vector3 posOffset = new Vector3 ();
	Vector3 tempPos = new Vector3 ();

	// Use this for initialization
	void Start () {
		// Store the starting position & rotation of the object
		posOffset = transform.position;

		myLight = GetComponent<Light>();

	}
	
	// Update is called once per frame
	void Update () {
		// Spin object around Y-Axis
		transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

		// Float up/down with a Sin()
		tempPos = posOffset;
		tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;

		transform.position = tempPos;

		Debug.Log (timeLeft);


	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("collide with sphere");
		timeLeft = maxTime;
		Timer ();

		Destroy(gameObject);

		Light1.enabled = !Light1.enabled;


		StartCoroutine (Lights() );


	




	}

	IEnumerator Lights(){
		print(Time.time);
		yield return new WaitForSeconds(5);
		Debug.Log ("Lights");
	}


	void Timer (){
		if (timeLeft > 0) {
			
			Debug.Log ("Lights on");
			timeLeft = maxTime;


		} else {


			Debug.Log ("Lights off");

		}
	}

}
















	
