using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AIController : MonoBehaviour {
	public float thrust = 5f;
	/*public float movementSpeed = 8f;
	float speed = 100.0f;
	float desiredSpeed = 0;
	public float currentSpeed = 0;
	float accel = .02f;
	float deccel = .05f;*/
	Vector3 dirVector = new Vector3(0,0,0);
	List<KeyCode> currentInput = new List<KeyCode>();
	//public float rotation = 0;
	//public Vector3 rotationVector3 = new Vector3(0,0,0);
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		checkInput ();
	}
	public void sendInput(KeyCode key){
		if (!currentInput.Contains (key)) {
			currentInput.Add (key);
		}
	}
	void checkInput(){
		//transform.Rotate ( rotationVector3 * Time.deltaTime * speed);

		if (currentInput.Contains(KeyCode.Q)) {
			thrust = 10f;
		}
		if (currentInput.Contains(KeyCode.W)) {
			dirVector = transform.forward; 
		} else if (currentInput.Contains (KeyCode.A)) {
			dirVector = -transform.right; 
		} else if (currentInput.Contains (KeyCode.D)) {
			dirVector = transform.right; 
		}
		//x	Debug.Log (desiredSpeed + " " + currentSpeed + " " + accel);
		/*if (currentSpeed < desiredSpeed) {
			currentSpeed += accel;
		} else if (currentSpeed > desiredSpeed) {
			currentSpeed -= deccel;
		}*/
		GetComponent<Rigidbody> ().AddRelativeForce (dirVector * 2f);
		//Debug.Log (thrust);
		currentInput.Clear ();
		transform.position = new Vector3 (transform.position.x, .7f, transform.position.z);
		//rotation = 0;
	}
}