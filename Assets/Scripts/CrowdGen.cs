using UnityEngine;
using System.Collections;

public class CrowdGen : MonoBehaviour {

	// Use this for initialization
	public GameObject prefab;
	void Start () {
		for (int j = 0; j < 5; j++) {
			for (int i = 0; i < 30; i++) {
				GameObject clone = Instantiate (prefab, new Vector3 (69.0f + (9 * j), 7.24f + (j * 2f), (i * 10f) - 132.45f), Quaternion.Euler (new Vector3 (0, 90, 0))) as GameObject;
				clone.transform.localScale = new Vector3 (500, 500, 500);
			}
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
