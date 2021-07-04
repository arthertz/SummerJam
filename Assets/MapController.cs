using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public MapNode currentNode;
    public GameObject mapCamera;

    public GameObject playerPrefab;

    private GameObject player;

    public float lerpStrength = .2f;

    // Start is called before the first frame update
    void Start()
    {
        if (!player && playerPrefab) {
            player = GameObject.Instantiate(
                playerPrefab,
                currentNode.cameraTarget.transform.position,
                Quaternion.identity
            );
        }
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

    void TransitionToNode(MapNode newNode) {
        currentNode = newNode;

        transform.position = currentNode.transform.position;
        
        if (player) {
            player.transform.position = currentNode.transform.position;
        }
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
                TransitionToNode(currentNode.northNode);
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentNode.westNode != null)
            {
                TransitionToNode(currentNode.westNode);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentNode.southNode != null)
            {
                TransitionToNode(currentNode.southNode);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentNode.eastNode != null)
            {
                TransitionToNode(currentNode.eastNode);
            }
        }
    }
}
