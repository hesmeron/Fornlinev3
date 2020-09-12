using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RepulsedBody : Repulsable
{
    Rigidbody2D rgb;
    [SerializeField]
    float charge = 1f;

    public override void Repulse(Vector2 force)
    {
        if(rgb != null)
        {
            rgb.AddForce(force * charge);
        }
    }

    // Start is called before the first frame update
    public void Awake()
    {
        rgb = this.gameObject.GetComponent<Rigidbody2D>();
    }
}
