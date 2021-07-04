using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapController : MonoBehaviour
{
    public static UnityEvent<TransitionZone.TransitionDirection> TransitionEvent;

    public MapNode currentNode;
    public GameObject mapCamera;

    public GameObject playerPrefab;

    private GameObject player;

    public bool teleportTransitions = false;

    public float lerpStrength = .2f;

    // Start is called before the first frame update
    void Start()
    {
        if (TransitionEvent == null) {
            TransitionEvent = new UnityEvent<TransitionZone.TransitionDirection>();
            TransitionEvent.AddListener(TransitionDirection);
        }

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

    void TransitionDirection (TransitionZone.TransitionDirection direction) {
        switch (direction) {
            case TransitionZone.TransitionDirection.North:
            if (currentNode.northNode != null)
            {
                TransitionToNode(currentNode.northNode);
            }
            break;

            case TransitionZone.TransitionDirection.South:
            if (currentNode.southNode != null)
            {
                TransitionToNode(currentNode.southNode);
            }
            break;

            case TransitionZone.TransitionDirection.East:
            if (currentNode.eastNode != null)
            {
                TransitionToNode(currentNode.eastNode);
            }
            break;

            case TransitionZone.TransitionDirection.West:
            if (currentNode.westNode != null)
            {
                TransitionToNode(currentNode.westNode);
            }
            break;
        }
    }

    void TransitionToNode(MapNode newNode) {

        currentNode.DisableTransitionZones();

        currentNode = newNode;

        transform.position = currentNode.transform.position;
        
        if (player && teleportTransitions) {
            player.transform.position = currentNode.transform.position;
        }

        currentNode.EnableTransitionZones();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentNode) {
           LerpCameraToCurrentNode(lerpStrength);
        }
    }
}
