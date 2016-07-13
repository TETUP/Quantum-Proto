using UnityEngine;
using System.Collections;

public class colorScript : MonoBehaviour {

	[Range(0.0F, 1.0f)]
	public float hue = 0.0f;
	private SpriteRenderer _halo = null;

	// Use this for initialization
	void Start () {
		_halo = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		_halo.color = Color.HSVToRGB (hue, Mathf.Cos(Time.time)*0.25f + 1, 1);
	}
}
