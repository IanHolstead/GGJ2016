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

    public Orb RemoveOrb()
    {
        Orb toReturn = orb;
        orb = null;
        return toReturn;
    }
}
