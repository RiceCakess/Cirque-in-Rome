using UnityEngine;
using System.Collections;

public class chariotMovement : MonoBehaviour {
	public float movementSpeed = 10f;
	float speed = 100.0f;
	float desiredSpeed = 0;
	float currentSpeed = 0;
	float accel = .02f;
	float deccel = .05f;
	int cameraBump = 60;
	int length = 10;
	float cameraRotation = 0;
	int rotationAmt = 5;
	Vector3 dirVector = new Vector3(0,0,0);
	// Use this for initialization
	GameObject camera;
	void Start () {
		camera = transform.GetChild(0).transform.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		checkInput ();
	}



	void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "otherChariot" || col.gameObject.name == "circus") {
			col.gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.back * 200f);
			Debug.Log ("Test");
		}
	}

	void checkInput(){
		//rotates camera based off of horizontal movement
		transform.Rotate (new Vector3 (0, Input.GetAxis ("Mouse X"), 0) * Time.deltaTime * speed);
		//uses translate to move relative to the camera angle ^ 
		//speed is the speed of the chariot - change based off of stamina? 
		cameraBump--;
		if (cameraBump <= 0 && currentSpeed > 1) {
			transform.GetChild (0).transform.Rotate (rotationAmt, 0, 0);
			cameraRotation = -rotationAmt;
			length = 10;
			cameraBump = 200 - (int)currentSpeed *20;
			if (cameraBump  < 30) {
				cameraBump  = 30;
			}
		}
		if (cameraRotation < 0) {
			length--;
			camera.transform.rotation = Quaternion.identity;
			if (length <= 0) {
				cameraRotation = 0;
			}
		}
		if (Input.GetMouseButton (0)) {
			accel = .5f;
		} else {
			accel = .02f;
		}
		if (Input.GetKey (KeyCode.W)) {
			dirVector = transform.forward; 
			desiredSpeed = movementSpeed;
		} else if (Input.GetKey (KeyCode.A)) {
			dirVector = -transform.right; 
			desiredSpeed = movementSpeed;
		} else if (Input.GetKey (KeyCode.D)) {
			dirVector = transform.right; 
			desiredSpeed = movementSpeed;
		} else {
			desiredSpeed = 0;
		}
		Debug.Log (desiredSpeed + " " + currentSpeed + " " + accel);
		if (currentSpeed < desiredSpeed) {
			currentSpeed += accel;
		} else if (currentSpeed > desiredSpeed) {
			currentSpeed -= deccel;
		}

 		transform.position += dirVector * Time.deltaTime * currentSpeed;

	}



}