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
	private bool isHoldingJump = false;

	public bool hasDoubleJump = false;
	public bool hasFastMovement = false;

    public Vector3 respawn = new Vector3(-3, 1, 0);
    public float respawnDelay = 1.0f;
    private float respawnTime = 0.0f;

    // Use this for initialization
    void Start ()
	{
		playerCode = playerCode + playerID + "_";
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (GetComponent<SpriteRenderer>().enabled == false)
        {
            respawnTime += Time.deltaTime;
            if (respawnTime >= respawnDelay)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                transform.position = respawn;
                GetComponent<SpriteRenderer>().enabled = true;
                respawnTime = 0.0f;
            }
        }

        vertVel = GetComponent<Rigidbody2D> ().velocity.y;
		Movement ();
		Debug.Log (jumpsLeft);
		if (playerID == 0 && Input.GetKeyDown (KeyCode.Joystick1Button0)) {
			isHoldingJump = true;
		} else if (playerID == 1 && Input.GetKeyDown (KeyCode.Joystick2Button0)) {
			isHoldingJump = true;
		} else if (playerID == 2 && Input.GetKeyDown (KeyCode.Joystick3Button0)) {
			isHoldingJump = true;
		} else if (playerID == 3 && Input.GetKeyDown (KeyCode.Joystick4Button0)) {
			isHoldingJump = true;
		} else if (playerID == 0 && Input.GetKeyUp (KeyCode.Joystick1Button0)) {
			isHoldingJump = false;
		} else if (playerID == 1 && Input.GetKeyUp (KeyCode.Joystick2Button0)) {
			isHoldingJump = false;
		} else if (playerID == 2 && Input.GetKeyUp (KeyCode.Joystick3Button0)) {
			isHoldingJump = false;
		} else if (playerID == 3 && Input.GetKeyUp (KeyCode.Joystick4Button0)) {
			isHoldingJump = false;
		}
	}

	void Movement ()
	{
		float jumpHeight = 300f;
		bool jumping = false;
		float hor = Input.GetAxis (playerCode + "Horizontal") * 50;
		float jump = 0f;
		if (isGrounded ^ isPlayered) {
			if (playerID == 0 && Input.GetKey (KeyCode.Joystick1Button0)) {
				jumping = true;
			} else if (playerID == 1 && Input.GetKey (KeyCode.Joystick2Button0)) {
				jumping = true;
			} else if (playerID == 2 && Input.GetKey (KeyCode.Joystick3Button0)) {
				jumping = true;
			} else if (playerID == 3 && Input.GetKey (KeyCode.Joystick4Button0)) {
				jumping = true;
			}
			if (jumping) {
				Debug.Log ("I am jumping");
				jump = jumpHeight;
				isGrounded = false;
				jumpsLeft = jumpsLeft > 0 ? jumpsLeft - 1 : 0;
			}
		} else {
			hor = hor / flyingSpeedDenominator;
		}
		if (!isGrounded && !isPlayered && !isHoldingJump && jumpsLeft > 0) {
			Debug.Log ("I am able to DJump");
			jumping = isJump ();
			if (jumping) {
				jump = jumpHeight;
				isGrounded = false;
				jumpsLeft = jumpsLeft > 0 ? jumpsLeft - 1 : 0;
			}
		}
		GetComponent<Rigidbody2D> ().velocity += (new Vector2 (hor, 0) * Time.deltaTime);
		GetComponent<Rigidbody2D> ().velocity += (new Vector2 (0, jump) * Time.deltaTime);
	}

	bool isJump(){
		if (playerID == 0 && Input.GetKey (KeyCode.Joystick1Button0)) {
			return true;
		} else if (playerID == 1 && Input.GetKey (KeyCode.Joystick2Button0)) {
			return true;
		} else if (playerID == 2 && Input.GetKey (KeyCode.Joystick3Button0)) {
			return true;
		} else if (playerID == 3 && Input.GetKey (KeyCode.Joystick4Button0)) {
			return true;
		}
        return false;
	}

	void OnCollisionEnter2D (Collision2D c)
	{
		if (c.collider.sharedMaterial.name == "Ground") {
			isGrounded = true;
			jumpsLeft = jumpsLeft < 2 ? jumpsLeft + 1 : 2;
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