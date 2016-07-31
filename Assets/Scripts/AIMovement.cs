using UnityEngine;
using System.Collections;

public class AIMovement : MonoBehaviour {
	bool canMove = true;
	AIController control;
	// Use this for initialization
	void Start () {
		control = GetComponent<AIController> ();
		navMeshAgent = this.GetComponent<NavMeshAgent> ();
		waypoint = GameObject.Find ("waypoint1").transform;
		navMeshAgent.SetDestination(waypoint.position);
		chariotObjects = GameObject.FindGameObjectsWithTag ("otherChariot");
	}
	Transform waypoint;
	NavMeshAgent navMeshAgent;
	GameObject[] chariotObjects;
	int index = 1;
	int maxIndex = 7;
	// Update is called once per frame
	void Update () {

		foreach (GameObject g in chariotObjects) {
			if (getDistance (g.transform, transform) < .1f) {
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
		if (getDistance (waypoint.transform, transform) <= .05f) {
			index++;
			if (index > maxIndex)
				index = 1;
			waypoint = GameObject.Find ("waypoint"+index).transform;
			navMeshAgent.SetDestination(waypoint.position);
			//Debug.Log (index + " " + transform.gameObject.name);
		}
		//Debug.DrawRay (transform.position,waypoint.position,Color.red); 
		//navMeshAgent.speed = GetComponent<Rigidbody> ().velocity.x +  GetComponent<Rigidbody> ().velocity.z;
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