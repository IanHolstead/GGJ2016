using UnityEngine;
using System.Collections.Generic;

public class EvilShrine : UsableObject {

    Color colour = new Color();
    List<PlayerOrb> validPlayers;
    PlayerMotion[] players;

    void Start()
    {
        validPlayers = new List<PlayerOrb>();
        players = FindObjectsOfType<PlayerMotion>();
    }

    public override bool Use(PlayerOrb player)
    {
        if (validPlayers.Contains(player))
        {
            EnpowerPlayers();
            RemoveWalls();
            DestroyOrb(player);

            validPlayers.Clear();

            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            return true;
        }
        return false;
    }

    private void EnpowerPlayers()
    {
        if (!players[0].hasJump)
        {
            foreach (PlayerMotion playerMotion in players)
            {
                playerMotion.hasJump = true;
            }
        }
        else if (!players[0].hasDoubleJump)
        {
            foreach (PlayerMotion playerMotion in players)
            {
                playerMotion.hasDoubleJump = true;
            }
        }
        else if (!players[0].hasFastMovement)
        {
            foreach (PlayerMotion playerMotion in players)
            {
                playerMotion.hasFastMovement = true;
            }
        }
        else
        {
            //win condition
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
        Orb orb = player.RemoveOrb();
        Destroy(orb);
    }

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Player")
        {
            PlayerOrb player = otherObj.GetComponent<PlayerOrb>();
            if (player.Colour != colour)
            {
                colour = player.Colour;
                validPlayers.Add(player);
            }
        }
    }

    void OnTriggerExit2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Player")
        {
            PlayerOrb player = otherObj.GetComponent<PlayerOrb>();
            if (validPlayers.Contains(player))
            {
                validPlayers.Remove(player);
                colour = new Color();
            }
        }
    }
}
