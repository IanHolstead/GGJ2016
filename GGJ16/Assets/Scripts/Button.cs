using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public bool buttonPressed = false;
    public GameObject[] platforms;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Player")
        {
            Debug.Log("Detected");
            foreach (GameObject obj in platforms)
            {
                Debug.Log("Platform 1");
                Controllable platform = obj.GetComponent<MovingPlatform>();
                platform.Activate();
            }
        }
    }

    void OnTriggerExit2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Player")
        {
            foreach (GameObject obj in platforms)
            {
                Controllable platform = obj.GetComponent<MovingPlatform>();
                platform.Deactivate();
            }
        }
    }

}
