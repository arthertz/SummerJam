using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{

    List<TransitionZone> transitionZones = new List<TransitionZone>();

    public MapNode northNode;
    public MapNode eastNode;
    public MapNode southNode;
    public MapNode westNode;

    public GameObject cameraTarget;

    private BoxCollider2D[] boundaryTriggers;

    // Start is called before the first frame update
    void Start()
    {
        InitializeTransitionZoneList();
        DisableTransitionZones();
        cameraTarget = new GameObject("CameraTarget");
        cameraTarget.transform.SetParent(transform);
        cameraTarget.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }


    void InitializeTransitionZoneList () {
        transitionZones.Clear();

        foreach (TransitionZone tz in transform.GetComponentsInChildren(typeof(TransitionZone))) {
            transitionZones.Add(tz);
        }
    }


    public void EnableTransitionZones () {
        foreach (TransitionZone tz in transitionZones) {
            tz.gameObject.SetActive(true);
        }
    }

    public void DisableTransitionZones () {
        foreach (TransitionZone tz in transitionZones) {
            tz.gameObject.SetActive(false);
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

        Gizmos.color = Color.green;
        foreach (TransitionZone tz in transitionZones) {
            Gizmos.DrawLine(tz.transform.position, transform.position);
        }
    }
}
