using UnityEngine;
using System.Collections;

public class AIMovement : MonoBehaviour {
	bool canMove = false;
	AIController control;
	GameObject player;
	// Use this for initialization
	void Start () {
		control = GetComponent<AIController> ();
		navMeshAgent = this.GetComponent<NavMeshAgent> ();
		chariotObjects = GameObject.FindGameObjectsWithTag ("otherChariot");
		player = GameObject.Find ("finalChar");
	}
	Transform waypoint;
	Vector3 setPosition;
	NavMeshAgent navMeshAgent;
	GameObject[] chariotObjects;
	int index = 1;
	int maxIndex = 7;
	bool following = false;
	bool hasSpear = true;
	// Update is called once per frame
	void Update () {
		foreach (Transform child in transform) {
			if (child.tag == "spear") {
				Rigidbody rbc = child.GetComponent<Rigidbody> ();
				Vector3 target = transform.position - child.transform.position;
				Debug.DrawRay (child.transform.forward, transform.position - child.transform.position, Color.red);
				Vector3 look = Vector3.RotateTowards (child.transform.forward, target, 10f, 0.0F);
				look.z += 90f;
				child.transform.rotation = Quaternion.LookRotation(look);
				rbc.isKinematic = false;
				rbc.useGravity = true;

				rbc.transform.parent = null;

				//rbc.AddRelativeForce (new Vector3(1,0,0) * 150 + (Vector3.up) * 250);
			}
		}
		if (canMove != player.GetComponent<chariotMovement> ().canMove) {
			setDestination(GameObject.Find ("waypoint1").transform);
			canMove = true;
		}
		if (!canMove)
			return;
		foreach (GameObject g in chariotObjects) {
			if (getDistance (g.transform, transform) < .3f) {
				if (Random.value > .5f) {
					control.sendInput (KeyCode.Q);
				} else {
					canMove = false;
				}
			}
		}
		/*if ((transform.position - player.transform.position).magnitude > 0 && (transform.position - player.transform.position).magnitude <= 20f && following) {
			navMeshAgent.SetDestination (player.transform.position);
			if (Random.value > .8f) {
				setDestination(GameObject.Find("waypoint" + index).transform);
				following = false;
			}
		}*/
		if (canMove)
			control.sendInput (KeyCode.W);
		else
			canMove = true;
		if (navMeshAgent.remainingDistance <= 10f) {
			index++;
			if (index > maxIndex)
				index = 1;
			setDestination(GameObject.Find("waypoint" + index).transform);
		}
		//Debug.Log (gameObject.name + " " + navMeshAgent.remainingDistance + " " + index + " " + following);
		//Debug.DrawRay (transform.position, navMeshAgent.destination - transform.position, Color.red); 
	}
	void throwSpear(){
		if (hasSpear) {

			hasSpear = false;
			foreach (Transform child in transform) {
				if (child.tag == "spear") {
					Rigidbody rbc = child.GetComponent<Rigidbody> ();
					Vector3 target = child.transform.position - transform.position;
					Vector3 look = Vector3.RotateTowards (child.transform.forward, target, 10f, 0.0F);
					child.transform.rotation = Quaternion.LookRotation(look);
					rbc.isKinematic = false;
					rbc.useGravity = true;

					rbc.transform.parent = null;

					//rbc.AddRelativeForce (new Vector3(1,0,0) * 150 + (Vector3.up) * 250);
				}
			}
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
	void setDestination(Transform toWaypoint){
		waypoint = toWaypoint;
		setPosition = toWaypoint.transform.position + new Vector3 (Random.value*40f, 0, Random.value*10f);
		navMeshAgent.SetDestination (setPosition);
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