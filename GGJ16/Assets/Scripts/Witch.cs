using UnityEngine;
using System.Collections;

public class Witch : MonoBehaviour {

    PlayerMotion[] players;
    public int cursedPlayer = -1;

	// Use this for initialization
	void Start () {
        players = FindObjectsOfType<PlayerMotion>();
        //Debug.Assert(players.Length == 4);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CursePlayer(int overridePlayerIndex = -1)
    {
        int index = Random.Range(0, 3);
        if (overridePlayerIndex != -1)
        {
            foreach (PlayerMotion player in players)
            {
                if (player.playerID == overridePlayerIndex)
                {
                    player.gameObject.AddComponent<EvilPowers>();
                }
            }
        }
        else
        {
            players[index].gameObject.AddComponent<EvilPowers>();
        }

    }

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Player")
        {
            CursePlayer(cursedPlayer);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
