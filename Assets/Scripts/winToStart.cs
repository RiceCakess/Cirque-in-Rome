using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class winToStart : MonoBehaviour {

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
