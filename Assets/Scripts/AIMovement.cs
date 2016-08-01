using UnityEngine;
using System.Collections;

public class AIMovement : MonoBehaviour {
	bool canMove = true;
	AIController control;
	// Use this for initialization
	void Start () {
		Random.seed = System.DateTime.Now.Millisecond + convertToInt(gameObject.name);
		control = GetComponent<AIController> ();
		navMeshAgent = this.GetComponent<NavMeshAgent> ();
		setDestination(GameObject.Find ("waypoint1").transform);
		chariotObjects = GameObject.FindGameObjectsWithTag ("otherChariot");
	}
	Transform waypoint;
	Vector3 setPosition;
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
		if (navMeshAgent.remainingDistance <= 10f) {
			index++;
			if (index > maxIndex)
				index = 1;
			setDestination(GameObject.Find("waypoint" + index).transform);
		}
		//Debug.Log (gameObject.name + " " + navMeshAgent.remainingDistance);
		//Debug.DrawRay (transform.position, setPosition - transform.position, Color.red); 
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
		setPosition = toWaypoint.transform.position + new Vector3 (Random.value*20f, 0, Random.value*10f);
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
	int convertToInt(string name){
		if (name == "otherOne")
			return 1;
		else if (name == "otherTwo")
			return 2;
		else if (name == "otherthree")
			return 3;
		return 0;
	}
}