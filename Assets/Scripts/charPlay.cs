using UnityEngine;
using System.Collections;

public class charPlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (playChar ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	IEnumerator playChar(){
		GetComponent<Animation> ().Play ("rotate");
		yield return new WaitForSeconds (GetComponent<Animation> () ["rotate"].length - 1.5f);
		StartCoroutine (playChar ());
	}
}
