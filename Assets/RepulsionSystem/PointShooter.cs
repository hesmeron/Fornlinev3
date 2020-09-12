using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointShooter : Repulsable
{
    [SerializeField]
    Vector2 currentCharge;
    public override void Repulse(Vector2 force)
    {
        currentCharge += force;

        if(currentCharge.magnitude > ShootingProperties.treshold && Points.BuyForPoint())
        {
            currentCharge = Vector2.zero;
            CreateProjectile();
        }
    }

    void CreateProjectile()
    {
        GameObject newProjectile = Instantiate(ShootingProperties.point);
        newProjectile.transform.position = this.transform.position;
        newProjectile.AddComponent<RepulsedBody>();
        newProjectile.gameObject.tag = "Projectile";
    }
}
