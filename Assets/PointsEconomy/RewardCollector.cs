using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RewardCollector : MonoBehaviour
{
    Rigidbody2D rgb;

    private void Start()
    {
        rgb = this.gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Reward")
        {
            Destroy(collision.gameObject);
            rgb.velocity = Vector3.zero;
            Points.points++;
        }
    }
}
