using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AIController : MonoBehaviour {
	public float movementSpeed = 8f;
	float speed = 100.0f;
	float desiredSpeed = 0;
	public float currentSpeed = 0;
	float accel = .02f;
	float deccel = .05f;
	Vector3 dirVector = new Vector3(0,0,0);
	List<KeyCode> currentInput = new List<KeyCode>();
	void Start () {

	}

	// Update     called once per frame
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
			accel = 10f;
		} else {
			accel = .02f;
		}
		if (currentInput.Contains(KeyCode.W)) {
			dirVector = transform.forward; 
			desiredSpeed = movementSpeed;
		} else if (currentInput.Contains (KeyCode.A)) {
			dirVector = -transform.right; 
			desiredSpeed = movementSpeed * .5f;
		} else if (currentInput.Contains (KeyCode.D)) {
			dirVector = transform.right; 
			desiredSpeed = movementSpeed * .5f;
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
		currentInput.Clear ();
		transform.position = new Vector3 (transform.position.x, .7f, transform.position.z);
	}
}