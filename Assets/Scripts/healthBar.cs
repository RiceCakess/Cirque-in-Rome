using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class healthBar : MonoBehaviour {
	Image bar;
	// Use this for initialization
	void Start () {
		bar = GetComponent<Image> ();
		bar.type = Image.Type.Filled;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void hitStamina(){
		bar.fillAmount -= 1f/20f;
	}
	public void hit(){
		bar.fillAmount -= .1f;
	}
}
