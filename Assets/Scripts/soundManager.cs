using UnityEngine;
using System.Collections;

public class soundManager : MonoBehaviour {
	public static soundManager instance = null;
	//use 
	public AudioSource gallopSrc;
	public AudioSource cheerSrc;
	public AudioSource src1;
	public AudioSource heartSource;
	public AudioSource bgm_src;
	public AudioClip horses;
	public AudioClip chariotHitsOther;
	public AudioClip chariotHitsWall;
	public AudioClip whip;
	public AudioClip heartbeat;
	public AudioClip rolling;
	public AudioClip crash;
	public AudioClip cheering;
	public AudioClip booing;
	public AudioClip chattering;
	public AudioClip clapping;
	public AudioClip horseBreathing;
	public AudioClip neigh;
	public AudioClip gallop;



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
		playHeart ();
		playGallop ();
		cheer ();
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
		if (heartSource.isPlaying == false) {
			heartSource.Play ();
		}
		if (gallopSrc.isPlaying == false) {
			gallopSrc.Play ();
		}
		if (cheerSrc.isPlaying == false) {
			cheerSrc.Play ();
		}
	}
	public void playHeart(){
		heartSource.clip = heartbeat;
		heartSource.Play ();
	}
	public void playGallop(){
		gallopSrc.clip = gallop;
		gallopSrc.Play ();
	}
	public void cheer(){
		cheerSrc.clip = cheering;
		cheerSrc.Play ();

	}
}
