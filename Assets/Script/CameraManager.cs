using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	private GameObject target = null;
	private float wait = 0.0f;
	private bool isWaiting = false;
	private GameObject[] atomeList = null;
	// Use this for initialization
	void Start () {
		atomeList = GameObject.FindGameObjectsWithTag ("atome");
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			transform.position = Vector3.Lerp (transform.position, target.transform.position, Time.deltaTime*2);
			transform.position = new Vector3 (transform.position.x, transform.position.y, -10.0f);
		} else {
			if (!isWaiting) {
				wait = Time.time;
				isWaiting = true;
			}
			else {
				if (Time.time - wait > 3.0f)
					foreach(GameObject o in atomeList)
						if (o.GetComponent<atomeController>().currentPos){
							target = o;
							isWaiting = false;
						}
			}
		}
	}

	public void setTarget(GameObject t){
		target = t;
	}
}
