using UnityEngine;
using System.Collections;

public class EvilShrine : MonoBehaviour {

    Color colour = new Color();
    PlayerOrb validPlayer;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual bool DepositeOrb(PlayerOrb player)
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
        PlayerMotion[] players = FindObjectsOfType<PlayerMotion>();

        if (!players[0].hasDoubleJump)
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
        player.colour = new Color();
        Orb orb = player.RemoveOrb();
        Destroy(orb);
    }

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Player")
        {
            PlayerOrb player = otherObj.GetComponent<PlayerOrb>();
            if (player.GetColour() != colour)
            {
                colour = player.GetColour();
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
