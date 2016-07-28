using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class chariotMovement : MonoBehaviour {
	public float thrust;
	public float movementSpeed = 10f;
	public float stamina = 20f;
	public float health = 20;
	float speed = 10f;
	float currentSpeed = 0;
	float maxSpeed = 10f;
	public FLController flc;
	Rigidbody rb;
	Vector3 dirVector = new Vector3(0,0,0);
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		StartCoroutine (rotateWheels ());
		StartCoroutine (rotateCam ());
		flc.enable ();
		StartCoroutine (rideEffect ());
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
	float raiseValue;
	IEnumerator rideEffect(){
		raiseValue = Mathf.Floor(rb.velocity.magnitude * 2) / 2;
		raiseValue /= 2;
		flc.moveOne (2,raiseValue);
		flc.moveOne (3,raiseValue);
		flc.moveOne (0,0);
		flc.moveOne (1,0);
		yield return new WaitForSeconds (2.0f);
		flc.moveOne (2,0);
		flc.moveOne (3,0);
		flc.moveOne (0,raiseValue);
		flc.moveOne (1,raiseValue);
		yield return new WaitForSeconds (2.0f);
		StartCoroutine (rideEffect());
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
		if (col.gameObject.tag == "otherChariot" || col.gameObject.tag == "circus" || col.gameObject.tag == "median") {
			//col.gameObject.rb.AddForce (-col.gameObject.GetComponent<Rigidbody>().transform.right * 200f);
			Debug.Log("test");
			GameObject healthImage = GameObject.FindWithTag ("health");
			Image heal = healthImage.GetComponent<Image> ();
			heal.GetComponent<healthBar> ().hit ();
			soundManager.instance.playfx (transform, soundManager.instance.chariotHitsWall);
			health -= 1;
			print ("health is" + health);
			flc.RumbleDown ();
		}
//		if (col.gameObject.tag == "circus") {
//			currentSpeed /= 2;
//
//		}
	}
	bool controller = true;
	void checkInput(){
		if(controller)
			transform.Rotate (new Vector3 (0, Input.GetAxis ("Horizontal"), 0) * Time.deltaTime * speed * 5);
		else
			transform.Rotate (new Vector3 (0, Input.GetAxis ("Mouse X"), 0) * Time.deltaTime * speed * 2);

		if ((Input.GetMouseButtonUp (0) || Input.GetKeyDown("joystick button 0")) && stamina >= 0) {
			GameObject bar = GameObject.FindWithTag ("stamina");
			Image health = bar.GetComponent<Image> ();
			health.GetComponent<healthBar> ().hitStamina ();
			print ("hit");
			stamina--;
			rb.AddRelativeForce (Vector3.forward * thrust * 5, ForceMode.Acceleration);
		} else {
			//rb.velocity *= .9f;
		}
		//Debug.Log (Input.GetAxis ("Mouse ScrollWheel"));
		if (Input.GetKey (KeyCode.W) || Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0.01f && rb.velocity.magnitude < maxSpeed) {
			rb.AddRelativeForce (Vector3.forward * thrust, ForceMode.Acceleration);
		} else if (Input.GetKey (KeyCode.A)) {
			rb.AddRelativeForce (Vector3.left * thrust, ForceMode.Acceleration);
		} else if (Input.GetKey (KeyCode.D)) {
			rb.AddRelativeForce (Vector3.right * thrust, ForceMode.Acceleration);
		} else {
			//no S
			//desiredSpeed = 0;
		}
		//Debug.Log (rb.velocity.magnitude  + " " + (rb.velocity.magnitude < maxSpeed));

		if (Input.GetKey (KeyCode.R)) {
			SceneManager.LoadScene (0);
		}
		if (Input.GetKey (KeyCode.E)) {
			print ("spear key pressed");
			GameObject spear = GameObject.FindWithTag ("spear");
			spear.GetComponent<spearScript> ().throwSpear ();

		}

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