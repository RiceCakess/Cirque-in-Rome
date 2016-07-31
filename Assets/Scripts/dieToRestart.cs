using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class dieToRestart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("joystick button 0") || Input.GetKeyDown(KeyCode.Space) ){
			SceneManager.LoadScene (0);

		}
	}
}
