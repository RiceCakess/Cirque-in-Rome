

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class healthBar : MonoBehaviour {
	Image bar;
	public Image redFlash;
	public Sprite no;
	public Sprite red;
	public Image bloodTint;
	// Use this for initialization
	void Start () {
		bar = GetComponent<Image> ();
		bar.type = Image.Type.Filled;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void updateStamina(float stamina){
		bar.fillAmount = stamina/50f;
	}
	public void hit(){
		bar.fillAmount -=  1/50f;
		StartCoroutine (flashRed ());
		updateTint ();
	}

	private void updateTint(){
		Color c = bloodTint.color;
		c.a = 1f - bar.fillAmount;
		bloodTint.color = c;
	}
	IEnumerator flashRed(){
		redFlash.sprite = red;
		yield return new WaitForSeconds (.1f);
		redFlash.sprite = no;
		//print ("thou hast been hit");

	}

}
