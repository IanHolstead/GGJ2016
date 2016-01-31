using UnityEngine;
using System.Collections.Generic;

public class Shrine : UsableObject {

    public Color colour;
    List<PlayerOrb> validPlayers;

	void Start(){
        validPlayers = new List<PlayerOrb>();
	}

    public override bool Use(PlayerOrb player)
    {
        if (validPlayers.Contains(player))
        {
            RemoveWalls();
            DestroyOrb(player);
            validPlayers.Clear();
            givePlayerVote(player.GetComponent<PlayerMotion>());
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            return true;
        }
        return false;
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

    void givePlayerVote(PlayerMotion player)
    {
        //player.
    }

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Player")
        {
            PlayerOrb player = otherObj.GetComponent<PlayerOrb>();
            if (player.Colour == colour)
            {
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
            }
        }
    }
}
