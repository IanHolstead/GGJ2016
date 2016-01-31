using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

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
            Debug.Log("Checkpoint Collision!");
            PlayerMotion[] players = FindObjectsOfType<PlayerMotion>();
            for(int i = 0; i < players.Length; i++)
            {
                players[i].respawn = new Vector3(0, 14, 0);
            }
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
