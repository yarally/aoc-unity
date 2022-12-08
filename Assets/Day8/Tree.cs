using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private GameObject sensor;
    [SerializeField] private GameObject spawner;
    public int Score { get; private set; } = 1;

    public void FindScenicScore()
    {
        var position = spawner.transform.position;
        Instantiate(sensor, position, Quaternion.Euler(Vector3.up * 90)).GetComponent<Sensor>().SetParent(this);
        Instantiate(sensor, position, Quaternion.Euler(Vector3.up * 180)).GetComponent<Sensor>().SetParent(this);
        Instantiate(sensor, position, Quaternion.Euler(Vector3.up * 270)).GetComponent<Sensor>().SetParent(this);
        Instantiate(sensor, position, Quaternion.Euler(Vector3.zero)).GetComponent<Sensor>().SetParent(this);
    }

    public void ReceiveSensor(int score)
    {
        Score *= score;
    } 
}
