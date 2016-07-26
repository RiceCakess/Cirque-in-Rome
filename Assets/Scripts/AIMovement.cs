using UnityEngine;
using System.Collections;

public class AIMovement : MonoBehaviour {
	bool canMove = true;
	AIController control;
	// Use this for initialization
	void Start () {
		control = GetComponent<AIController> ();
		currentInput = codes [(int)(Random.value * 3)];
		navMeshAgent = this.GetComponent<NavMeshAgent> ();
		waypoint = GameObject.Find ("waypoint1").transform;
		navMeshAgent.SetDestination(waypoint.position);
		chariotObjects = GameObject.FindGameObjectsWithTag ("otherChariot");
	}
	bool walking = false;
	Transform waypoint;
	KeyCode currentInput;
	KeyCode[] codes = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.Q };
	NavMeshAgent navMeshAgent;
	GameObject[] chariotObjects;
	int index = 1;
	int frames = 0;
	// Update is called once per frame
	void Update () {
		//navMeshAgent.Resume();
		//control.sendInput (KeyCode.W);
		/*if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
			if (!navMeshAgent.hasPath || Mathf.Abs (navMeshAgent.velocity.sqrMagnitude) < float.Epsilon) {
				navMeshAgent.SetDestination(GameObject.Find ("waypoint4").transform.position);
			}
		} else {
			walking = true;
		}*/


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
		if (getDistance (waypoint.transform, transform) <= .1f) {
			//goTo (waypoint.transform);
			index++;
			if (index > 3)
				index = 1;
			waypoint = GameObject.Find ("waypoint"+index).transform;
			navMeshAgent.SetDestination(waypoint.position);
		}

	}
	int time = 0;
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
	
}
