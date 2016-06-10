using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class soundController : MonoBehaviour {
	public static List<AudioSource> _audioSrc = new List<AudioSource>();
	private int loop0 = 0;
	private bool lerp = false;
	private bool delerp = false;
	//private bool tempo = false;
	private static float[] tempoCollision;
	private static int tom = 0;
	private static int next = 0;
	private static float[] partitionAtome;
	private static int currentMark = 0;

	// Use this for initialization
	void Awake() {
		_audioSrc = new List<AudioSource> ();
		tempoCollision = new float[12];
		partitionAtome = new float[4];
		partitionAtome[0] =	1.0f;
		partitionAtome[1] = 1.3f;
		partitionAtome[2] = 1.6f;
		partitionAtome[3] = 0.9f;
		foreach (AudioClip clips in Resources.LoadAll("Sound")) {
			AudioSource audio = gameObject.AddComponent<AudioSource> ();
			audio.clip = clips;
			_audioSrc.Add(audio);
			//Debug.Log (audio.clip.name);
		}
	}

	void Update(){
		if ((_audioSrc [4].isPlaying || _audioSrc [5].isPlaying) && (_audioSrc [4].time > 5.0f || _audioSrc [5].time > 3.0f)){
				lerp = true;
		}
		if (_audioSrc [6].isPlaying || _audioSrc [6].time > 3.0f)
			_audioSrc [6].pitch = 1.0f;
		if (lerp){
			_audioSrc [0].volume = Mathf.Lerp(_audioSrc [0].volume, 0.5f, 0.01f);
			if (_audioSrc [0].volume > 0.49f)
				lerp = false;
		}
		if (delerp){
			_audioSrc [0].volume = Mathf.Lerp(_audioSrc [0].volume, 0.5f, 0.01f);
			if (_audioSrc [0].volume < 0.51f)
				delerp = false;
		}
		//Jouer le rythme batterie

	}
	
	static public void play(int nbpiste){
		if (nbpiste < _audioSrc.Count) {
			if (!_audioSrc [nbpiste].isPlaying) {
				if (_audioSrc [6].isPlaying && nbpiste == 7) {
				} else {
					_audioSrc [nbpiste].Play ();
					_audioSrc [nbpiste].volume = 1.0f;
				}
				if (nbpiste == 6)
					_audioSrc [nbpiste].pitch = -3.0f;
			}
		}
		else
			Debug.Log("Play: Numéro de piste invalide "+ nbpiste);
	}

	static public void mute(int nbpiste){
		if (nbpiste < _audioSrc.Count) {
			if (_audioSrc [nbpiste].isPlaying)
				_audioSrc [nbpiste].volume = 0.0f;
		}
		else
			Debug.Log("Mute: Numéro de piste invalide "+ nbpiste);
	}

	static public void pitchUp (int nbpiste, float pitch){
		if (nbpiste < _audioSrc.Count) {
			if (_audioSrc [nbpiste].isPlaying)
				_audioSrc [nbpiste].pitch += pitch;
		}
		else
			Debug.Log("PitchUp: Numéro de piste invalide "+ nbpiste);
	}

	static public void reset (int nbpiste){
		if (nbpiste < _audioSrc.Count) {
			if (_audioSrc [nbpiste].isPlaying)
				_audioSrc [nbpiste].time = 0.0f;
		}
		else
			Debug.Log("Reset: Numéro de piste invalide "+ nbpiste);
	}

	static public void loop (int nbpiste){
		if (nbpiste < _audioSrc.Count) {
			if (_audioSrc [nbpiste].isPlaying)
				_audioSrc [nbpiste].loop = true;
		}
		else
			Debug.Log("Loop: Numéro de piste invalide "+ nbpiste);
	}

	static public void unloop (int nbpiste){
		if (nbpiste < _audioSrc.Count) {
			if (_audioSrc [nbpiste].isPlaying)
				_audioSrc [nbpiste].loop = false;
		}
		else
			Debug.Log("Unloop: Numéro de piste invalide "+ nbpiste);
	}

	static public void recordTom(){
		if (tom < 11) {
			tempoCollision [tom] = Time.time;
			tom++;
		}
	}

	static public void nextMark(){
		if (currentMark < partitionAtome.Length-1)
			currentMark++;
		else
			currentMark = 0;
		_audioSrc [0].pitch = partitionAtome [currentMark];

	}

	static public void resetMark(){
		currentMark = 0;
		_audioSrc [0].pitch = partitionAtome [currentMark];
	}
		
}
