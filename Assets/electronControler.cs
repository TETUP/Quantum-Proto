using UnityEngine;
using System.Collections;

public class electronControler : MonoBehaviour {

	GameObject _atome = null;
	Rigidbody _rb = null;
	public float _couche = 1;
	public float position = 1.0f;
	public float rotateSpeed = 1.0f;
	private bool leashed = true;
	private Light _lgt = null;
	private float delay = 0.0f;
	void Awake(){
		_lgt = GetComponent<Light> ();
	}

	// Use this for initialization
	void Start () {
		_atome = transform.parent.gameObject;
		_rb = GetComponent<Rigidbody> ();
		//init e-
		transform.localPosition = new Vector3(0.0f, position, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (leashed) {
			transform.RotateAround (_atome.transform.position, new Vector3 (0, 0, 1), -rotateSpeed * Time.deltaTime);

		}
		if (!leashed && delay + 5.0f <= Time.time)
			Destroy (this.gameObject);
	}

	void ChangeOrbit(float rayon){
		transform.localPosition = new Vector3 (0.0f, rayon, 0.0f);
		transform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
	}

	public void Unleash (){
		leashed = false;
		_rb.AddRelativeForce (rotateSpeed, 0.0f, 0.0f);
		delay = Time.time;
	}

	public void LoadElec (int coucheMax){
		position += 0.5f;
		_couche++;
		ChangeOrbit (position);
	}

	public void ActivateColor (){
		Debug.Log (_lgt.name);
		_lgt.color = Color.yellow;
	}

	public void RefillColor(){
		_lgt.color = Color.green;
	}
}
