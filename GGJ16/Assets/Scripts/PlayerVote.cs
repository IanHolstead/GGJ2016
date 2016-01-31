using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerVote : MonoBehaviour {
    public int votesLeft = 0;
    
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CallVote(GamePadState state)
    {
        if (votesLeft > 0)
        {
            if (state.Buttons.B == ButtonState.Pressed)
            {
                FindObjectOfType<Vote>().voteNow();
                votesLeft--;
            }
        }
    }

    public void AddVote()
    {
        votesLeft++;
    }
}
