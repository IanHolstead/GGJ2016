﻿using UnityEngine;
using System.Collections;

public class PlayerOrb : MonoBehaviour {

    private Color colour;
    public Sprite redSprite;
    public Sprite blueSprite;
    public Sprite greenSprite;
    public Sprite yellowSprite;
    public Sprite defaultSprite;
    Orb orb;

    private UsableObject useableObject;

    // Use this for initialization
    void Start () {
        colour = new Color();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (useableObject != null)
            {
                Debug.Log("Use Pressed on:" + useableObject);
                if(useableObject.Use(this))
                {
                    useableObject = null;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Orb Dropped");
            Colour = new Color();
            orb = null;
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

    void newPlayerSprite()
    {
        colour.a = 1;
        Sprite sprite;
        Debug.Log("Colour: " + colour);
        Debug.Log("New colour: " + new Color(0, 0, 1));
        if (colour == new Color(1, 0, 0)) { sprite =  redSprite; }
        else if (colour == new Color(0, 1, 0)) { sprite = greenSprite; }
        else if (colour == new Color(0, 0, 1)) { sprite = blueSprite; }
        else if (colour == new Color(1, 1, 0)) { sprite = yellowSprite; }
        else {
            //Debug.Log("Unknown colour!");
            colour = new Color();
            sprite = defaultSprite;
        }
        Debug.Log("setting sprite to: " + sprite);
        GetComponent<SpriteRenderer>().sprite = sprite;
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

    public void Die()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        if (orb != null)
        {
            orb.GetComponent<SpriteRenderer>().enabled = true;
            orb.GetComponent<BoxCollider2D>().enabled = true;
            RemoveOrb();
        }
    }

    public Orb RemoveOrb()
    {
        Colour = new Color();
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

    public Color Colour
    {
        get
        {
            return colour;
        }

        set
        {
            Debug.Log("Set colour to: " + value);
            colour = value;
            newPlayerSprite();
        }
    }
}
