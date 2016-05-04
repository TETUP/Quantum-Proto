using UnityEngine;
using System.Collections;

public class atomeController : MonoBehaviour {

	public int _coucheMax = 1;
	public bool touchable = false;

	GameObject _orbit = null;
	GameObject _electron = null;
	electronControler _elctrl = null;
	Light _lgt = null;
	Renderer _rnder = null;
	// Use this for initialization

	void Awake () {
		//Init Gameobjects
		int i = 0;
		_orbit = transform.GetChild (0).gameObject;
		while (i < transform.childCount) {
			if (transform.GetChild (i).gameObject.name == "Orbit")
				_orbit = transform.GetChild (i).gameObject;
			else if (transform.GetChild (i).gameObject.name == "electron") {
				_electron = transform.GetChild (i).gameObject;
				_elctrl = _electron.GetComponent<electronControler> ();
			}
			i++;
		}

	}
	void Start () {
		//Active orbits
		int i = 0;
		for (i = 0; i < _coucheMax; i++)
			_orbit.transform.GetChild (i).gameObject.SetActive (true);

		_lgt = GetComponent<Light> ();
		_rnder = GetComponent<Renderer> ();

		if (touchable)
			Activate ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider c){
		if (c.gameObject.name == "electron" && _electron != null) {
			if (_elctrl._couche < _coucheMax) {
				_elctrl.LoadElec (_coucheMax);
				if (_elctrl._couche == _coucheMax) {
					touchable = true;
					Activate ();
				}
			}
		}
			
	}

	void Activate(){
		touchable = true;
		_lgt.color = Color.yellow;
		_rnder.material = _rnder.materials [1];
		_elctrl.ActivateColor();
	}

	void OnMouseDown (){
		if (touchable) {
			_elctrl.Unleash ();
			_electron.transform.parent = null;
			_electron = null;
			_elctrl = null;
		}
	}
}
