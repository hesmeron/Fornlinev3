using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingProperties : MonoBehaviour
{
    public static ShootingProperties shootingProperties;
    [SerializeField]
    GameObject pointPref;
    public static GameObject point;
    [SerializeField]
    float releaseTreshold = 1f;
    public static float treshold;

    public void Start()
    {
        if(shootingProperties != null)
        {
            Destroy(this);
            return;
        }
        shootingProperties = this;
        point = pointPref;
        treshold = releaseTreshold;
    }
}
