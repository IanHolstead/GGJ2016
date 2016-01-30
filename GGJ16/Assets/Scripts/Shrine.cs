﻿using UnityEngine;
using System.Collections;

public class Shrine : UsableObject {

    public Color colour;
    PlayerOrb validPlayer;

    public override void Use(PlayerOrb player)
    {
        if (player == validPlayer)
        {
            RemoveWalls();
            DestroyOrb(player);

            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void RemoveWalls()
    {
        ColourWall[] walls = FindObjectsOfType<ColourWall>();
        foreach (ColourWall wall in walls)
        {
            wall.DisableWallByColour(colour);
        }
    }

    private void DestroyOrb(PlayerOrb player)
    {
        player.colour = new Color();
        Orb orb = player.RemoveOrb();
        Destroy(orb);
    }

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Player")
        {
            PlayerOrb player = otherObj.GetComponent<PlayerOrb>();
            if (player.GetColour() == colour)
            {
                validPlayer = player;
            }
        }
    }

    void OnTriggerExit2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Player")
        {
            PlayerOrb player = otherObj.GetComponent<PlayerOrb>();
            if (player == validPlayer)
            {
                validPlayer = null;
            }
        }
    }
}
