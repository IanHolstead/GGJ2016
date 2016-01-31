using UnityEngine;
using System.Collections.Generic;

public class EvilPowers : MonoBehaviour {

    public float trapCooldown = 10;
    public float freezeCooldown = 20;

    float timeSinceTraps = 0;
    float timeSinceFreeze = 0;

    List<Trap> traps;
    List<Rigidbody2D> players;
    

	// Use this for initialization
	void Start () {
        //GET READY TO RUUUUUUUUMMMMMMMMBBBBBBBBLLLLLLLLLLEEEEEEE
        traps = new List<Trap>();
        //traps = FindObjectsOfType<Trap>();
        //find all traps
	}
	
	// Update is called once per frame
	void Update () {
        timeSinceFreeze += Time.deltaTime;
        timeSinceTraps += Time.deltaTime;
        
        //if button is pushed


	}

    void UseTraps()
    {
        foreach (Trap trap in traps)
        {
            trap.Activate();
        }
    }

    void Freeze()
    {
        foreach (Rigidbody2D player in players)
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }
    }
}
