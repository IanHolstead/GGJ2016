using UnityEngine;
using System.Collections.Generic;
using XInputDotNetPure;

public class EvilPowers : MonoBehaviour {

    public float trapCooldown = 5;
    public float freezeCooldown = 2;
    public float rumbleLength = 1;

    float timeSinceTraps = 0;
    float timeSinceFreeze = 0;
    float age = 0;
    PlayerIndex playerIndex;

    static PlayerIndex[] playerIndices = new PlayerIndex[] { PlayerIndex.One, PlayerIndex.Two, PlayerIndex.Three, PlayerIndex.Four };

    Trap[] traps;
    List<Rigidbody2D> players;

    GamePadState state;

    // Use this for initialization
    void Start () {
        players = new List<Rigidbody2D>();

        PlayerOrb[] playerOrbs = FindObjectsOfType<PlayerOrb>();
        foreach (PlayerOrb playerOrb in playerOrbs)
        {
            players.Add(playerOrb.GetComponent<Rigidbody2D>());
        }
        
        playerIndex = playerIndices[GetComponent<PlayerMotion>().playerID];

        GamePad.SetVibration(playerIndex, 0, .5f);//GET READY TO RUUUUUUUUMMMMMMMMBBBBBBBBLLLLLLLLLLEEEEEEE
        //traps = new List<Trap>();
        traps = FindObjectsOfType<Trap>();
        //find all traps
	}
	
	// Update is called once per frame
	void Update () {
        state = GamePad.GetState(playerIndex);
        age += Time.deltaTime;
        if (age > rumbleLength)
        {
            GamePad.SetVibration(playerIndex, 0, 0);
        }

        timeSinceFreeze += Time.deltaTime;
        timeSinceTraps += Time.deltaTime;
        if (timeSinceFreeze > freezeCooldown && state.Triggers.Left > .85f)
        {
            Freeze();
        }
        if (timeSinceTraps > trapCooldown && state.Triggers.Right > .85f)
        {
            UseTraps();
        }
    }

    void UseTraps()
    {
        foreach (Trap trap in traps)
        {
            trap.Activate();
            timeSinceTraps = 0;
        }
    }

    void Freeze()
    {
        foreach (Rigidbody2D player in players)
        {
            player.velocity = new Vector2(0, player.velocity.y);
            timeSinceFreeze = 0;
        }
    }
}
