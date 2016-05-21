using UnityEngine;
using System.Collections;

public class electronControler : MonoBehaviour {

	GameObject _atome = null;
	Rigidbody _rb = null;
	public float _couche = 1.0f; //numéro de la couche sur laquelle l'électron orbite
	public float position = 1.0f; //Position en terme spatial
	public float rotateSpeed = 1.0f;
	public bool leashed = true;
	private Light _lgt = null;
	public float delay = 0.0f;
	private Collider _c = null;
	private bool clockwise = true;
	public TrailRenderer _tRender = null;

    void Awake(){
		_lgt = GetComponent<Light> ();
		_tRender = GetComponent<TrailRenderer> ();
	}

	// Use this for initialization
	void Start () {
		_atome = transform.parent.gameObject;
		_rb = GetComponent<Rigidbody> ();
		//init e-
		transform.localPosition = new Vector3(0.0f, position, 0.0f);
		_c = GetComponent<Collider> ();
		GameObject[] allElec = GameObject.FindGameObjectsWithTag ("electron");
		foreach (GameObject elecCurrent in allElec) {
			if (elecCurrent.GetComponent<Collider>() != null)
				Physics.IgnoreCollision (elecCurrent.GetComponent<Collider>(), _c);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (leashed) {
			if (clockwise)
				transform.RotateAround (_atome.transform.position, new Vector3 (0, 0, 1), -rotateSpeed * Time.deltaTime);
			else
				transform.RotateAround (_atome.transform.position, new Vector3 (0, 0, 1), rotateSpeed * Time.deltaTime);
		} else {
			transform.LookAt (transform.position + _rb.velocity);
		}

		if (!leashed && delay + 5.0f <= Time.time) {
			Destroy (this.gameObject);
		}
	}

	void ChangeOrbit(float rayon){
		transform.localPosition = new Vector3 (0.0f, rayon, 0.0f);
		transform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
	}

	public void Unleash (){
		leashed = false;
		if (clockwise)
			_rb.AddRelativeForce (position*rotateSpeed, 0.0f, 0.0f);
		else
			_rb.AddRelativeForce (-position*rotateSpeed, 0.0f, 0.0f);
		delay = Time.time;
	}

	public void LoadElec (int coucheMax){
		position += 0.5f;
		_couche++;
		ChangeOrbit (position);
	}

	public void ActivateColor (){
		_lgt.color = Color.yellow;
		_tRender.material.SetColor("_TintColor", Color.yellow);
	}

	public void RefillColor(){
		_lgt.color = Color.green;
		_tRender.material.SetColor("_TintColor", Color.green);
	}

	void OnCollisionEnter (Collision c){
		temporise ();
	}

	void OnDestroy(){
		Instantiate (Resources.Load("ParticleEnd"), transform.position, transform.rotation);
	}

	public void SetClockwise(bool b){
		clockwise = b;
	}

	public void temporise(){
		delay = Time.time;
	}
    }
