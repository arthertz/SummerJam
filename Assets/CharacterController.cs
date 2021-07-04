using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private float horizontalForce = 0.0f;
    public float horizontalPower = 1.0f;
    public float hoverPower = 100.0f;
    public float dampeningParam = .5f;
    public float maxHorizontalVelocity = 2f;

    private Rigidbody2D body;

    public float hoverHeight = 1f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        horizontalForce = Input.GetAxis("Horizontal") * horizontalPower;
        body.AddForce(horizontalForce * Vector2.right);
        body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * Mathf.Min(body.velocity.magnitude, maxHorizontalVelocity),
                                        body.velocity.y);

        if (Mathf.Abs(Input.GetAxis("Horizontal")) < .05)
        {
            body.velocity -= new Vector2(body.velocity.x, 0);
        }

        RaycastHit2D hit;
        Ray2D downRay = new Ray2D(transform.position, Vector2.down);
        hit = Physics2D.Raycast(downRay.origin, downRay.direction, hoverHeight);


        if (hit.collider != null)
        {
            float hoverDifference = hoverHeight - hit.distance;
            Debug.Log("hit object " + hit.collider.transform.name);
            Debug.Log("hit distance: " + hit.distance);
            Debug.Log("hover height: " + hoverHeight);

            if (hoverDifference > 0)
            {
                // Subtract the damping from the lifting force and apply it to
                // the rigidbody.
                float upwardSpeed = body.velocity.y;
                float lift = hoverDifference * hoverPower - upwardSpeed * dampeningParam;
                body.AddForce(lift * Vector3.up);
            }
        }
        Debug.DrawRay(downRay.origin, downRay.direction, Color.red);
    }
}
