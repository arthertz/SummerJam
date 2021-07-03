using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public MapNode currentNode;
    public GameObject mapCamera;

    public float lerpStrength = .2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    void LerpCameraToCurrentNode(float lerpStrength) {
        //Get Z component of current position
        float _zComponent = mapCamera.transform.position.z;
        print(_zComponent);

        Vector3 target = new Vector3(
            currentNode.cameraTarget.transform.position.x,
            currentNode.cameraTarget.transform.position.y,
            _zComponent
            );

        mapCamera.transform.position = Vector3.Lerp(mapCamera.transform.position, target, lerpStrength);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentNode) {
           LerpCameraToCurrentNode(lerpStrength);
        }

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
