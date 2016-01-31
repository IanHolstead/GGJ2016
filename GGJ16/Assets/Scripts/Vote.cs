using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Vote : MonoBehaviour
{
	// List of lists for tracking the vote objects
	private List<List<Transform>> playerVotes = new List<List<Transform>>();
	private List<Transform> p1Votes = new List<Transform>();
	private List<Transform> p2Votes = new List<Transform>();
	private List<Transform> p3Votes = new List<Transform>();
	private List<Transform> p4Votes = new List<Transform>();
	private int[] playerBalletsPartial = new int[4] { -1, -1, -1, -1 };
	public Text timerText;
	public GameObject storm;
	private Shocking stormShock;
	private PlayerMediam cameraFollowScript;
	private SpriteRenderer Center;

	public float timer = 10f;
	bool displaying = true;
	bool voting = false;

	// Use this for initialization
	void Start ()
	{
		//Debug.Log ("Start!");
		playerVotes.Add (p1Votes);
		playerVotes.Add (p2Votes);
		playerVotes.Add (p3Votes);
		playerVotes.Add (p4Votes);
		timerText.text = timer.ToString();

		for (int i = 0; i < 4; ++i) {
			for (int ii = 0; ii < 4; ++ii) {
				playerVotes[i].Add(gameObject.transform.FindChild("P"+ (i+1) + "Vote" + (ii+1)));
				playerVotes[i][ii].GetComponent<SpriteRenderer> ().enabled = false;
			}
		}
		cameraFollowScript = GetComponentInParent<PlayerMediam> ();
		stormShock = storm.GetComponent<Shocking> ();
		Center = GetComponent<SpriteRenderer> ();
		Center.enabled = voting;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (voting) {
			Center.enabled = true;
			// Collect the vote of each player
			for (int i = 1; i < 5; i++) {
				if (Input.GetAxis ("P" + i + "DPadVert") < 0) {
					playerBalletsPartial [i - 1] = 0;
				} else if (Input.GetAxis ("P" + i + "DPadHoriz") > 0) {
					playerBalletsPartial [i - 1] = 1;
				} else if (Input.GetAxis ("P" + i + "DPadVert") > 0) {
					playerBalletsPartial [i - 1] = 2;
				} else if (Input.GetAxis ("P" + i + "DPadHoriz") < 0) {
					playerBalletsPartial [i - 1] = 3;
				}
			}

			int[] playerBallets = new int[4] { 0, 0, 0, 0 };
			for (int i = 0; i < 4; i++) {
				if (playerBalletsPartial [i] == -1) {
					continue;
				}
				playerBallets [playerBalletsPartial [i]]++;
			}

			// Render the appropriate vote boxes
			for (int i = 0; i < 4; i++) {
				for (int ii = 0; ii < 4; ii++) {
					playerVotes [i] [ii].GetComponent<SpriteRenderer> ().enabled = ((playerBallets [i] > ii) && displaying);
				}
			}
			if (displaying == false) {
				return;
			}
			timer -= Time.deltaTime;
			timerText.text = timer.ToString ();
			if (timer < 0) {
				voting = false;
				displaying = false;
				// Close the vote and call lightning
				this.GetComponent<Renderer> ().enabled = false;
				timerText.text = "";

				// Figure out the character with the most votes
				// Iterate through to find the most votes
				int[] votesDist = new int[4] { 0, 0, 0, 0 };
				for (int i = 0; i < 4; ++i) {
					votesDist [playerBallets [i]]++;
				}
				// Iterate backwards through the vote distribution to
				// find the player(s) with the most votes
				for (int i = 3; i >= 0; --i) {
					if (votesDist [i] == 1) {
						// Iterate through to find the player with 
						// those votes and zap them
						for (int ii = 0; ii < 4; ++ii) {
							if (playerBallets [ii] == i) {
								//Debug.Log ("Zapping Player" + (ii + 1));
								stormShock.AttackTarget (ii);
								cameraFollowScript.FollowPlayer (ii);
								return;
							}
						}
					} else if (votesDist [i] >= 2 || i == 0) {
						// If there is to "winners" or we've got to the 
						//end of the array
						//Debug.Log ("Zapping Everyone");
						stormShock.AttackAll ();
						return;
					}
				}
			}
		} else {
			for (int i = 0; i < 4; i++) {
				for (int ii = 0; ii < 4; ii++) {
					playerVotes [i] [ii].GetComponent<SpriteRenderer> ().enabled = false;
				}
			}
			resetVote ();
			timerText.text = "";
		}
	}

	void resetVote(){
		for (int x = 0; x < 4; x++) {
			playerBalletsPartial [x] = -1;
		}
	}

	public void voteNow(){
		voting = true;
	}
}

