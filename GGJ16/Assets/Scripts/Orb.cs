using UnityEngine;
using System.Collections.Generic;

public class Orb : MonoBehaviour {
    public Color colour;

    float initialHeight;
    public float floatFrequency = 1f;
    public float floatHeight = 1f;
    float occilator = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        hover();
	}

    void hover() {
        occilator += Time.deltaTime;
        float height = initialHeight + floatHeight + floatHeight * Mathf.Sin(occilator / floatFrequency);
        transform.position = new Vector3(transform.position.x, height);
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
