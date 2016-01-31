using UnityEngine;
using System.Collections;

public abstract class Controllable : MonoBehaviour {
    public abstract void Activate();
    public abstract void Deactivate();
}
