using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repulsor : MonoBehaviour
{
    Vector2 a, b;
    public Vector2 direction;
    public static float strength = 0.15f;


    public void Initialaize(Point from, Point to)
    {
        Vector2 lineDriection = from.transform.position - to.transform.position;
        direction = Vector2.Perpendicular(lineDriection).normalized;
        a = from.transform.position;
        b = to.transform.position;
        RepulsionSystem.repulsors.Add(this);
    }
    public void Update()
    {
        foreach(var repulsable in RepulsionSystem.repulsables)
        {
            if(repulsable != null && IsAffected(repulsable.transform.position))
                ApplyForce(direction, repulsable);
        }
    }

    public void ApplyForce(Vector2 direction, Repulsable repulsable)
    {
        repulsable.Repulse(direction * strength * SideModifier(repulsable.transform.position) / CalculaterDistance(repulsable.transform.position));
    }

    float CalculaterDistance(Vector2 c)
    {
        float up = ((c.x - a.x) * (-b.y + a.y)) + ((c.y - a.y) * (b.x - a.x));
        float down = Mathf.Pow((-b.y + a.y), 2) + Mathf.Pow((b.x - a.x), 2);
        return Mathf.Clamp(Mathf.Abs(up) / Mathf.Sqrt(down), 0.1f, 100f);
    }

    public bool IsAffected(Vector2 c)
    {
        Vector2 delta = (a - b);
        delta.Normalize();
        delta *= 0.1f;
        Vector2 da = (a - delta), db = (b + delta);
        Vector2 range = (direction * 50);
        Vector2 d =  da + range;
        Vector2 e = db + range;
        Vector2 f = da - range;
        Vector2 g = db - range;

        Vector2[] polygon = { d, f, g, e };
        return Poly.ContainsPoint(polygon, c);
    }

    float SideModifier(Vector2 c)
    {
        return -Mathf.Sign(((c.x - a.x) * (-b.y + a.y)) + ((c.y - a.y) * (b.x - a.x)));
    }

    public void OnDestroy()
    {
        RepulsionSystem.repulsors.Remove(this);
    }
}
