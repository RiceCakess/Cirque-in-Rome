using UnityEngine;
using System.Collections;

public class soundManager : MonoBehaviour {
	public static soundManager instance = null;
	//use 

	public AudioSource src1;
	public AudioSource bgm_src;
	public AudioClip horses;
	public AudioClip chariotHitsOther;
	public AudioClip chariotHitsWall;
	public AudioClip CaligulaVoice;


	public AudioClip bgm;

	public GameObject emptySource;
	//prefab

	void Awake(){
		//ensures that there is only one of soundManager
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}

	}


	// Use this for initialization
	void Start () {
		
		playBgm (bgm);
	}

	public void playfx(Transform target_object, AudioClip clip){
		//create an emptysource prefab
		//put it where the sound comes from 
		//let the audio source of this prefab play the audio clip
		GameObject newEmptySrc =  Instantiate(emptySource);
		newEmptySrc.transform.position = target_object.position;
		AudioSource src = newEmptySrc.GetComponent<AudioSource>();
		//getting the audioSource on the new emptySource //empty source is child of soundManager
		newEmptySrc.transform.parent = transform;
		//print ("done");
		src.clip = clip;
		src.Play ();
	}
	public void playBgm(AudioClip clip){
		bgm_src.clip = clip;
		bgm_src.Play ();


	}

	public void stopBgm(){
		bgm_src.Stop ();
	}
	// Update is called once per frame
	void Update () {
		//GetComponent<Animator> ().SetBool ("move", true);
		if (bgm_src.isPlaying  == false) {
			bgm_src.Play ();

		}
	}
}
