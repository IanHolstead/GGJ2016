using UnityEngine;
using System.Collections;

public class testMovement : MonoBehaviour {

    public bool moveRight = true;

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
        if (Input.GetKey(KeyCode.X))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            //rb.velocity = rb.velocity * -2;
            rb.AddForce(rb.velocity * -2, ForceMode2D.Impulse);
        }
    }
}
