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

		if (currentLaps == 2 && fir == false) {
			print ("first lap done");
			GameObject.FindWithTag ("tracker").GetComponent<Animation> ().Play ("First");
			fir = true;
		} else if (currentLaps == 3 && sec == false) {
			print ("second lap done");
			GameObject.FindWithTag ("tracker").GetComponent<Animation> ().Play ("Second");
			sec = true;
		} else if (currentLaps == 4 && thir == false) {
			print ("third lap done");
			GameObject.FindWithTag ("tracker").GetComponent<Animation> ().Play ("Third");
			thir = true;
		} else if (currentLaps == 5 && four == false) {
			print ("fourth lap done");
			GameObject.FindWithTag ("tracker").GetComponent<Animation> ().Play ("Fourth");
			four = true;
			print ("you wooooon kinda");
			SceneManager.LoadScene (3);

		}


	}
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "chariot" && lapCheat == false) {
			currentLaps++;
			print ("you have completed " + currentLaps + " laps");
			lapCheat = false;
			StartCoroutine (delayLaps(col));
			
		}
		if (col.gameObject.tag == "otherChariot") {
			col.GetComponent<AIController> ().lapCompleted ();
		}





	}
	IEnumerator delayLaps(Collider col){
		lapCheat = true;
		yield return new WaitForSeconds (10);
		lapCheat = false;
	}

}
