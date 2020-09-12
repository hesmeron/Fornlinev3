using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Repulsable : MonoBehaviour
{ 
    public abstract void Repulse(Vector2 force);


    public void OnDestroy()
    {
        RepulsionSystem.repulsables.Remove(this);
    }

    public void Start()
    {
        RepulsionSystem.repulsables.Add(this);
        foreach (var r in RepulsionSystem.repulsors)
        {
            if (r == null)
            {
                Debug.LogError("Non assigned repulsor");
                continue;
            }
        }
    }
}
