using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Line))]
public class LineUpdater : MonoBehaviour
{
    public Transform from, to;
    Line line;
    // Start is called before the first frame update
    void Start()
    {
        line = this.gameObject.GetComponent<Line>();
    }

    // Update is called once per frame
    void Update()
    {
        line.SetNewPosition(from.position, to.position);
    }
}
