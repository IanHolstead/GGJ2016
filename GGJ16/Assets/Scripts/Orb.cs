﻿using UnityEngine;
using System.Collections.Generic;

public class Orb : UsableObject {
    public Color colour;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

	}

    public override void Use(PlayerOrb player)
    {
        Debug.Log("Orb used by: " + player);
        if (player.Orb == null)
        {
            player.Colour = colour;
            player.Orb = this;

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            //SwapOrb()
        }
    }

    //void OnTriggerEnter2D(Collider2D otherObj)
    //{
    //    Debug.Log("Orb Collision!");
    //    if(otherObj.gameObject.tag == "Player")
    //    {
    //        otherObj.gameObject.GetComponent<SpriteRenderer>().sprite = otherObj.gameObject.GetComponent<PlayerOrb>().newPlayerSprite(this.gameObject.GetComponent<SpriteRenderer>().color);
    //    }
    //}
}
