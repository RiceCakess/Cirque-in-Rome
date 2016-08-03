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
		GetComponent<Animation> ().Play ("horse");
		transform.localPosition = Vector3.Slerp (transform.localPosition, new Vector3 (transform.localPosition.x, transform.localPosition.y + .1f, transform.localPosition.z), GetComponent<Animation> () ["horse"].length / 2);
		yield return new WaitForSeconds (GetComponent<Animation> () ["horse"].length / 2f);
		transform.localPosition = Vector3.Slerp (transform.localPosition, new Vector3 (transform.localPosition.x, transform.localPosition.y - .1f, transform.localPosition.z), GetComponent<Animation> () ["horse"].length / 2);
		yield return new WaitForSeconds (GetComponent<Animation> () ["horse"].length / 2f);

		StartCoroutine (playHorse ());

	}
}
