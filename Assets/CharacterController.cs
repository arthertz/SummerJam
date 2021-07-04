using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private float horizontalForce = 0.0f;

    public float horizontalPower = 1.0f;
    private float hoverPower = 5.0f;

    private Rigidbody2D body;

    private float hoverHeight = 1f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //horizontalForce = Input.GetAxis("Horizontal") * power;
        //body.AddForce(horizontalForce * Vector2.right);
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit;
        Ray2D downRay = new Ray2D(transform.position, -Vector2.up);

        hit = Physics2D.Raycast(downRay.origin, downRay.direction);
        if (hit.collider != null)
        {
            float hoverDifference = hoverHeight - hit.distance;
            Debug.Log("hit distance: " + hit.distance);
            Debug.Log("hover height: " + hoverHeight);

            if (hoverDifference > 0)
            {
                // Subtract the damping from the lifting force and apply it to
                // the rigidbody.
                float upwardSpeed = body.velocity.y;
                float lift = hoverDifference * hoverPower - upwardSpeed * 0.5f;
                body.AddForce(lift * Vector3.up);
            }
        }
        Debug.DrawRay(downRay.origin, downRay.direction, Color.red);
    }
}
