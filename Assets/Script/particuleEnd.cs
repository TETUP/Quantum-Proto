using UnityEngine;
using System.Collections;

public class particuleEnd : MonoBehaviour {

	private float lifetime = 1.0f;
	private float timespawn = 0.0f;
	// Use this for initialization
	void Start () {
		timespawn = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (timespawn + lifetime <= Time.time)
			Destroy(gameObject);
	}
}
