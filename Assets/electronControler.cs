using UnityEngine;
using System.Collections;

public class electronControler : MonoBehaviour {

	GameObject _atome = null;
	Rigidbody _rb = null;
	public float rotateSpeed = 1.0f;
	private bool leashed = true;

	// Use this for initialization
	void Start () {
		_atome = transform.parent.gameObject;
		_rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (leashed)
			transform.RotateAround(_atome.transform.position, new Vector3(0, 0, 1), rotateSpeed * Time.deltaTime);
	}

	void ChangeOrbit(){
		
	}

	void Unleash (){
		leashed = false;
		_rb.AddForce (rotateSpeed, 0.0f, 0.0f);
	}
}
