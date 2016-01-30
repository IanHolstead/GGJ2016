using UnityEngine;
using System.Collections;

public class PlayerOrb : MonoBehaviour {

    public Color colour; //just for testing
    public Sprite redSprite;
    public Sprite blueSprite;
    public Sprite greenSprite;
    public Sprite yellowSprite;
    public Sprite defaultSprite;
    Orb orb;

    private UsableObject useableObject;

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
        if (Input.GetKey(KeyCode.E))
        {
            useableObject.Use(this);
        }

    }

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Orb")
        {
            useableObject = otherObj.gameObject.GetComponent<Orb>();
        }
        else if (otherObj.gameObject.tag == "Shrine")
        {
            UsableObject shrine = otherObj.gameObject.GetComponent<Shrine>();
            if (shrine == null)
            {
                shrine = otherObj.gameObject.GetComponent<EvilShrine>();
            }
            useableObject = shrine;
        }
    }

    void OnTriggerExit2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Orb")
        {
            if (otherObj.gameObject.GetComponent<Orb>() == useableObject)
            {
                useableObject = null;
            }
        }
        else if (otherObj.gameObject.tag == "Shrine")
        {
            UsableObject shrine = otherObj.gameObject.GetComponent<Shrine>();
            if (shrine == null)
            {
                shrine = otherObj.gameObject.GetComponent<EvilShrine>();
            }
            if (shrine == useableObject)
            {
                useableObject = null;
            }
        }
    }


    public Color GetColour()
    {
        return colour;
    }

    public Sprite newPlayerSprite(Color colour)
    {
        colour.r *= 255;
        colour.g *= 255;
        colour.b *= 255;
        if (colour == new Color(255, 0, 0)) { return redSprite; }
        else if (colour == new Color(0, 255, 0)) { return greenSprite; }
        else if (colour == new Color(0, 0, 255)) { return blueSprite; }
        else if (colour == new Color(255, 255, 0)) { return yellowSprite; }
        else { Debug.Log("Unknown colour!"); return defaultSprite; }
    }

    public Orb RemoveOrb()
    {
        Orb toReturn = orb;
        orb = null;
        return toReturn;
    }
    public Orb Orb
    {
        get
        {
            return orb;
        }

        set
        {
            orb = value;
        }
    }

}
