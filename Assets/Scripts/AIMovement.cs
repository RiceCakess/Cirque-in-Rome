using UnityEngine;
using System.Collections;

public class AIMovement : MonoBehaviour {
	bool canMove = true;
	bool waitingForStart = true;
	AIController control;
	// Use this for initialization
	void Start () {
		StartCoroutine (waitForStart ());
		control = GetComponent<AIController> ();
		currentInput = codes [(int)(Random.value * 3)];
		navMeshAgent = this.GetComponent<NavMeshAgent> ();
		waypoint = GameObject.Find ("waypoint1").transform;
		navMeshAgent.SetDestination(waypoint.position);
		chariotObjects = GameObject.FindGameObjectsWithTag ("otherChariot");

	}
	Transform waypoint;
	KeyCode currentInput;
	KeyCode[] codes = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.Q };
	NavMeshAgent navMeshAgent;
	GameObject[] chariotObjects;
	int index = 1;
	Vector3 lastPosition;
	int notMoving = 0;
	int time = 0;
	public bool debug = false;
	// Update is called once per frame
	IEnumerator waitForStart(){
		yield return new WaitForSeconds (20);
		waitingForStart = false;
	}



	void Update () {
		if (waitingForStart == false) {
			foreach (GameObject g in chariotObjects) {
				if (getDistance (g.transform, transform) < .01f) {
					if (Random.value > .5f) {
						control.sendInput (KeyCode.Q);
					} else {
						canMove = false;
					}
				}
			}
			if (canMove)
				control.sendInput (KeyCode.W);
			else
				canMove = true;
			if (getDistance (waypoint.transform, transform) <= .5f) {
				index++;
				if (index > 4)
					index = 1;
				waypoint = GameObject.Find ("waypoint" + index).transform;
				navMeshAgent.SetDestination (waypoint.position += new Vector3 (Random.value, 0, Random.value));
			}

			/*if (lastPosition != null) {
			if (getDistance (lastPosition, transform.position) < .009f) {
				notMoving++;
			} else if (notMoving > 3) {
				notMoving = 0;
			}
			if(debug)
				Debug.Log (getDistance (transform.position, lastPosition) + " " + notMoving);
			lastPosition = transform.position;
		} else {
			lastPosition = transform.position;
		}
		if (notMoving > 8 && control.currentSpeed > 6f) {
			transform.Rotate (new Vector3 (0, 180f, 0));
			notMoving = 0;
		}
		time++;*/
		}

	}
	void goTo(Transform pos){
		float step = 2f * Time.deltaTime;
		Vector3 target = pos.position - transform.position;
		//target = new Vector3 (target.x + (Random.value * 20f), target.y, target.z + (Random.value * 20f));
		Vector3 look = Vector3.RotateTowards (transform.forward, target, step, 0.0F);
		look.y = 0;
		transform.rotation = Quaternion.LookRotation(look);

	}
	float getDistance(Transform d1, Transform d2){
		float subx = (d1.position.x - d2.position.x);
		float subz = (d2.position.z - d2.position.z);
		float hyp = Mathf.Sqrt (subx * subx + subz * subz);
		return hyp;
	}
	float getDistance(Vector3 d1, Vector3 d2){
		float subx = (d1.x - d2.x);
		float subz = (d2.z - d2.z);
		float hyp = Mathf.Sqrt (subx * subx + subz * subz);
		return hyp;
	}
	
}
