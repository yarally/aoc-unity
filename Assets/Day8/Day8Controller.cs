using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Day8Controller : MonoBehaviour
{
    [SerializeField] private GameObject treePrefab;

    [SerializeField] private TextAsset input;
    [SerializeField] private int spawnLimit;
    [SerializeField] private float sensorStepSize;
    [SerializeField] private float sensorInterval;
    public float SensorStepSize => sensorStepSize;
    public float SensorInterval => sensorInterval;

    public int Size { get; private set; }

    void Start()
    {
        StartCoroutine(SpawnTrees());
    }

    private IEnumerator SpawnTrees()
    {
        var col = 0;
        var row = 0;
        foreach (var line in input.text.Split("\r\n"))
        {
            col = 0;
            foreach (var tree in line)
            {
                var objRef = Instantiate(treePrefab, new Vector3(col++, 0, row), Quaternion.identity);
                objRef.transform.localScale *= int.Parse(tree.ToString());
            }

            yield return null;
            row++;
        }

        Size = row - 1;

        yield return new WaitForSeconds(3f);

        foreach (var tree in FindObjectsOfType<Tree>())
        {
            while (FindObjectsOfType<Sensor>().Length > spawnLimit)
            {
                yield return null;
            }
            tree.FindScenicScore();
        }
        
        yield return CheckScore();
    }

    private IEnumerator CheckScore()
    {
        while (true)
        {
            if (FindObjectsOfType<Sensor>().Length > 0)
            {
                yield return new WaitForSeconds(1f);
            }
            else
            {
                var maxScore = 0;
                Tree godTree = null;
                foreach (var tree in FindObjectsOfType<Tree>())
                {
                    Debug.Log(tree.transform.position + " :: " + tree.Score);
                    if (tree.Score > maxScore)
                    {
                        maxScore = tree.Score;
                        godTree = tree;
                    }
                }

                godTree.transform.localScale *= 5;
                godTree.GetComponentInChildren<Animator>().Play(0);
                FindObjectOfType<TextMeshProUGUI>().text = "Highest Score: " + godTree.Score;
                break;
            }
        }
    }
}