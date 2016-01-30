using UnityEngine;
using System.Collections;

public class ColourWall : MonoBehaviour {

    public Color colour;

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Player")
        {
            PlayerOrb player = otherObj.gameObject.GetComponent<PlayerOrb>();
            if (player.GetColour() == colour)
            {
                Physics2D.IgnoreCollision(otherObj, this.GetComponent<BoxCollider2D>());
            }
        }
    }

    void OnTriggerExit2D(Collider2D otherObj)
    {
        if (otherObj.gameObject.tag == "Player")
        {
            PlayerOrb player = otherObj.gameObject.GetComponent<PlayerOrb>();
            Physics2D.IgnoreCollision(otherObj, this.GetComponent<BoxCollider2D>(), false);
        }
    }
}
