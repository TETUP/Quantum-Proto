using UnityEngine;
using System.Collections;

public class blackholeControler : MonoBehaviour {

	public GameObject _dest = null;
	private blackholeControler _bhCtrlDest = null;
	private GameObject field = null;
	private float delay = 0.5f;
	private float spawn = 0.0f;
	private float exit = 0.0f;
	// Use this for initialization
	void Start () {
		if (_dest != null) {
			_bhCtrlDest = _dest.GetComponent<blackholeControler> ();
			_bhCtrlDest._dest = gameObject;
			field =_bhCtrlDest.gameObject.transform.GetChild(0).gameObject;
		}
		else
			Debug.Log("Please link Blackhole between them");
	}

	void Update() {
		if (!field.activeSelf && spawn + delay < Time.time) {
			field.SetActive (true);
			_dest.GetComponent<Collider> ().enabled = true;
		}
	}
	
	void OnTriggerEnter (Collider c){
		Vector3 v = c.GetComponent<Rigidbody> ().velocity;
		field.SetActive (false);
		_dest.GetComponent<Collider> ().enabled = false;
		spawn = Time.time;
		c.transform.position = _dest.transform.position;
		soundController.play (7);
	}

	void OnTriggerExit (Collider c){
		if ((spawn - exit) < 1.0f) {
			successBehavior.accelerateurSuccess ();
			/*soundController.play (6);
			soundController.pitchUp (6, 0.5f);*/
		}
		exit = Time.time;
	}
}
