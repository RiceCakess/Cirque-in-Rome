using UnityEngine;
using System.Collections;

public class CrowdGen : MonoBehaviour {

	// Use this for initialization
	public GameObject prefab;
	void Start () {
		for (int j = 0; j < 5; j++) {
			for (int i = 0; i < 60; i++) {
				GameObject clone = Instantiate (prefab, new Vector3 (69.0f + (9 * j), 7.8f + (j * 2f), (i * 10f) - 162.45f), Quaternion.Euler (new Vector3 (0, 90, 0))) as GameObject;
				clone.transform.localScale = new Vector3 (500, 500, 500);
				clone.name = "leftCrowd";
				if (i > 40) {
					clone.transform.Rotate(new Vector3(0,270 ,0));
					//clone.transform.position += new Vector3 (-i*.5f, 0, -50f);
					clone.transform.position = new Vector3 (-383.5f + (9 * (i-10)) , 8.8f + (j*3.5f),215 + (j * 10f) );
					clone.name = "curvedCrowd";
				}
			}
		}
		for (int j = 0; j < 5; j++) {
			for (int i = 0; i < 60; i++) {
				GameObject clone = Instantiate (prefab, new Vector3 ((69.0f + (9 * j))*-1 -40f, 7.8f + (j * 2f), (i * 10f) - 162.45f), Quaternion.Euler (new Vector3 (0, -90, 0))) as GameObject;
				clone.transform.localScale = new Vector3 (500, 500, 500);
				clone.name = "rightCrowd";
				if (i > 40 ) {
					clone.transform.Rotate(new Vector3(0,270 ,0));
					//clone.transform.position += new Vector3 (-i*.5f, 0, -50f);
					clone.transform.position = new Vector3 (-383.5f + (9 * (i-10)) , 21.8f - (j*3.5f),-230 + (j * 10f) );
					clone.name = "curvedCrowdBack";
				}
			}

		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
