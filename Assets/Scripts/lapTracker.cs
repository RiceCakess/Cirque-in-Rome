using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class lapTracker : MonoBehaviour {
	//IDK why but each lap = 3 laps so just go with it 
	public float finalLaps = 20;
	public float currentLaps = 0;
	public bool fir = false;
	public bool sec = false;
	public bool thir = false;
	public bool four = false;
	public bool lapCheat = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (currentLaps == 4 && fir == false) {
			print ("first lap done");
			GameObject.FindWithTag ("tracker").GetComponent<Animation> ().Play ("First");
			fir = true;
		} else if (currentLaps == 6 && sec == false) {
			print ("second lap done");
			GameObject.FindWithTag ("tracker").GetComponent<Animation> ().Play ("Second");
			sec = true;
		} else if (currentLaps == 8 && thir == false) {
			print ("third lap done");
			GameObject.FindWithTag ("tracker").GetComponent<Animation> ().Play ("Third");
			thir = true;
		} else if (currentLaps == 10 && four == false) {
			print ("fourth lap done");
			GameObject.FindWithTag ("tracker").GetComponent<Animation> ().Play ("Fourth");
			four = true;
			print ("you wooooon kinda");
			SceneManager.LoadScene (3);

		}


	}
	void OnTriggerEnter(Collider col){
		StartCoroutine (delayLaps(col));
		if (col.gameObject.tag == "chariot") {
			currentLaps++;
			print ("you have completed " + currentLaps + " laps");
			lapCheat = false;
		}





	}
	IEnumerator delayLaps(Collider col){
		yield return new WaitForSeconds (10);
		lapCheat = true;
	}

}
