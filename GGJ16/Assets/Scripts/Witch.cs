using UnityEngine;
using System.Collections;

public class Witch : MonoBehaviour {

    PlayerOrb[] players;
    public int cursedPlayer = -1;

	// Use this for initialization
	void Start () {
        players = FindObjectsOfType<PlayerOrb>();
        //Debug.Assert(players.Length == 4);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CursePlayer(int overridePlayerIndex = -1)
    {
        int index = overridePlayerIndex != -1 ? overridePlayerIndex : Random.Range(0, 3);
        players[index].gameObject.AddComponent<EvilPowers>();
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
