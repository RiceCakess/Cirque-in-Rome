using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startToChariot : MonoBehaviour {
	Button btn;
	// Use this for initialization
	void Start () {
		btn = gameObject.GetComponent<Button> ();
		print("error after getting component");
		btn.onClick.AddListener (Click);
		print ("error after add listener");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void Click(){
		print ("start button clicked");
		SceneManager.LoadScene (1);
	}
}
