using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCollider : MonoBehaviour
{
    public EdgeCollider2D edge;
    public Line line;
    // Start is called before the first frame update
    void Start()
    {
        edge = this.gameObject.AddComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2[] points = { line.from - (Vector2)this.transform.position,  line.to - (Vector2)this.transform.position };
        edge.points = points;
    }
}
