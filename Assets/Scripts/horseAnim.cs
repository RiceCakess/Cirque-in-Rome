using UnityEngine;
using System.Collections;

public class horseAnim : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (playHorse ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	IEnumerator playHorse(){
		GetComponent<Animation> ().Play ("Take 001");
		yield return new WaitForSeconds (GetComponent<Animation> () ["Take 001"].length);
		StartCoroutine (playHorse ());

	}
}
