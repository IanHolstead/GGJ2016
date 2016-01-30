using UnityEngine;
using System.Collections;

public class PlayerOrb : MonoBehaviour {

    public Color colour; //just for testing
    public Sprite redSprite;
    Orb orb;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.C))
        {
            if (colour == new Color(255,0,0))
            {
                colour = new Color();
            }
            else
            {
                colour = new Color(255, 0, 0);
            }
        }

    }

    void OnCollisionEnter2D(Collision2D otherObj)
    {
        if (otherObj.gameObject.tag == "ColourWall")
        {

        }
        else if (otherObj.gameObject.tag == "Orb")
        {
            Orb orb = otherObj.gameObject.GetComponent<Orb>();
        }
        else if (otherObj.gameObject.tag == "Shrine")
        {

        }
    }

    void PickUpOrb(Orb pickupOrb)
    {
        colour = pickupOrb.colour;
        orb = pickupOrb;
        //hide orb in level
    }

    void SwapOrb(Orb newOrb, Orb oldOrb)
    {
        oldOrb.transform.position = newOrb.transform.position;
        colour = newOrb.colour;
    }

    void PlaceOrb(Orb orb)
    {
        colour = new Color();
        //send message no double jump
    }

    public Color GetColour()
    {
        return colour;
    }

    public Orb RemoveOrb()
    {
        Orb toReturn = orb;
        orb = null;
        return toReturn;
    }
}
