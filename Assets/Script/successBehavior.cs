using UnityEngine;
using System.Collections;

public class successBehavior : MonoBehaviour {

	static private bool[] success = null;

	// Use this for initialization
	void Start () {
		success = new bool[6];
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Changement de charge d'une particule
	static public void quantumSuccess(){
		if (!success [0]) {
			Debug.Log ("Les Quanta");
			success [0] = true;
		}
	}

	//Passage d'une particule dans un lazer
	static public void dualitySuccess(){
		if (!success [1]) {
			Debug.Log ("Dualité Onde-Particule");
			success [1] = true;
		}
		//soundController.play (1);
	}

	//rencontre d'une particule avec un noyau à une grande vitesse
	static public void fissionSuccess(){
		if (!success [2]) {
			Debug.Log("Fission Nucléaire");
			success [2] = true;
		}
		soundController.play (4);
		soundController.mute (0);
	}

	//rencontre d'une particule avec un noyau à une très grande vitesse
	static public void fusionSuccess(){
		if (!success [3]) {
			Debug.Log ("Fusion Nucléaire");
			success [3] = true;
		}
		soundController.play (5);
		soundController.mute (0);
	}

	//accélerer une particule à l'aide de deux trou noir
	static public void accelerateurSuccess(){
		if (!success [4]) {
			Debug.Log("Accelerateur de Particule");
			success [4] = true;
		}
		soundController.play (6);
	}

	//faire orbité une particule autour d'un noyau
	static public void valenceSuccess(){
		if (!success [5]) {
			Debug.Log ("Liaison Covalente");
			success [5] = true;
		}
	}
}
