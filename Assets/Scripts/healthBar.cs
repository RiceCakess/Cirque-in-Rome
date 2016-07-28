using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class healthBar : MonoBehaviour {
	Image bar;
	public Image redFlash;
	public Sprite no;
	public Sprite red;
	// Use this for initialization
	void Start () {
		bar = GetComponent<Image> ();
		bar.type = Image.Type.Filled;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void hitStamina(){
		bar.fillAmount -= 1f/50f;
	}
	public void hit(){
		bar.fillAmount -= .05f;
		StartCoroutine (flashRed ());
	}

	IEnumerator flashRed(){
		redFlash.sprite = red;
		yield return new WaitForSeconds (.1f);
		redFlash.sprite = no;
		//print ("thou hast been hit");

	}

}
