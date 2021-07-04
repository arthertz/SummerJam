using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{

    public List<TransitionZone> transitionZones = new List<TransitionZone>();

    public MapNode northNode;
    public MapNode eastNode;
    public MapNode southNode;
    public MapNode westNode;

    public GameObject cameraTarget;

    private BoxCollider2D[] boundaryTriggers;

    // Start is called before the first frame update
    void Start()
    {
        cameraTarget = new GameObject("CameraTarget");
        cameraTarget.transform.SetParent(transform);
        cameraTarget.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }

    public void EnableTransitionZones () {
        foreach (TransitionZone tz in transitionZones) {
            tz.enabled = true;
        }
    }

    public void DisableTransitionZones () {
        foreach (TransitionZone tz in transitionZones) {
            tz.enabled = false;
        }
    }

    void DrawDebugConnection(MapNode otherNode) {
        if (!otherNode) {
            return;
        }
        Gizmos.DrawLine(transform.position, otherNode.transform.position);
    }

    void OnDrawGizmos () {
        Gizmos.color = Color.red;
        DrawDebugConnection(westNode);
        DrawDebugConnection(southNode);
        DrawDebugConnection(eastNode);
        DrawDebugConnection(northNode);
    }
}
