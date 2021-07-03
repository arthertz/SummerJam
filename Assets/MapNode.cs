using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    public MapNode northNode;
    public MapNode eastNode;
    public MapNode southNode;
    public MapNode westNode;
    public GameObject cameraTarget;

    // Start is called before the first frame update
    void Start()
    {
        cameraTarget = new GameObject("CameraTarget");
        cameraTarget.transform.SetParent(transform);
        cameraTarget.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
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
