using System;
using System.Collections;
using UnityEngine;

public class Day8Controller : MonoBehaviour
{
    [SerializeField] private GameObject treePrefab;

    [SerializeField] private TextAsset input;

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
                    if (tree.Score > maxScore)
                    {
                        maxScore = tree.Score;
                        godTree = tree;
                    }
                }

                godTree.transform.localScale *= 5;
                godTree.GetComponentInChildren<Animator>().Play(0);
                Debug.Log(godTree.Score);
                break;
            }
        }
    }
}