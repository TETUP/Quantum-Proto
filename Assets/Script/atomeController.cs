using UnityEngine;
using System.Collections;

public class atomeController : MonoBehaviour {

	public int _coucheMax = 1;
	public bool touchable = false;
	public bool refill = false;
	public GameObject prefabE = null;

	GameObject _orbit = null;
	gravityField _orbctrl = null;
	GameObject _electron = null;
	electronControler _elctrl = null;
	Light _lgt = null;
	Renderer _rnder = null;
	float delay = 0.0f;
	public bool clockwise = true;
	private bool mousedown = false;
	// Use this for initialization

	void Awake () {
		//Init Gameobjects
		int i = 0;
		_orbit = transform.GetChild (0).gameObject;
		while (i < transform.childCount) {
			if (transform.GetChild (i).gameObject.name == "Orbit") {
				_orbit = transform.GetChild (i).gameObject;
				_orbctrl = _orbit.GetComponent<gravityField> ();
			}
			i++;
		}
		CreateElectron ();
		//Active orbits
		i = 0;
		for (i = 0; i < _coucheMax; i++)
			_orbit.transform.GetChild (i).gameObject.SetActive (true);
		
	}
	void Start () {
		_lgt = GetComponent<Light> ();
		_rnder = GetComponent<Renderer> ();

		if (touchable)
			Activate ();
	}

	// Update is called once per frame
	void Update () {
		if (refill && _electron == null && delay + 3.0f <= Time.time) {
			CreateElectron ();
		}
		if (mousedown && Time.timeScale > 0.5f)
			Time.timeScale -= 0.01f;
	}

	void OnTriggerEnter (Collider c){
		if (c.gameObject.name == "electron") {
			bool explosion = false;
			if (c.GetComponent<Rigidbody> ().velocity.magnitude > 15.0f && c.GetComponent<Rigidbody> ().velocity.magnitude < 50.0f) {
				successBehavior.fissionSuccess ();
				explosion = true;
			} else if (c.GetComponent<Rigidbody> ().velocity.magnitude > 50.0f) {
				successBehavior.fusionSuccess ();
				explosion = true;
			}
			if (_electron != null) {
				if (_elctrl._couche < _coucheMax) {
					_elctrl.LoadElec (_coucheMax);
					if (!explosion) {
						soundController.reset (0);
					}
					soundController.nextMark ();
					if (_elctrl._couche == _coucheMax) {
						touchable = true;
						refill = true;
						Activate ();
						Camera.main.GetComponent<CameraManager> ().changeCamera ();
					}
				}
			}
			Destroy (c.gameObject);
		}
			
	}

	void CreateElectron (){
		GameObject tampon = Instantiate (prefabE);
		tampon.transform.parent = this.gameObject.transform;
		tampon.name = "electron";
		int i = 0;
		while (i < transform.childCount) {
			if (transform.GetChild (i).gameObject.name == "electron") {
				_electron = transform.GetChild (i).gameObject;
				_elctrl = _electron.GetComponent<electronControler> ();
				_elctrl.SetClockwise (clockwise);
				if (refill) {
					_elctrl._couche = _coucheMax;
					_elctrl.position = 0.5f * _coucheMax + 0.5f;
				}
			}
			i++;
		}
		if (refill) {
			_elctrl.RefillColor ();
		}

	}

	void Activate(){
		touchable = true;
		_orbctrl.DesactivateField ();
		if (refill) {
			_lgt.color = Color.green;
			_rnder.material = _rnder.materials [2];
		} 
		else {
			_lgt.color = Color.yellow;
			_rnder.material = _rnder.materials [1];
			_elctrl.ActivateColor();
		}

	}


	void OnMouseUp (){
		if (touchable) {
			if (!refill)
				soundController.resetMark ();
			if (_elctrl != null) {
				_elctrl.Unleash ();
				_electron.transform.parent = null;
				_electron = null;
				_elctrl = null;
			}
			delay = Time.time;
			Time.timeScale = 1.0f;
			mousedown = false;
		}
	}

	void OnMouseDown (){
		mousedown = true;
	}
}
