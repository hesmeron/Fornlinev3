using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaRestricition : MonoBehaviour
{
    [SerializeField]
    float tolerance = 0f;
    Point point;
    Vector2 left, right;
    Rect rect;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 offsetVector = new Vector2(tolerance, tolerance);
        left = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)) - offsetVector;
        right = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10)) + offsetVector;
        rect = new Rect(left, new Vector2(right.x - left.x, right.y - left.y));
        point = this.gameObject.GetComponent<Point>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!rect.Contains(this.transform.position))
        {
            Destroy(point.gameObject);
        }
    }
}
