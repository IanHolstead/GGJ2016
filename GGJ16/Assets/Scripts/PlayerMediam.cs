﻿using UnityEngine;
using System.Collections;

public class PlayerMediam : MonoBehaviour
{
	public GameObject[] players;
	private float x = 0f, y = 0f, maxX = 0f, minX = 0f, maxY = 0f, minY = 0f;
	private Camera childCamera;
	private int following = -1;

	// Use this for initialization
	void Start ()
	{
		childCamera = gameObject.transform.GetChild (0).GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (following < 0) {
			x = players [0].transform.position.x;
			y = players [0].transform.position.y;
			maxX = players [0].transform.position.x;
			minX = players [0].transform.position.x;
			maxY = players [0].transform.position.y;
			minY = players [0].transform.position.y;
			for (int i = 1; i < players.Length; i++) {
				maxX = maxX < players [i].transform.position.x ? players [i].transform.position.x : maxX;
				minX = minX > players [i].transform.position.x ? players [i].transform.position.x : minX;
				maxY = maxY < players [i].transform.position.y ? players [i].transform.position.y : maxY;
				minY = minY > players [i].transform.position.y ? players [i].transform.position.y : minY;
				x += players [i].transform.position.x;
				y += players [i].transform.position.y;
			}
			x /= players.Length;
			y /= players.Length;
		} else {
			x = players [following].transform.position.x;
			y = players [following].transform.position.y;
			maxX = players [following].transform.position.x;
			minX = players [following].transform.position.x;
			maxY = players [following].transform.position.y;
			minY = players [following].transform.position.y;
		}
		gameObject.transform.position = new Vector2 (x, y);
		float X = (maxX - minX);
		float Y = (maxY - minY);
		if (X - 1 > Y) {
			childCamera.orthographicSize = (X + 2) / 2f;
		} else {
			childCamera.orthographicSize = (Y + 2) / 1.7f;
		}
	}

	public void FollowPlayer(int player){
		following = player;
	}

	public void UnFollowPlayer(){
		following = -1;
	}
}
