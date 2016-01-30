using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {

    public Vector3 respawn = new Vector3(-3, 1, 0);

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        Debug.Log("Lava Collision!");
        if(otherObj.gameObject.tag == "Player")
        {
            otherObj.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            otherObj.transform.position = respawn;
        }
    }

}
