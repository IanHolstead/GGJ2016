using UnityEngine;
using System.Collections;

public class BoxBlink : MonoBehaviour {

	public KeyCode KeyToPress;
	// Use this for initialization
	void Start () {
		Debug.Log ("Start", this);
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Update", this);
		if (Input.GetKey(KeyToPress)) {
			gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		} else {
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		}
	}
}
