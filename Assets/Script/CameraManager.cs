using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	private Transform[] targets;
	private Transform eTarget = null;
	private Vector3 targetPosition = Vector3.zero;
	private float wait = 0.0f;
	private bool isWaiting = false;
	private GameObject[] atomeList = null;
	private float _sizeCam = 5.0f;
	// Use this for initialization
	void Start () {
		atomeList = GameObject.FindGameObjectsWithTag ("atome");
		targets = new Transform[10];
	}
	
	// Update is called once per frame
	void Update () {
		if (targets != null && targetPosition != Vector3.zero) {
			if (eTarget == null)
				transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * 2);
			else
				transform.position = Vector3.Lerp (transform.position, eTarget.position, Time.deltaTime * 8);
			transform.position = new Vector3 (transform.position.x, transform.position.y, -10.0f);
			Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize,_sizeCam, Time.deltaTime*2);
				
		} else {
			if (!isWaiting) {
				wait = Time.time;
				isWaiting = true;
			}
			else {
				if (Time.time - wait > 3.0f){
					changeCamera ();
				}
			}
		}
	}

	public void changeCamera(){
		int i = 0;

		foreach (GameObject o in atomeList) {
			if (o.tag == "atome" && o.GetComponent<atomeController> ().touchable) {
				targets [i] = o.transform;
				isWaiting = false;
				i++;
			}

		}
		targetPosition = Vector3.zero;
		foreach (Transform t in targets) {
			if (t != null) {
				targetPosition =  (t.position/i) + targetPosition;
				_sizeCam = 4.0f + i*0.5f;
			}
		}
	}

	public void targetElectron(GameObject t){
		eTarget = t.transform;
	}

	public void untargetElectron(){
		eTarget = null;
	}
}
