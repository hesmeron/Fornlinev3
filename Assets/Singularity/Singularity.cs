using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LineRenderer))]
[ExecuteAlways]
public class Singularity : MonoBehaviour
{
    public int segments;
    public float xradius;
    public float yradius;
    public int currentHealth = 12;
    public int maxHealth = 12;
    public static Vector2 position;
    LineRenderer line;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        position = this.transform.position;
        Points.points = 4;
        line.useWorldSpace = false;
    }


    void Update()
    {
        line.positionCount = segments + 1;
        float x;
        float y;
        float z = 0f;
        float angleDelta = (360f / segments) * (currentHealth / (float) maxHealth);
        float angle = angleDelta;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += angleDelta;
        }

        if (Input.GetKeyDown(KeyCode.R)){
            Lose();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Point point = collision.gameObject.GetComponent<Point>();
        Line line = collision.gameObject.GetComponent<Line>();
        LineCollider col = collision.gameObject.GetComponent<LineCollider>();

        currentHealth--;
        if (currentHealth == 1)
        {
            StartCoroutine("Lose");
        }
        
        if(point != null)
        {
            Destroy(point.gameObject);
        }
        else if(line != null && col != null)
        {
            Destroy(line.gameObject);
        }
    }
    IEnumerator Lose()
    {
        Points.points = 4;
        Time.timeScale = 0.2f;
        yield return  new WaitForSeconds(1f);
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }
}

