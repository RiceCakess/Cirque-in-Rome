using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class reticleMovement : MonoBehaviour {
	Image img;
	//Sprite reticle;
	bool controller = true;
	public float speed = 10;
	// Use this for initialization
	void Start () {
		img = GetComponent<Image> ();
		//reticle = img.sprite;
	}

	// Update is called once per frame
	void Update () {
		Debug.Log (Input.GetAxis ("p2 Horizontal") + " " + Input.GetAxis ("p2 Vertical"));
		if (controller == true && (Mathf.Abs(Input.GetAxis ("p2 Horizontal")) >= 1 || Mathf.Abs(Input.GetAxis ("p2 Vertical")) >= 1 )) {

			Vector3 desiredPosX = img.rectTransform.anchoredPosition + new Vector2(speed * Input.GetAxis ("p2 Horizontal") * -1, 0);
			Vector3 desiredPosY = img.rectTransform.anchoredPosition + new Vector2(0, Input.GetAxis ("p2 Vertical") * speed);
			if(gameObject.name == "ReticleRight"){
				if (desiredPosX.x < 620 && desiredPosX.x > 245) {
					img.rectTransform.anchoredPosition += new Vector2 (speed * Input.GetAxis ("p2 Horizontal") * -1, 0); 
				}
			}
			else{
				if (desiredPosX.x > -600 && desiredPosX.x < -255) {
					img.rectTransform.anchoredPosition += new Vector2 (speed * Input.GetAxis ("p2 Horizontal") * -1, 0); 
				}
			}
			if (desiredPosY.y < 280 && desiredPosY.y > 37) {
				img.rectTransform.anchoredPosition += new Vector2(0, Input.GetAxis ("p2 Vertical") * speed); 
			}
			//* Time.deltaTime * speed;
		} else {
			print ("controller not used");
		}
	}
}