using UnityEngine;
using System.Collections;

public class chariotMovement : MonoBehaviour {
	public float movementSpeed = 10f;
	float speed = 100.0f;
	float desiredSpeed = 0;
	float currentSpeed = 0;
	float accel = .02f;
	float deccel = .05f;
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
			col.gameObject.GetComponent<Rigidbody> ().AddForce (-col.gameObject.GetComponent<Rigidbody>().transform.right * 200f);
			Debug.Log("test");
		}
	}

	void checkInput(){
		transform.Rotate (new Vector3 (0, Input.GetAxis ("Mouse X"), 0) * Time.deltaTime * speed);

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
		//x	Debug.Log (desiredSpeed + " " + currentSpeed + " " + accel);
		if (currentSpeed < desiredSpeed) {
			currentSpeed += accel;
		} else if (currentSpeed > desiredSpeed) {
			currentSpeed -= deccel;
		}

 		transform.position += dirVector * Time.deltaTime * currentSpeed;

	}



}