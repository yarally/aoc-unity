using System;
using System.Collections;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    private Tree _parent;
    private Day8Controller _controller;
    private Vector3 _startPos;

    private void Start()
    {
        _controller = FindObjectOfType<Day8Controller>();
        _startPos = transform.position;
        StartCoroutine(UpdateAsync());
    }

    IEnumerator UpdateAsync()
    {
        while (true)
        {
            transform.position += transform.forward * 0.5f;
            var pos = transform.position;
            var x = pos.x;
            var z = pos.z;
            if (x < 0 || x > _controller.Size || z < 0 || z > _controller.Size)
            {
                Kill();
                yield break;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void SetParent(Tree parent)
    {
        _parent = parent;
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Tree>())
        {
            return;
        }
        if (_parent != null && other.GetComponent<Tree>().GetInstanceID() != _parent.GetInstanceID())
        {
            Kill();
        }
    }

    private void Kill()
    {
        _parent.ReceiveSensor((int) Math.Round((transform.position - _startPos).magnitude));
        Destroy(gameObject);
    }
}