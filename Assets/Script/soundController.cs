using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class soundController : MonoBehaviour {
	public static List<AudioSource> _audioSrc = null;
	private int loop0 = 0;
	private bool lerp = false;
	private bool delerp = false;
	private bool tempo = false;
	// Use this for initialization
	void Start () {
		_audioSrc = new List<AudioSource> ();
		foreach (AudioClip clips in Resources.LoadAll("Sound")) {
			AudioSource audio = gameObject.AddComponent<AudioSource> ();
			audio.clip = clips;
			_audioSrc.Add(audio);
		}
	}

	void Update(){
		if (_audioSrc [0].isPlaying && _audioSrc [0].loop && _audioSrc [0].time > 12.0f){
			if (loop0 == 1) {
				delerp = true;
			}
			else if (loop0 < 1)
				loop0++;
			_audioSrc [0].time = 5.0f;
		}
		if (_audioSrc [3].isPlaying && _audioSrc [3].loop && _audioSrc [3].time > 4.0f){
			_audioSrc [3].time = 0.0f;
		}
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
	}
	
	static public void play(int nbpiste){
		if (nbpiste < _audioSrc.Count) {
			if (!_audioSrc [nbpiste].isPlaying) {
				if (_audioSrc [6].isPlaying && nbpiste == 7) {
				} else
					_audioSrc [nbpiste].Play ();
				if (nbpiste == 6)
					_audioSrc [nbpiste].pitch = -3.0f;
			}
		}
		else
			Debug.Log("Numéro de piste invalide");
	}

	static public void mute(int nbpiste){
		if (nbpiste < _audioSrc.Count) {
			if (_audioSrc [nbpiste].isPlaying)
				_audioSrc [nbpiste].volume = 0.0f;
		}
		else
			Debug.Log("Numéro de piste invalide");
	}

	static public void pitchUp (int nbpiste, float pitch){
		if (nbpiste < _audioSrc.Count) {
			if (_audioSrc [nbpiste].isPlaying)
				_audioSrc [nbpiste].pitch += pitch;
		}
		else
			Debug.Log("Numéro de piste invalide");
	}

	static public void reset (int nbpiste){
		if (nbpiste < _audioSrc.Count) {
			if (_audioSrc [nbpiste].isPlaying)
				_audioSrc [nbpiste].time = 0.0f;
		}
		else
			Debug.Log("Numéro de piste invalide");
	}

	static public void loop (int nbpiste){
		if (nbpiste < _audioSrc.Count) {
			if (_audioSrc [nbpiste].isPlaying)
				_audioSrc [nbpiste].loop = true;
		}
		else
			Debug.Log("Numéro de piste invalide");
	}

	static public void unloop (int nbpiste){
		if (nbpiste < _audioSrc.Count) {
			if (_audioSrc [nbpiste].isPlaying)
				_audioSrc [nbpiste].loop = false;
		}
		else
			Debug.Log("Numéro de piste invalide");
	}
		
}
