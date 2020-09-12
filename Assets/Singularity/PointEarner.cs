using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointEarner : Repulsable
{
    public List<Vector2> directions = new List<Vector2>();
    [SerializeField]
    float treshold = 10f, radious = 2f;
    [SerializeField]
    GameObject rewardPref;
    public override void Repulse(Vector2 force)
    {
        Debug.Log("Earn");
        bool wasFound = false;
        for(int i=0; i< directions.Count; i++)
        {
            
            if(directions[i].normalized == force.normalized)
            {
                directions[i] += force;
                wasFound = true;
                if(directions[i].magnitude > treshold)
                {
                    directions[i] = directions[i].normalized;
                    CreatePointCharge(directions[i]);
                }
                break;
            }
        }

        if (!wasFound)
        {
            directions.Add(force);
        }
    }

    public void CreatePointCharge(Vector2 direction)
    {
        Instantiate(rewardPref);
        rewardPref.transform.position = this.transform.position - (Vector3)direction * radious;
    }
}
