using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        Debug.Log("Lava Collision!");
        if (otherObj.gameObject.tag == "Player")
        {
            otherObj.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

}
