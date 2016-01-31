using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public bool buttonPressed = false;
    public GameObject[] platforms;

    public Sprite normalButtonSprite;
    public Sprite activatedButtonSprite;

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = normalButtonSprite;
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Player")
        {
            GetComponent<SpriteRenderer>().sprite = activatedButtonSprite;
            foreach (GameObject obj in platforms)
            {
                Controllable platform = obj.GetComponent<MovingPlatform>();
                platform.Activate();
            }
        }
    }

    void OnTriggerExit2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Player")
        {
            GetComponent<SpriteRenderer>().sprite = normalButtonSprite;
            foreach (GameObject obj in platforms)
            {
                Controllable platform = obj.GetComponent<MovingPlatform>();
                platform.Deactivate();
            }
        }
    }

}
