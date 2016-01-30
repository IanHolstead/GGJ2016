using UnityEngine;
using System.Collections;

public class Shrine : MonoBehaviour {

    public Color colour;
    PlayerOrb validPlayer;
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual bool DepositeOrb(PlayerOrb player)
    {
        if (player == validPlayer)
        {
            RemoveWalls();
            DestroyOrb(player);

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
