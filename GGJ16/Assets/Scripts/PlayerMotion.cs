using UnityEngine;
using System.Collections;

public class PlayerMotion : MonoBehaviour
{
	public bool isGrounded = false, isPlayered = false;
	public int playerID;
	public int flyingSpeedDenominator = 1;
	private string playerCode = "P";
	private Vector3 rotation;
	private bool isHoldingJump = false;
	private bool hasDJumped = false;
	private Rigidbody2D rigidbody;

	public bool hasDoubleJump = false;
	public bool hasFastMovement = false;
    public bool hasJump = false;

    public Vector3 respawn = new Vector3(-3, 1, 0);
    public float respawnDelay = 1.0f;
    private float respawnTime = 0.0f;

    // Use this for initialization
    void Start ()
	{
		playerCode = playerCode + playerID + "_";
		rigidbody = GetComponent<Rigidbody2D> ();
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

		Movement ();
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
		//logBool (isHoldingJump, "Holding Jump", "Not Holding Jump");
	}

	void Movement ()
	{
		float jumpHeight = 350f;
		float maxHor = 5f;
		float maxHeight = 6f;
		float x, y;
		bool jumping = false;
		float hor = Input.GetAxis (playerCode + "Horizontal") * 50;
		float jump = 0f;
		x = rigidbody.velocity.x;
		if (x > maxHor) {
			hor = maxHor - x;
		} else if (-x > maxHor) {
			hor = maxHor + x;
		}
		if (isGrounded ^ isPlayered) {
			jumping = isJump ();
			if (jumping) {
				//Debug.Log ("I am jumping");
				jump = jumpHeight;
				isGrounded = false;
			}
		} else if (!isGrounded) {
			hor = hor / flyingSpeedDenominator;
		}
		if (!isHoldingJump && !isGrounded && !isPlayered && !hasDJumped && hasDoubleJump) {
			//Debug.Log ("I can DJump");
			jumping = isJump ();
			if (jumping) {
				//Debug.Log ("I am Djumping");
				jump = jump + jumpHeight * 0.75f;
				isGrounded = false;
				hasDJumped = true;
			}
		}
		if (hasFastMovement) {
			hor *= 2;
		}
		rigidbody.velocity += (new Vector2 (hor, 0) * Time.deltaTime);
		rigidbody.velocity += (new Vector2 (0, jump) * Time.deltaTime);
		y = rigidbody.velocity.y;
		x = rigidbody.velocity.x;
		if (y > maxHeight && !hasDJumped) {
			rigidbody.velocity = new Vector2 (x, maxHeight);
		}
	}

	bool isJump(){
        if (!hasJump) {
            return false;
        }
        if (playerID == 0 && Input.GetKeyDown (KeyCode.Joystick1Button0)) {
			return true;
		} else if (playerID == 1 && Input.GetKeyDown (KeyCode.Joystick2Button0)) {
			return true;
		} else if (playerID == 2 && Input.GetKeyDown (KeyCode.Joystick3Button0)) {
			return true;
		} else if (playerID == 3 && Input.GetKeyDown (KeyCode.Joystick4Button0)) {
			return true;
		} else {
			return false;
		}
	}

	void OnCollisionEnter2D (Collision2D c)
	{
		if (c.collider.sharedMaterial.name == "Ground") {
			isGrounded = true;
		}
		if (c.collider.sharedMaterial.name == "Player") {
			isPlayered = true;
		}
	}

	void OnCollisionStay2D (Collision2D c)
	{
		if (c.collider.sharedMaterial.name == "Ground") {
			isGrounded = true;
		}
		if (c.collider.sharedMaterial.name == "Player") {
			isPlayered = true;
		}
	}

	void OnCollisionExit2D (Collision2D c)
	{
		if (c.collider.sharedMaterial.name == "Ground") {
			isGrounded = false;
			hasDJumped = false;
		}
		if (c.collider.sharedMaterial.name == "Player") {
			isPlayered = false;
		}
	}

	void logBool(bool b, string tr, string fa){
		if (b) {
			Debug.Log (tr);
		} else {
			Debug.Log (fa);
		}
	}
}