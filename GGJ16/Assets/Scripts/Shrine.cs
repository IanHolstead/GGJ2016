using UnityEngine;
using System.Collections;

public class Shrine : UsableObject {

    public Color colour;
    PlayerOrb validPlayer;
	public GameObject votePasser;
	private VotePasser votePasserScript;

	void Start(){
		votePasserScript = votePasser.GetComponent<VotePasser> ();
	}

    public override bool Use(PlayerOrb player)
    {
        if (player == validPlayer)
        {
            RemoveWalls();
            DestroyOrb(player);
			votePasserScript.giveVoteToo(player.playerID);
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

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Player")
        {
            PlayerOrb player = otherObj.GetComponent<PlayerOrb>();
            if (player.Colour == colour)
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
