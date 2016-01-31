using UnityEngine;
using System.Collections;

public class EvilShrine : UsableObject {

    Color colour = new Color();
    PlayerOrb validPlayer;
    PlayerMotion[] players;

    void Start()
    {
        players = FindObjectsOfType<PlayerMotion>();
    }

    public override bool Use(PlayerOrb player)
    {
        if (player == validPlayer)
        {
            EnpowerPlayers();
            RemoveWalls();
            DestroyOrb(player);

            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            return true;
        }
        return false;
    }

    private void EnpowerPlayers()
    {
        Debug.Log("Ian Empower");
        if (!players[0].hasJump)
        {
            Debug.Log("Ian jump");
            foreach (PlayerMotion playerMotion in players)
            {
                Debug.Log("Ian player: " + playerMotion);
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
                colour = new Color();
            }
        }
    }
}
