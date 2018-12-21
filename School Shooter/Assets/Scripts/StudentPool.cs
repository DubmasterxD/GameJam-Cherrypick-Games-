using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentPool : MonoBehaviour
{

    public GameObject studentPrefab;
    public int studentCount;
    public List<Transform> spawnPoints;
    public Text studentsLeftText;
    public Text studentsLeftTextShadow;


    // Use this for initialization
    void Start()
    {
        SpawnStudents();
        SetStudentsLeftText();
    }

    void SpawnStudents()
    {
        for (int i = 0; i < studentCount; i++)
        {
            int point = Random.Range(0, spawnPoints.Count);
            Instantiate(studentPrefab, spawnPoints[point].position, spawnPoints[point].rotation);
            spawnPoints.RemoveAt(point);
        }
    }

    public void SetStudentsLeftText()
    {
        studentsLeftText.text = "Students left : " + studentCount;
        studentsLeftTextShadow.text = "Students left : " + studentCount;
    }
}
