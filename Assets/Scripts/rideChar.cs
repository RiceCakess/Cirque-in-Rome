using UnityEngine;
using System.Collections;

public class rideChar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (playIt ());
	}
	
	// Update is called once per frame
	void Update () {

	}
	IEnumerator playIt(){
		GetComponent<Animation> ().Play ("upDown");
		yield return new WaitForSeconds(GetComponent<Animation>()["upDown"].length);
		StartCoroutine (playIt ());
	}
}
