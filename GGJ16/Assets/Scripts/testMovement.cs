using UnityEngine;
using System.Collections;

public class testMovement : MonoBehaviour {

    public bool moveRight = true;
    public bool moveRightRigidBody = true;
    public Vector3 respawn = new Vector3(-3, 1, 0);
    public float respawnDelay = 1.0f;
    private float respawnTime = 0.0f;

    // Use this for initialization
    void Start () {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (moveRight)
        {
            rb.velocity = new Vector3(2, 0, 0);
        }        
    }
	
	// Update is called once per frame
	void Update () {
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
        if (moveRightRigidBody)
        {
            transform.position = transform.position + new Vector3(0.1f, 0, 0);
        }
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (Input.GetKeyDown(KeyCode.X))
        {
            //rb.velocity = rb.velocity * -2;
            rb.AddForce(rb.velocity * -2, ForceMode2D.Impulse);
            if (rb.velocity == new Vector2(0,0))
            {
                rb.AddForce(new Vector3(2, 0, 0), ForceMode2D.Impulse); 
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            rb.AddForce(new Vector3(-2, 0, 0), ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
}
