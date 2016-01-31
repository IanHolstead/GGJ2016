using UnityEngine;
using System.Collections.Generic;

public class Orb : UsableObject {
    public Color colour;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

	}

    public override bool Use(PlayerOrb player)
    {
        if (player.Orb == null)
        {
            player.Colour = colour;
            player.Orb = this;

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            return true;
        }
        else
        {
            //SwapOrb()
        }
        return false;
    }
}
