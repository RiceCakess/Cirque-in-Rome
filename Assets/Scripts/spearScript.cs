using UnityEngine;
using System.Collections;

public class spearScript : MonoBehaviour {
	public float speed;
	public float smooth = 2.0F;

	//private Vector3 position;
	private Vector3 leftPos = new Vector3(-4.3f, 1.5f, -4.5f);
	private Vector3 rightPos = new Vector3 (-2.1f, 1.4f, -4.5f);
	bool facingLeft = true;
	bool throwable = true;
	// Use this for initialization
	void Start () {
		//position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		//if the X or B buttons are pressed, switch pos
		//if the X button is pressed, then switch the spear's side to the left
		if (Input.GetKey("joystick 2 button 2") || Input.GetKeyUp(KeyCode.Z)) {
			if (facingLeft != true && throwable == true) {
				transform.localPosition = leftPos;
				transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, 90));
			}
			//facing left boolean 
			facingLeft = true;
			//if the B button is pressed, then switch the spear's side to the right

		} else if (Input.GetKey("joystick 2 button 1") || Input.GetKeyUp(KeyCode.X)) {
			if (facingLeft == true && throwable == true) {
				transform.localPosition = rightPos;
				//rotates the spear so that the 
				transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, -90));
			}
			facingLeft = false;
		}
		//third is up-down
		//first is right-left
		//adds torque, does not move spear itself but changes the effect when it is thrown - CHANGE WHEN HAVE 2 CONTROLLERS TO JUST ROTATION
		if (Mathf.Abs(Input.GetAxis ("p2 Horizontal")) >= 1) {
			transform.Rotate(new Vector3(Input.GetAxis("p2 Horizontal"), 0, 0));
		}
		if (Mathf.Abs(Input.GetAxis ("p2 Vertical")) >= 1) {
			transform.Rotate(new Vector3(0, 0, -1 * Input.GetAxis("p2 Vertical")));
		}

		//float vert = Input.GetAxis ("p2 Vertical") * -1;
		//float horiz = Input.GetAxis ("p2 Horizontal") * -1;
		//Quaternion target = Quaternion.Euler (vert, 0, horiz + 90);
		//transform.localRotation = Quaternion.Slerp (transform.localRotation, target, Time.deltaTime * smooth);
		//if A is pressed, shoot the spear

		//remove third, joystick button 0, if you have 2 controllers 
		if (Input.GetKeyDown (KeyCode.K) || Input.GetKeyDown("joystick 2 button 0") || Input.GetKeyDown("joystick 1 button 3") ){
			if (facingLeft == true) {
				//makes the spear moveable and use gravity
				GetComponent<Rigidbody> ().isKinematic = false;
				GetComponent<Rigidbody> ().useGravity = true;
				//makes it so there is no parent 
				transform.parent = null;
				//adds force diagonally up and left 
				GetComponent<Rigidbody> ().AddForce ((Vector3.left) * 150 + (Vector3.up) * 250);
				//makes it so the player cannot throw again until the throwreturn corountine is over 
				throwable = false;
				//starts coroutine with a delay 
				StartCoroutine (throwReturn (facingLeft));

			} else {
				GetComponent<Rigidbody> ().isKinematic = false;
				GetComponent<Rigidbody> ().useGravity = true;
				transform.parent = null;
				GetComponent<Rigidbody> ().AddForce ((Vector3.right) * 150 + (Vector3.up) * 250);
				throwable = false;
				StartCoroutine (throwReturn (facingLeft));
			}
			//transform.Rotate (0, 0, 90);

		}
	}

	IEnumerator throwReturn(bool left){
		//GameObject.FindWithTag ("shooter").GetComponent<Animation> ().Play ("Take 001");
		//plays the animation for throwing
		StartCoroutine(charThrows());
		//reparents to the chariot
		transform.parent = GameObject.FindWithTag ("chariot").transform;
		//creates a delay of 1.75 sec 
		yield return new WaitForSeconds (1.75f);
		GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		//resets so that the spear can be thrown again 
		throwable = true;
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		GetComponent<Rigidbody> ().useGravity = false;
		if (left == true) {
			transform.localPosition = leftPos;
			transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, 90));
		} else {
			transform.localPosition = rightPos;
			transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, -90));
		}
		GetComponent<Rigidbody> ().isKinematic = true;

	}
	IEnumerator charThrows(){
		GameObject.FindWithTag ("shooter").GetComponent<Animation> ().Play ("Take 001");
		yield return new WaitForSeconds (GameObject.FindWithTag ("shooter").GetComponent<Animation> () ["Take 001"].length);
		GameObject.FindWithTag ("shooter").GetComponent<Animation> ().Play ("Idle");
	}
}
