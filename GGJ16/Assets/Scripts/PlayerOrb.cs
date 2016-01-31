using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerOrb : MonoBehaviour {

    private Color colour;
    public Sprite redSprite;
    public Sprite blueSprite;
    public Sprite greenSprite;
    public Sprite yellowSprite;
    public Sprite orangeSprite;
    public Sprite purpleSprite;
    public Sprite defaultSprite;
    Orb orb;

    private UsableObject useableObject;

    // Use this for initialization
    void Start () {
        colour = new Color();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void Use(GamePadState gamePadState)
    {
        if (gamePadState.Buttons.X == ButtonState.Pressed)
        {
            if (useableObject != null)
            {
                if (useableObject.Use(this))
                {
                    useableObject = null;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Orb")
        {
            useableObject = otherObj.gameObject.GetComponent<Orb>();
            useableObject.Use(this);
            useableObject = null;
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
        if (colour == new Color(1, 0, 0)) { sprite =  redSprite; }
        else if (colour == new Color(0, 1, 0)) { sprite = greenSprite; }
        else if (colour == new Color(0, 0, 1)) { sprite = blueSprite; }
        else if (colour == new Color(1, 1, 0)) { sprite = yellowSprite; }
        else if (colour == new Color(1, .5f, 0)) { sprite =  orangeSprite; }
        else if (colour == new Color(1, 0, 1)) { sprite = purpleSprite; }
        else {
            colour = new Color();
            sprite = defaultSprite;
        }
        GetComponent<SpriteRenderer>().sprite = sprite;
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
            colour = value;
            newPlayerSprite();
        }
    }
}
