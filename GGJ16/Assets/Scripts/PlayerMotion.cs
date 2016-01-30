using UnityEngine;
using System.Collections;

public class PlayerMotion : MonoBehaviour {
	static private float distToGround = 0f;
	public int playerID;
	public int flyingSpeedDenominator = 1;
	private string playerCode = "P";
	private Vector3 rotation;
	private float vertVel;

	// Use this for initialization
	void Start () {
		playerCode = playerCode + playerID + "_";
		distToGround = GetComponent<PolygonCollider2D> ().bounds.extents.y;
	}
	
	// Update is called once per frame
	void Update () {
		vertVel = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y);
		if (flyingSpeedDenominator == 0) {
			flyingSpeedDenominator = 1;
		}
		Movement ();
		HoldRotation ();
	}

	void Movement(){
		float hor = Input.GetAxis (playerCode + "Horizontal") * 1000;
		float jump = 0f;
		Debug.Log (isGrounded ());
		if (isGrounded()) {
			jump = Input.GetAxis (playerCode + "Jump") * 10000;
		} else {
			hor = hor / flyingSpeedDenominator;
		}
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (hor, jump) * Time.deltaTime);
	}

	void HoldRotation(){
		rotation = transform.eulerAngles;
		float z = rotation.z;
		float y = rotation.y;
		float x = rotation.x;
		if (15 < z && z < 180) {
			transform.Rotate (new Vector3 (0, 0, 15 - z));
		} else if (rotation.z < 345 && rotation.z >= 180) {
			transform.Rotate (new Vector3 (0, 0, 345 - z));
		}
		z = transform.eulerAngles.z;
		if (!isGrounded ()) {
			transform.Rotate (new Vector3 (0, 0, -z));
		}
	}

	bool isGrounded(){
		return vertVel < 0.01f;
	}
}
