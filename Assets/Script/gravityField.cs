﻿using UnityEngine;
using System.Collections;

public class gravityField : MonoBehaviour {

	public float intensity = 10.0f;
	SphereCollider _sphCldr = null;
	int _orbitMax = 0;
	private float delay = 0.0f;

	void Awake () {
		_sphCldr = GetComponent<SphereCollider> ();
	}

	// Use this for initialization
	void Start () {
		for (int i = 0; i < transform.childCount; i++) {
			if (transform.GetChild(i).gameObject.activeSelf)
				_orbitMax++;
		}
		switch(_orbitMax){
		case 1:
			_sphCldr.radius = 1.0f;
			break;
		case 2:
			_sphCldr.radius = 1.5f;
			break;
		case 3:
			_sphCldr.radius = 2.0f;
			break;
		default:
			_sphCldr.radius = 1.0f;
			break;
		}
	}

	public void DesactivateField(){
		_sphCldr.enabled = false;
	}

	void OnTriggerEnter (Collider other){
		delay = Time.time;
	}

	void OnTriggerStay (Collider other){
		if (other.transform.parent == null)
			other.attachedRigidbody.AddForce (intensity * (transform.position - other.transform.position));
		if (!other.GetComponent<electronControler>().leashed && delay + 1.0f < Time.time)
			successBehavior.valenceSuccess ();
	}
}
