using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningObstacles : MonoBehaviour
{

    public int squaresAmount = 30;
    public int trianglesAmount = 80;
    public GameObject squaresPrefab;
    public GameObject trianglesPrefab;
    public float activeRate = 3f;
    public Vector2 activeMin;
    public Vector2 activeMax;
    private List<GameObject> inactiveSquares;
    private List<GameObject> inactiveTriangles;
    private Vector2 inactiveObstaclesSpawnLocation = new Vector2(40, 0);
    private float timeSinceLastActivated;

    // Use this for initialization
    void Start()
    {
        inactiveSquares = new List<GameObject>();
        inactiveTriangles = new List<GameObject>();
        for (int i = 0; i < squaresAmount; i++)
        {
            inactiveSquares.Add(Instantiate(squaresPrefab, inactiveObstaclesSpawnLocation, Quaternion.identity));
        }
        for (int i = 0; i < trianglesAmount; i++)
        {
            inactiveTriangles.Add(Instantiate(trianglesPrefab, inactiveObstaclesSpawnLocation, Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastActivated += Time.deltaTime;
        if (!GameControl.instance.gameOver && timeSinceLastActivated >= activeRate)
        {
            timeSinceLastActivated = 0;
            if (Random.Range(0, 2) == 1)
            {
                if (inactiveSquares.Count > 0)
                {
                    inactiveSquares[0].transform.SetPositionAndRotation(new Vector3(Random.Range(activeMin[0], activeMax[0]), Random.Range(activeMin[1], activeMax[1]), 0), transform.rotation);
                    inactiveSquares[0].GetComponent<Obstacles>().Randomize();
                    inactiveSquares.RemoveAt(0);
                }
            }
            else
            {
                if(inactiveTriangles.Count>0)
                {
                    inactiveTriangles[0].transform.SetPositionAndRotation(new Vector3(Random.Range(activeMin[0], activeMax[0]), Random.Range(activeMin[1], activeMax[1]), 0), transform.rotation);
                    inactiveTriangles[0].GetComponent<Obstacles>().Randomize();
                    inactiveTriangles.RemoveAt(0);
                }
            }
        }
    }

    public void AddToList(GameObject obstacle)
    {
        if(obstacle.GetComponent<Obstacles>().type==Obstacles.Types.Triangle)
        {
            inactiveTriangles.Add(obstacle);
        }
        else
        {
            inactiveSquares.Add(obstacle);
        }
    }
}
