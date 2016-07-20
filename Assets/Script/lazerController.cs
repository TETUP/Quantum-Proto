using UnityEngine;
using System.Collections;

public class lazerController : MonoBehaviour {
	[Range(100.0f, 1000.0f)]
	public float force;

	// Use this for initialization
	void Start () {
	}

	void Update () {
	}

	void OnTriggerEnter(Collider c){
		if (c.tag == "electron" && !c.GetComponent<electronControler>().leashed) {
			successBehavior.dualitySuccess ();
			float mag = c.GetComponent<Rigidbody> ().velocity.x;
			Vector3 projection = Vector3.Dot (c.transform.position - transform.position, transform.right) * transform.right;
			Vector3 gravity = (c.transform.position - transform.position) - projection;
			c.GetComponent<Rigidbody> ().AddForce (force * gravity + 20 * transform.right);
		}
	}

	void OnTriggerStay(Collider c){
		if (c.tag == "electron" && !c.GetComponent<electronControler>().leashed) {
			c.GetComponent<electronControler> ().temporise ();
			Vector3 projection = Vector3.Dot(c.transform.position - transform.position, transform.right) * transform.right;
			Vector3 gravity = projection - (c.transform.position - transform.position);
			c.GetComponent<Rigidbody>().AddForce(force*gravity);
		}

	}

}
