using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class chariotMovement : MonoBehaviour {
	public float thrust;
	//public float movementSpeed = 10f;
	float stamina = 50f;
	public float health = 20;
	float speed = 10f;
	float currentSpeed = 0;
	float maxSpeed = 15f;
	bool invincible = false;
	public FloorController flc;
	public bool canMove = false;
	Rigidbody rb;
	Vector3 dirVector = new Vector3(0,0,0);

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		flc.enable ();
		StartCoroutine (startAfterDelay ());
	}
	//1,3
	//0,2
	IEnumerator startAfterDelay(){
		yield return new WaitForEndOfFrame ();
		StartCoroutine (rotateWheels ());
		StartCoroutine (rotateCam ());
		StartCoroutine (rideEffect ());
		StartCoroutine (startPause ());
		soundManager.instance.playfx (transform, soundManager.instance.CaligulaVoice);
		soundManager.instance.playBgm (soundManager.instance.bgm);
		StartCoroutine (regenStamina());
	}


	IEnumerator rotateWheels(){
		GetComponent<Animation> ().Play ("rotate");
		yield return new WaitForSeconds (GetComponent<Animation> ()["rotate"].length);
		StartCoroutine (rotateWheels ());

	}
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("joystick 1 button 0") || Input.GetKey("joystick 2 button 0")){
			GameObject.Find ("skipText").GetComponent<Text>().text = "";
			canMove = true;
		}
		if (!canMove)
			return;
		checkInput ();
		if (Input.GetKey (KeyCode.Space)) {
			Cursor.visible = !Cursor.visible;
		}
		if (health < 1) {
			SceneManager.LoadScene (2);
		}

	}
	//float raiseValue;
	IEnumerator rideEffect(){
		if (rb.velocity.magnitude > 2) {
			float time = (10.0f / rb.velocity.magnitude);
			flc.moveOne (0, 2.5f);
			flc.moveOne (1, 2.5f);
			flc.moveOne (2, 0f);
			flc.moveOne (3, 0f);
			yield return new WaitForSeconds (time);
			flc.moveOne (0, 0f);
			flc.moveOne (1, 0f);
			flc.moveOne (2, 2.5f);
			flc.moveOne (3, 2.5f);
			yield return new WaitForSeconds (time);
		} else {
			yield return new WaitForSeconds (1.0f);
		}
			StartCoroutine (rideEffect ());
	}
	IEnumerator floorRight(){
		flc.moveOne (1, 4.5f);
		flc.moveOne (3, 4.5f);
		yield return new WaitForSeconds (3.0f);
		flc.moveOne (1, 0f);
		flc.moveOne (3, 0f);
	}
	IEnumerator floorLeft(){
		flc.moveOne (0, 4.5f);
		flc.moveOne (2, 4.5f);
		yield return new WaitForSeconds (3.0f);
		flc.moveOne (0, 0f);
		flc.moveOne (2, 0f);
	}
	IEnumerator floorFront(){
		flc.moveOne (2, 4.5f);
		flc.moveOne (3, 4.5f);
		yield return new WaitForSeconds (3.0f);
		flc.moveOne (2, 0f);
		flc.moveOne (3, 0f);
	}
	IEnumerator regenStamina(){
		print ("stamina coroutine starts");
		Debug.Log ("stam coroutine starts");
		yield return new WaitForSeconds (1.0f);
		if (stamina < 50f) {
			Debug.Log ("stamina is updating");
			print ("stam is updating");
			stamina += 1f;
			GameObject bar = GameObject.FindWithTag ("stamina");
			Image health = bar.GetComponent<Image> ();
			health.GetComponent<healthBar> ().updateStamina (stamina);
		}
		Debug.Log ("stamina is updating");
		print ("stam update");
		StartCoroutine (regenStamina());
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
	IEnumerator startPause(){
		yield return new WaitForSeconds (18f);
		GameObject.Find ("skipText").GetComponent<Text>().text = "";
		canMove = true;
	}
	IEnumerator invincibility(){
		print ("invincible");
		invincible = true;
		yield return new WaitForSeconds (1.5f);
		invincible = false;
		print ("not invincible");

	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "otherChariot" || col.gameObject.tag == "circus" || col.gameObject.tag == "median") {
			soundManager.instance.playfx (transform, soundManager.instance.chariotHitsWall);
			Vector3 dir = col.transform.position - transform.position;

			if (invincible == false) {
				GameObject healthImage = GameObject.FindWithTag ("health");
				Image heal = healthImage.GetComponent<Image> ();
				heal.GetComponent<healthBar> ().hit ();
				health -= 1;
				StartCoroutine (invincibility ());

				var relativePoint = transform.InverseTransformPoint(col.transform.position);
				if (relativePoint.x < 0.0) {
					StartCoroutine (floorLeft ());
				} else if (relativePoint.x > 0.0) {
					StartCoroutine (floorRight ());
				} else {
					StartCoroutine (floorFront ());
				}
					
				Debug.Log (relativePoint);
			}
		}
	}
	bool controller = true;
	void checkInput(){
		if(controller)
			transform.Rotate (new Vector3 (0, Input.GetAxis ("p1 Camera"), 0) * Time.deltaTime * speed * 5);
		else
			transform.Rotate (new Vector3 (0, Input.GetAxis ("Mouse X"), 0) * Time.deltaTime * speed * 2);

		if ((Input.GetMouseButtonUp (0) || Input.GetKeyDown("joystick 1 button 0")) && stamina >= 0) {
			GameObject bar = GameObject.FindWithTag ("stamina");
			Image health = bar.GetComponent<Image> ();
			health.GetComponent<healthBar> ().updateStamina (stamina);
			stamina--;
			print ("decreased stamina");
			soundManager.instance.playfx (transform, soundManager.instance.whip);
			soundManager.instance.playfx (transform, soundManager.instance.neigh);
			rb.AddRelativeForce (Vector3.forward * thrust * 50, ForceMode.Acceleration);
		}
		//Debug.Log (Input.GetAxis ("Mouse ScrollWheel"));
		else if (Input.GetKey (KeyCode.W) || Mathf.Abs(Input.GetAxis("p1 Trigger")) > 0.01f && rb.velocity.magnitude < maxSpeed) {
			rb.AddRelativeForce (Vector3.forward * thrust, ForceMode.Acceleration);
		} else if (Input.GetKey (KeyCode.A)) {
			transform.Rotate (new Vector3 (0, -1 * Time.deltaTime * speed * 2, 0));
			//rb.AddRelativeForce (Vector3.left * thrust, ForceMode.Acceleration);
			rb.velocity = rb.velocity * .98f;
		} else if (Input.GetKey (KeyCode.D)) {
			transform.Rotate (new Vector3 (0, Time.deltaTime * speed * 2, 0));
			//rb.AddRelativeForce (Vector3.right * thrust, ForceMode.Acceleration);
			rb.velocity = rb.velocity * .98f;
		} else {
			//no S
			rb.velocity = rb.velocity * .98f;
			//desiredSpeed = 0;
		}
		//Debug.Log (rb.velocity.magnitude  + " " + (rb.velocity.magnitude < maxSpeed));

		if (Input.GetKey (KeyCode.R)) {
			SceneManager.LoadScene (0);
		}
		if (Input.GetKey (KeyCode.E)) {
			print ("spear key pressed");
			//GameObject spear = GameObject.FindWithTag ("spear");
			//spear.GetComponent<spearScript> ().throwSpear ();

		}

		transform.position = new Vector3 (transform.position.x, 0.8f, transform.position.z);

	}
	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "tracker") {
			lapTracker lap = GameObject.FindWithTag ("tracker").GetComponent<lapTracker> ();
			lap.currentLaps++;
			print ("you have completed " + lap.currentLaps + " laps");
		}
	}


}