using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    float timeForWave = 3f, timePassed = 0f, currentTime = 0, deltaX = 2, deltaY=2, minConnectionDistance = 1, maxConnectionDistance = 2.5f;
    Vector2 left, right, constrains;
    [SerializeField]
    float offset = 10f;
    [SerializeField]
    float speed = 0.2f, spawnMultiplier = 0.2f, rangeMiltiplier = 3f;
    [SerializeField]
    GameObject projectilePref, linePref;
    List<Point> createdPoints = new List<Point>();
    // Start is called before the first frame update
    void Start()
    {
        Vector2 offsetVector = new Vector2(offset, offset);
        left = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)) - offsetVector;
        right = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10)) + offsetVector;
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        currentTime += Time.deltaTime;
        if(currentTime >= timeForWave)
        {
            currentTime = 0;
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        createdPoints.Clear();
        int quantity = 1 + (int)(spawnMultiplier * Mathf.Log(timePassed, 2));
        Debug.Log("Spawn Wave " + quantity);
        for (int i = 0; i < quantity; i++)
        {
            SpawnProjectile();
        }
        Debug.Log("Projectiles in wave " + createdPoints.Count + " " + quantity);
        if(quantity >=2)
        {
            CreateConections();
        }        
        foreach(var c in createdPoints)
        {
            SetDirection(c.gameObject);
        }
    }

    void SpawnProjectile()
    {
        Debug.Log("Spawn Projectile");
        GameObject projectile = Instantiate(projectilePref);
        projectile.transform.position = GetObjectPosition();
        createdPoints.Add(projectile.GetComponent<Point>());
    }

    void CreateConections()
    {
        int connections = Random.Range(createdPoints.Count / 4, createdPoints.Count / 2) -1;
        Debug.Log("Try create connection " + connections +  " " + createdPoints.Count);
        for (int i = 0; i < connections; i++)
        {
            List<Point> connectablePoints = createdPoints.GetRange(0, createdPoints.Count);
            Point a = connectablePoints[Random.Range(0, connectablePoints.Count)];
            connectablePoints.Remove(a);
            Point b = connectablePoints[Random.Range(0, connectablePoints.Count-1)];
            StandardizeDistance(a, b);
            CreateConnection(a, b);
            CheckForLimit(a);
            CheckForLimit(b);
        }
    }

    void SetDirection(GameObject projectile)
    {
        Vector2 heading = (Singularity.position - (Vector2)projectile.transform.position);
        projectile.GetComponent<Rigidbody2D>().AddForce(heading * speed * Mathf.Log10(timePassed));
    }
    void StandardizeDistance(Point a, Point b)
    {
        Vector3 direction = a.transform.position - b.transform.position;

        if (direction.magnitude < maxConnectionDistance && direction.magnitude > minConnectionDistance)
        {
            return;
        }
        else
        {
            
            List<Point> pointsToMove = new List<Point>();
            List<Point> movedPoints = new List<Point>();
            
            int choice = -1;
            if (direction.x > direction.y)
            {
                choice = 0;
            }
            else
            {
                choice = 1;
            } 
            Vector3 translation = GetTranslation(a,b,direction);
            pointsToMove.Add(b);
            while (pointsToMove.Count > 0)
            {
                MovePoint(ref pointsToMove, ref movedPoints, translation);
            }
        }
    }

    void MovePoint(ref List<Point> pointsToMove, ref List<Point> movedPoints, Vector3 translation)
    {
        Point currentPoint = pointsToMove[0];
        if (!movedPoints.Contains(currentPoint))
        {
            currentPoint.transform.position += translation;
            pointsToMove.Remove(currentPoint);
            pointsToMove.AddRange(currentPoint.neighbours);
            movedPoints.Add(currentPoint);
        }
        else {
            pointsToMove.Remove(currentPoint);
        }
    }

    Vector3 GetTranslation(Point a, Point b, Vector3 direction)
    {
        Vector3 translation = direction.normalized * Random.Range(minConnectionDistance, maxConnectionDistance);
        translation = new Vector3(Mathf.Clamp(translation.x, minConnectionDistance, maxConnectionDistance),
                                    Mathf.Clamp(translation.y, minConnectionDistance, maxConnectionDistance),
                                    translation.z);
        return a.transform.position + translation - b.transform.position;
    }

    void CreateConnection(Point a, Point b)
    {
        Debug.Log("Connection created");
        Line line = Drawer.drawer.CreateConnection(a, b);
        line.color = Color.red;
        line.gameObject.layer = 10;
        line.name = "EnemyLine";
        LineUpdater updater = line.gameObject.AddComponent<LineUpdater>();
        updater.from = a.transform;
        updater.to = b.transform;
        b.GetComponent<Rigidbody2D>().velocity = a.GetComponent<Rigidbody2D>().velocity;
    }

    void CheckForLimit(Point target)
    {
        int maxConnections = (int)Mathf.Log10(timePassed);
        if(target.neighbours.Count > maxConnections)
        {
            createdPoints.Remove(target);
        }
    }
    Vector3 GetObjectPosition()
    {
        float x = Random.Range(-deltaX, deltaX);
        float y = Random.Range(-deltaY, deltaY);
        int choice = Random.Range(0, 2);
        Vector3 position = new Vector3(x, y, 0);
        GetXY(ref position, choice);
        return position;
    }

    Vector2 GetConstrains(int choice)
    {
        float spawnRange = Mathf.Clamp((Mathf.Log10(timePassed) * rangeMiltiplier)/100, 0,  1);
        float delta = (0.5f -spawnRange) * (right[choice] - left[choice]);
        Debug.Log("Set constraints "  + new Vector2(left[choice], right[choice]) + " " + delta + " "  + spawnRange);
        return new Vector2(left[choice] + delta, right[choice] - delta);
    }
    void GetXY(ref Vector3 position, int choice)
    {
        constrains = GetConstrains(choice);
        position[choice] = Random.Range(constrains.x, constrains.y);
        int antichoice = 1 - choice;
        if (position[antichoice] >= 0)
        {
            position[antichoice] += right[antichoice];
        }
        if (position[antichoice] < 0)
        {
            position[antichoice] = left[antichoice] - position[antichoice];
        }
    }
}
