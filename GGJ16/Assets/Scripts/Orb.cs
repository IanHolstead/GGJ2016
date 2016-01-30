using UnityEngine;
using System.Collections.Generic;

public class Orb : MonoBehaviour {
    public Color colour;

    float initialHeight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        hover();
	}

    void hover() {

    }

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        Debug.Log("Orb Collision!");
        if(otherObj.gameObject.tag == "Player")
        {
            otherObj.gameObject.GetComponent<SpriteRenderer>().sprite = otherObj.gameObject.GetComponent<PlayerOrb>().redSprite;
        }
    }
}
