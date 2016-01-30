using UnityEngine;
using System.Collections;

public class Vote : MonoBehaviour
{
	private Transform[] boxes = new Transform[4];

	int p1vote = -1;

	// Use this for initialization
	void Start ()
	{
	 	boxes[0] = gameObject.transform.FindChild("P1Box");
	 	boxes[1] = gameObject.transform.FindChild("P2Box");
	 	boxes[2] = gameObject.transform.FindChild("P3Box");
	 	boxes[3] = gameObject.transform.FindChild("P4Box");
		boxes[0].GetComponent<SpriteRenderer> ().enabled = false;
		boxes[1].GetComponent<SpriteRenderer> ().enabled = false;
		boxes[2].GetComponent<SpriteRenderer> ().enabled = false;
		boxes[3].GetComponent<SpriteRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log ("Update!");
		if (Input.GetAxis("GamePadVertical") > 0) {
			p1vote = 0;
		}
		if (Input.GetAxis("GamePadHorizontal") > 0) {
			p1vote = 1;
		}
		if (Input.GetAxis("GamePadVertical") < 0) {
			p1vote = 2;
		}
		if (Input.GetAxis("GamePadHorizontal") < 0) {
			p1vote = 3;
		}
		if (p1vote != -1) {
			for (int i = 0; i < 4; i++) {
				boxes[i].GetComponent<SpriteRenderer> ().enabled = false;
			}
			boxes[p1vote].GetComponent<SpriteRenderer> ().enabled = true;
		}
	}
}

