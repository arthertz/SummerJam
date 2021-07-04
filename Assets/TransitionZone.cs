using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionZone : MonoBehaviour
{
    public enum TransitionDirection {
        North,
        South,
        East,
        West
    }

    public TransitionDirection zoneDirection;

    void OnTriggerEnter2D (Collider2D collider2D) {
        MapController.TransitionEvent.Invoke(zoneDirection);
    }
}
