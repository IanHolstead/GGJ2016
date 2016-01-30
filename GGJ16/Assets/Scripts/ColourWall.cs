using UnityEngine;
using System.Collections;

public class ColourWall : MonoBehaviour {

    public Color colour;

	// Use this for initialization
	void Start () {
        Debug.Log("Test");
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Player")
        {
            PlayerOrb player = otherObj.gameObject.GetComponent<PlayerOrb>();
            if (player.GetColour() == colour)
            {
                Collider2D playerCol = player.GetComponent<BoxCollider2D>();
                Physics2D.IgnoreCollision(playerCol, this.GetComponent<BoxCollider2D>());
                Debug.Log("Ignore");
            }
        }
    }

    void OnTriggerExit2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Player")
        {
            PlayerOrb player = otherObj.gameObject.GetComponent<PlayerOrb>();
           
            Collider2D playerCol = player.GetComponent<BoxCollider2D>();
            Physics2D.IgnoreCollision(playerCol, this.GetComponent<BoxCollider2D>(), false);

            Debug.Log("Exit");
        }
    }
}
