using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public MapNode currentNode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentNode.northNode != null)
            {
                currentNode = currentNode.northNode;
                transform.position = currentNode.transform.position;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentNode.westNode != null)
            {
                currentNode = currentNode.westNode;
                transform.position = currentNode.transform.position;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentNode.southNode != null)
            {
                currentNode = currentNode.southNode;
                transform.position = currentNode.transform.position;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentNode.eastNode != null)
            {
                currentNode = currentNode.eastNode;
                transform.position = currentNode.transform.position;
            }
        }
    }
}
