using UnityEngine;
using System.Collections;

public class PlayerMotion : MonoBehaviour
{
	public bool isGrounded = false, isPlayered = false;
	private int jumpsLeft = 2;
	public int playerID;
	public int flyingSpeedDenominator = 1;
	private string playerCode = "P";
	private Vector3 rotation;
	private float vertVel;

    public bool hasDoubleJump = false;
    public bool hasFastMovement = false;

	// Use this for initialization
	void Start ()
	{
		playerCode = playerCode + playerID + "_";
	}
	
	// Update is called once per frame
	void Update ()
	{
		vertVel = GetComponent<Rigidbody2D> ().velocity.y;
		Movement ();
		Debug.Log (jumpsLeft);
	}

	void Movement ()
	{
		float hor = Input.GetAxis (playerCode + "Horizontal") * 50;
		float jump = 0f;
		if (isGrounded ^ isPlayered || jumpsLeft > 0 ) {
			if (playerID == 0 && Input.GetKey (KeyCode.Joystick1Button0)) {
				jump = 500f;
				isGrounded = false;
				jumpsLeft = jumpsLeft > 0 ? jumpsLeft - 1 : 0;
			} else if (playerID == 1 && Input.GetKey (KeyCode.Joystick2Button0)) {
				jump = 500f;
				isGrounded = false;
				jumpsLeft = jumpsLeft > 0 ? jumpsLeft - 1 : 0;
			} else if (playerID == 2 && Input.GetKey (KeyCode.Joystick3Button0)) {
				jump = 500f;
				isGrounded = false;
				jumpsLeft = jumpsLeft > 0 ? jumpsLeft - 1 : 0;
			} else if (playerID == 3 && Input.GetKey (KeyCode.Joystick4Button0)) {
				jump = 500f;
				isGrounded = false;
				jumpsLeft = jumpsLeft > 0 ? jumpsLeft - 1 : 0;
			}
		} else {
			hor = hor / flyingSpeedDenominator;
		}
		GetComponent<Rigidbody2D> ().velocity += (new Vector2 (hor, 0) * Time.deltaTime);
		GetComponent<Rigidbody2D> ().velocity += (new Vector2 (0, jump) * Time.deltaTime);
	}

	void OnCollisionEnter2D (Collision2D c)
	{
		if (c.collider.sharedMaterial.name == "Ground") {
			isGrounded = true;
			jumpsLeft = jumpsLeft <= 2 ? jumpsLeft + 1 : 2;
		}
		if (c.collider.sharedMaterial.name == "Player") {
			isPlayered = true;
		}
	}

	void OnCollisionStay2D (Collision2D c)
	{
		if (c.collider.sharedMaterial.name == "Ground") {
			isGrounded = true;
			jumpsLeft = jumpsLeft < 2 ? jumpsLeft + 1 : 2;
		}
		if (c.collider.sharedMaterial.name == "Player") {
			isPlayered = true;
		}
	}

	void OnCollisionExit2D (Collision2D c)
	{
		if (c.collider.sharedMaterial.name == "Ground") {
			isGrounded = false;
		}
		if (c.collider.sharedMaterial.name == "Player") {
			isPlayered = false;
		}
	}
}