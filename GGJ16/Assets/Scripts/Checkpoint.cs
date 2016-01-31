using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    public Vector3 respawnOffset = new Vector3(0, 5, 0);

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        if(otherObj.gameObject.tag == "Player")
        {
            Vector3 respawnLocation = transform.position + respawnOffset;
            PlayerMotion[] players = FindObjectsOfType<PlayerMotion>();
            for(int i = 0; i < players.Length; i++)
            {
                players[i].SetRespawnLocation(respawnLocation);
            }
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
