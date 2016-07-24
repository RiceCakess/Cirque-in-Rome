using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startToChariot : MonoBehaviour {
	Button btn;
	// Use this for initialization
	void Start () {
		btn = GetComponent<Button> ();
		btn.onClick.AddListener (Click);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void Click(){
		print ("start button clicked");
		SceneManager.LoadScene (1);
	}
}
