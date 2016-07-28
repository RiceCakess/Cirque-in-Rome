using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class reticleMovement : MonoBehaviour {
	Image img;
	Sprite reticle;
	bool controller = true;
	public float speed = 10;
	// Use this for initialization
	void Start () {
		img = GetComponent<Image> ();
		reticle = img.sprite;
	}
	
	// Update is called once per frame
	void Update () {
		if (controller == true) {
			img.rectTransform.anchoredPosition += speed * (Input.GetAxis ("Horizontal") * Vector2.right + Input.GetAxis ("Vertical") * Vector2.up);
			if (img.transform.position.x > 1280) {
				img.transform.position = new Vector2 (1280, transform.position.y);
			} else if (img.transform.position.x < 0) {
				img.transform.position = new Vector2 (0, transform.position.y);
			}

			if (img.transform.position.y > 1024) {
				img.transform.position = new Vector2 (transform.position.x, 1024);
			} else if (img.transform.position.y < 0) {
				img.transform.position = new Vector2 (transform.position.x, 0);
			}

				//* Time.deltaTime * speed;
		} else {
			print ("controller not used");
		}
	}
}
