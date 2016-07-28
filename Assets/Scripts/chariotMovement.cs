﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class chariotMovement : MonoBehaviour {
	public Transform cam;
	public Transform colCam;
	public float thrust;
	public float movementSpeed = 10f;
	public float stamina = 20f;
	public float health = 20;
	float speed = 10f;
	float desiredSpeed = 0;
	float currentSpeed = 0;
	float accel = .02f;
	float deccel = .05f;
	Vector3 dirVector = new Vector3(0,0,0);
	// Use this for initialization
	void Start () {
		StartCoroutine (rotateWheels ());
		StartCoroutine (rotateCam ());
		soundManager.instance.playBgm (soundManager.instance.bgm);
		soundManager.instance.playfx (transform, soundManager.instance.CaligulaVoice);
	}
	IEnumerator rotateWheels(){
		GetComponent<Animation> ().Play ("rotate");
		yield return new WaitForSeconds (GetComponent<Animation> ()["rotate"].length);
		StartCoroutine (rotateWheels ());

	}
	// Update is called once per frame
	void Update () {
		checkInput ();
		if (Input.GetKey (KeyCode.Space)) {
			Cursor.visible = !Cursor.visible;
		}
		if (health < 1) {
			SceneManager.LoadScene (2);
		}

	}
	IEnumerator rotateCam(){
		float speed = currentSpeed;
		for(int i = 0; i < 12 - speed; i++){
			transform.Rotate (0.2f, 0, 0);
			//Debug.Log ("current speed:" + currentSpeed);
			yield return new WaitForSeconds (.02f);
			}
		yield return new WaitForSeconds (.02f);
		for(int i = 0; i < 12 - speed; i++){
			//Debug.Log ("current speed:" + currentSpeed);
			transform.Rotate (-0.2f, 0, 0);
			yield return new WaitForSeconds (.02f);
		}
		//soundManager.instance.playfx (transform, soundManager.instance.CaligulaVoice);
		StartCoroutine (rotateCam ());
	}


	void OnCollisionEnter(Collision col){
		//if (col.gameObject.tag == "otherChariot" || col.gameObject.tag == "circus" || col.gameObject.tag == "median") {
			cam = transform;
			Vector3 selfVec = cam.InverseTransformPoint (transform.position);
			colCam = col.transform;
			Vector3 colVec = colCam.InverseTransformPoint (col.transform.position);
			if (selfVec.x > colVec.x) {
				print ("selfVec.x > colVec.x");
			} else if (selfVec.x < colVec.x) {
				print ("selfVec.x < colVec.x");
			} else if (selfVec.x == colVec.x) {
				print ("selfVec.x = colVec.x");
			}

			//col.gameObject.GetComponent<Rigidbody> ().AddForce (-col.gameObject.GetComponent<Rigidbody>().transform.right * 200f);
			//Debug.Log("test");
			GameObject healthImage = GameObject.FindWithTag ("health");
			Image heal = healthImage.GetComponent<Image> ();
			heal.GetComponent<healthBar> ().hit ();
			soundManager.instance.playfx (transform, soundManager.instance.chariotHitsWall);
			health -= 1;
			//print ("health is" + health);

		//}
//		if (col.gameObject.tag == "circus") {
//			currentSpeed /= 2;
//
//		}
	}

	void checkInput(){
		transform.Rotate (new Vector3 (0, Input.GetAxis ("Mouse X"), 0) * Time.deltaTime * speed * 2);

		if (Input.GetMouseButtonUp (0) && stamina >= 0) {
			GameObject bar = GameObject.FindWithTag ("stamina");
			Image health = bar.GetComponent<Image> ();
			health.GetComponent<healthBar> ().hitStamina ();
			print ("hit");
			stamina--;
			GetComponent<Rigidbody> ().AddRelativeForce (Vector3.forward * thrust * 5, ForceMode.Acceleration);
			//accel = .5f;
		} else {
			//accel = .02f;
		}
		if (Input.GetKey (KeyCode.W)) {
			GetComponent<Rigidbody> ().AddRelativeForce (Vector3.forward * thrust, ForceMode.Acceleration);
		} else if (Input.GetKey (KeyCode.A)) {
			GetComponent<Rigidbody> ().AddRelativeForce (Vector3.left * thrust, ForceMode.Acceleration);
		} else if (Input.GetKey (KeyCode.D)) {
			GetComponent<Rigidbody> ().AddRelativeForce (Vector3.right * thrust, ForceMode.Acceleration);
		} else {
			//no S
			//desiredSpeed = 0;
		}
		if (Input.GetKey (KeyCode.R)) {
			SceneManager.LoadScene (0);
		}
		if (Input.GetKey (KeyCode.E)) {
			print ("spear key pressed");
			GameObject spear = GameObject.FindWithTag ("spear");
			spear.GetComponent<spearScript> ().throwSpear ();

		}

		//x	Debug.Log (desiredSpeed + " " + currentSpeed + " " + accel);
//		if (currentSpeed < desiredSpeed) {
//			currentSpeed += accel;
//		} else if (currentSpeed > desiredSpeed) {
//			currentSpeed -= deccel;
//		}

		transform.position = new Vector3 (transform.position.x, 0.6f, transform.position.z);

	}
	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "tracker") {
			lapTracker lap = GameObject.FindWithTag ("tracker").GetComponent<lapTracker> ();
			lap.currentLaps++;
			print ("you have completed " + lap.currentLaps + " laps");
		}
	}


}