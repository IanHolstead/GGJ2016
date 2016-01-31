using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerMotion : MonoBehaviour
{
    //Parameters
    public float jumpHeight = 1600f;
    public float maxVelocityX = 5f;
    public float maxHeight = 12f;
    
    //debug
    public bool isGrounded = false, isPlayered = false;
    private bool isHoldingJump = false;

    public int playerID;
    public int flyingSpeedDenominator = 1;
    private string playerCode = "P";
    private Vector3 rotation;
    
    private bool hasDJumped = false;

    //components
    Rigidbody2D rb;
    PlayerVote playerVote;
    PlayerOrb playerOrb;

    GamePadState gamePadState;
    PlayerIndex playerIndex;

    bool isShocked = false;
    float timeSinceShockBegan = 0f;

    private int playersShocked = 1;
    public static float shockTime = 10f;

    public bool hasDoubleJump = false;
    public bool hasFastMovement = false;
    public bool hasJump = false;

    bool isDead = false;
    Vector3 respawnLocation = new Vector3();
    public float respawnDelay = 1.0f;
    private float timeSinceDeath = 0.0f;

    // Use this for initialization
    void Start()
    {
        playerIndex = (PlayerIndex)playerID;
        respawnLocation = transform.position;
        playerCode = playerCode + playerID + "_";

        rb = GetComponent<Rigidbody2D>();
        playerVote = GetComponent<PlayerVote>();
        playerOrb = GetComponent<PlayerOrb>();
    }

    // Update is called once per frame
    void Update()
    {
        gamePadState = GamePad.GetState(playerIndex);
        Respawn();
        isHoldingJump = gamePadState.Buttons.A == ButtonState.Pressed;
        Movement();
        unShock();

        playerOrb.Use(gamePadState);
        playerVote.CallVote(gamePadState);
    }

    void Respawn()
    {
        if (isDead)
        {
            timeSinceDeath += Time.deltaTime;
            if (timeSinceDeath >= respawnDelay)
            {
                rb.velocity = new Vector3(0, 0, 0);
                transform.position = respawnLocation;
                GetComponent<SpriteRenderer>().enabled = true;
                isDead = false;
            }
        }
    }

    public void Die()
    {
        timeSinceDeath = 0;
        isDead = true;
        GetComponent<SpriteRenderer>().enabled = false;
        PlayerOrb playerOrb = GetComponent<PlayerOrb>();
        Orb orb = playerOrb.Orb;
        if (orb != null)
        {
            orb.GetComponent<SpriteRenderer>().enabled = true;
            orb.GetComponent<BoxCollider2D>().enabled = true;
            playerOrb.RemoveOrb();
        }
    }

    public void SetRespawnLocation(Vector3 newLocation)
    {
        respawnLocation = newLocation;
    }

    void Movement()
    {
        float velocityX, y;
        bool jumping = false;
        float inputX = gamePadState.ThumbSticks.Left.X * 50;
        float jump = 0f;
        if (isShocked)//move
        {
            inputX = 0f;
        }
        velocityX = rb.velocity.x;
        if (velocityX > maxVelocityX)
        {
            inputX = maxVelocityX - velocityX;
        }
        else if (-velocityX > maxVelocityX)
        {
            inputX = maxVelocityX + velocityX;
        }
        if (isGrounded ^ isPlayered)
        {
            jumping = isJump();
            if (jumping)
            {
                //Debug.Log ("I am jumping");
                jump = jumpHeight;
                isGrounded = false;
            }
        }
        else if (!isGrounded)
        {
            inputX = inputX / flyingSpeedDenominator;
        }
        if (!isHoldingJump && !isGrounded && !isPlayered && !hasDJumped && hasDoubleJump)
        {
            //Debug.Log ("I can DJump");
            jumping = isJump();
            if (jumping)
            {
                //Debug.Log ("I am Djumping");
                jump = jump + jumpHeight * 0.75f;
                isGrounded = false;
                hasDJumped = true;
            }
        }
        if (hasFastMovement)
        {
            inputX *= 2;
        }
        rb.velocity += (new Vector2(inputX, 0) * Time.deltaTime);
        rb.velocity += (new Vector2(0, jump) * Time.deltaTime);
        y = rb.velocity.y;
        velocityX = rb.velocity.x;
        if (isShocked)
        {
            velocityX = 0;
        }
        if (y > maxHeight && !hasDJumped)
        {
            rb.velocity = new Vector2(velocityX, maxHeight);
        }
        else if (y > 1.2f * maxHeight && hasDJumped)
        {
            rb.velocity = new Vector2(velocityX, maxHeight * 1.2f);
        }
    }

    bool isJump()
    {
        if (!hasJump || isShocked)
        {
            return false;
        }
        if (gamePadState.Buttons.A == ButtonState.Pressed)
        {
            return true;
        }
        else {
            return false;
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
		Debug.Log ("C:" + c);
		Debug.Log ("col: " + c.collider);
		Debug.Log ("mat: " + c.collider.sharedMaterial);
        if (c.collider.sharedMaterial.name == "Ground")
        {
            isGrounded = true;
        }
        if (c.collider.sharedMaterial.name == "Player")
        {
            isPlayered = true;
        }
    }

    void OnCollisionStay2D(Collision2D c)
    {
        if (c.collider.sharedMaterial.name == "Ground")
        {
            isGrounded = true;
        }
        if (c.collider.sharedMaterial.name == "Player")
        {
            isPlayered = true;
        }
    }

    void OnCollisionExit2D(Collision2D c)
    {
        if (c.collider.sharedMaterial.name == "Ground")
        {
            isGrounded = false;
            hasDJumped = false;
        }
        if (c.collider.sharedMaterial.name == "Player")
        {
            isPlayered = false;
        }
    }

    void logBool(bool b, string tr, string fa)
    {
        if (b)
        {
            Debug.Log(tr);
        }
        else {
            Debug.Log(fa);
        }
    }

    public void Shock(int numberOfPlayers)
    {
        isShocked = true;
        //rb.gravityScale = 0;
        timeSinceShockBegan += Time.deltaTime;
        playersShocked = numberOfPlayers;
    }

    private void unShock()
    {
		if (isShocked) {
			timeSinceShockBegan += Time.deltaTime;
		}
		Debug.Log (timeSinceShockBegan);
		Debug.Log (shockTime / playersShocked);
		if (timeSinceShockBegan > (shockTime / playersShocked))
        {
            isShocked = false;
			timeSinceShockBegan = 0f;
            //rb.gravityScale = 1;
        }
    }
}