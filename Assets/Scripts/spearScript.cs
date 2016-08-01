using UnityEngine;
using System.Collections;

public class spearScript : MonoBehaviour {
	public float speed;
	public float smooth = 2.0F;

	private Vector3 position;
	private Vector3 leftPos = new Vector3(-4.3f, 1.5f, -4.5f);
	private Vector3 rightPos = new Vector3 (-2.1f, 1.4f, -4.5f);
	bool facingLeft = true;
	bool throwable = true;
	// Use this for initialization
	void Start () {
		position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//if the X or B buttons are pressed, switch pos
		if (Input.GetKeyUp (KeyCode.X)) {
			if (facingLeft != true && throwable == true) {
				transform.localPosition = leftPos;
				transform.Rotate (180, 0, 0);
			}
			facingLeft = true;
		} else if (Input.GetKeyUp (KeyCode.B)) {
			if (facingLeft == true && throwable == true) {
				transform.localPosition = rightPos;
				transform.Rotate (180, 0, 0);
			}
			facingLeft = false;
		}
		float vert = Input.GetAxis ("p2 Vertical") * -1;
		float horiz = Input.GetAxis ("p2 Horizontal") * -1;
		Quaternion target = Quaternion.Euler (vert, 0, horiz + 90);
		transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smooth);
		if (Input.GetKeyDown (KeyCode.K) || Input.GetKeyDown("joystick 1 button 0\t")) {
			if (facingLeft == true) {
				GetComponent<Rigidbody> ().isKinematic = false;
				transform.parent = null;
				GetComponent<Rigidbody> ().AddForce ((Vector3.left) * 25);
				throwable = false;
				StartCoroutine (throwReturn (facingLeft));

			} else {
				GetComponent<Rigidbody> ().isKinematic = false;
				GetComponent<Rigidbody> ().AddForce ((Vector3.right) * 25);
				throwable = false;
				StartCoroutine (throwReturn (facingLeft));
			}
			//transform.Rotate (0, 0, 90);

		}
	}

	IEnumerator throwReturn(bool left){
		//GameObject.FindWithTag ("shooter").GetComponent<Animation> ().Play ("Take 001");
		StartCoroutine(charThrows());
		transform.parent = GameObject.FindWithTag ("chariot").transform;
		yield return new WaitForSeconds (1);
		throwable = true;
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		if (left == true) {
			transform.localPosition = leftPos;
		} else {
			transform.localPosition = rightPos;
		}
		GetComponent<Rigidbody> ().isKinematic = true;
	}
	IEnumerator charThrows(){
		GameObject.FindWithTag ("shooter").GetComponent<Animation> ().Play ("Take 001");
		yield return new WaitForSeconds (GameObject.FindWithTag ("shooter").GetComponent<Animation> () ["Take 001"].length);
		GameObject.FindWithTag ("shooter").GetComponent<Animation> ().Play ("Idle");
	}
}
