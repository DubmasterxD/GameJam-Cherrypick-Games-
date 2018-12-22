using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicePool : MonoBehaviour
{
    public GameObject policePrefab;
    public List<Transform> spawnPoints;
    public float spawnTime = 10;
    public int walkingPoliceCount = 15;
    private GameObject[] walkingPolice;
    public GameObject magazinePrefab;
    public int magazinesCount = 100;
    private GameObject[] magazines;
    private int currMagazine = 0;
    private int currPoliceman = 0;

    // Use this for initialization
    void Start()
    {
        SpawnStandingPolice();
        InvokeRepeating("SpawnMovingPolice", 0, spawnTime);
    }

    void SpawnMovingPolice()
    {
        int activeCount = 0;
        while (walkingPolice[currPoliceman].GetComponent<Policeman>().isActive && activeCount < walkingPoliceCount)
        {
            activeCount++;
            currPoliceman++;
            if (currPoliceman == walkingPoliceCount)
            {
                currPoliceman = 0;
            }
        }
        if (activeCount != walkingPoliceCount)
        {
            walkingPolice[currPoliceman].GetComponent<Policeman>().isActive = true;
            currPoliceman++;
            if (currPoliceman == walkingPoliceCount)
            {
                currPoliceman = 0;
            }
        }
    }

    void SpawnStandingPolice()
    {
        for (int i = 1; i < spawnPoints.Count; i++)
        {
            Instantiate(policePrefab, spawnPoints[i].position, spawnPoints[i].rotation);
        }
        walkingPolice = new GameObject[walkingPoliceCount];
        magazines = new GameObject[magazinesCount];
        for (int i = 0; i < walkingPoliceCount; i++)
        {
            walkingPolice[i] = Instantiate(policePrefab, spawnPoints[0].position, spawnPoints[0].rotation);
            walkingPolice[i].GetComponent<Policeman>().ActivateMovable();
        }
        for (int i = 0; i < magazinesCount; i++)
        {
            magazines[i] = Instantiate(magazinePrefab, spawnPoints[0].position, spawnPoints[0].rotation);
        }
    }

    public void SpawnMagazine(int bullets, Vector3 position, Quaternion rotation)
    {
        int activeCount = 0;
        while (magazines[currMagazine].GetComponent<DropMagazineScript>().canPick && activeCount < magazinesCount)
        {
            activeCount++;
            currMagazine++;
            if (currMagazine == magazinesCount)
            {
                currMagazine = 0;
            }
        }
        if (activeCount != magazinesCount)
        {
            magazines[currMagazine].GetComponent<DropMagazineScript>().ammoLeft = bullets;
            magazines[currMagazine].GetComponent<DropMagazineScript>().canPick = true;
            magazines[currMagazine].transform.SetPositionAndRotation(position, rotation);
            currMagazine++;
            if (currMagazine == magazinesCount)
            {
                currMagazine = 0;
            }
        }
    }
}
