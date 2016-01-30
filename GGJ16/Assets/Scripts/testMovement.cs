using UnityEngine;
using System.Collections;

public class testMovement : MonoBehaviour {

    public bool moveRight = true;
    public bool moveRightRigidBody = true;

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
