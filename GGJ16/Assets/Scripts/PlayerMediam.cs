using UnityEngine;
using System.Collections;

public class PlayerMediam : MonoBehaviour
{
	public GameObject[] players;
	private float x = 0f, y = 0f, maxX = 0f, minX = 0f, maxY = 0f, minY = 0f;
	private Camera childCamera;

	// Use this for initialization
	void Start ()
	{
		childCamera = gameObject.transform.GetChild (0).GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		x = 0f;
		y = 0f;
		maxX = players [0].transform.position.x;
		minX = players [0].transform.position.x;
		maxY = players [0].transform.position.y;
		minY = players [0].transform.position.y;
		x += players [0].transform.position.x;
		y += players [0].transform.position.y;
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
		gameObject.transform.position = new Vector2 (x, y);
		float X = (maxX - minX);
		float Y = (maxY - minY);
		if (X - 1 > Y) {
			childCamera.orthographicSize = (X + 2) / 2f;
		} else {
			childCamera.orthographicSize = (Y + 2) / 1.7f;
		}
	}
}
