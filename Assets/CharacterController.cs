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
    public float hoverHeight = 1f;
    private Rigidbody2D body;

    private bool grounded = false; // for ground detection
    private bool ascending = false;
    private int jumpNum = 0; // for double jump

    private Vector3 originalSpriteScale;
    private bool spriteLerping = false;
    private float spriteLerp = 0.0f;
    private GameObject sprite;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = transform.Find("sprite").gameObject; // pls don't rename the child
        originalSpriteScale = sprite.transform.localScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ((grounded && !ascending) || jumpNum < 1))
        {
            body.AddForce(500f * Vector2.up);
            ascending = true;
            jumpNum++;

            // shit for graphics lerping
            spriteLerping = true;
            spriteLerp = 0.0f;
            sprite.transform.localScale = originalSpriteScale;
        }
        if (!Input.GetKey(KeyCode.Space))
        {
            body.gravityScale = 3f;
        }
        else
        {
            body.gravityScale = 2f;
        }

        if (spriteLerping)
        {
            spriteLerp += Time.deltaTime * 2f;
            Vector3 sizeChange;
            if (spriteLerp < 0.6f)
            {
                sizeChange = new Vector3(sprite.transform.localScale.x - spriteLerp / 80f, sprite.transform.localScale.y + spriteLerp / 80f, sprite.transform.localScale.z);
            }
            else
            {
                sizeChange = new Vector3(sprite.transform.localScale.x + spriteLerp / 100f, sprite.transform.localScale.y - spriteLerp / 100f, sprite.transform.localScale.z);
            }
            sprite.transform.localScale = sizeChange;
            if (spriteLerp >= 1f)
            {
                sprite.transform.localScale = originalSpriteScale;
                spriteLerping = false;
            }
        }
    }

    void Update () {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) < .05)
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) < .05)
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }
        horizontalForce = Input.GetAxis("Horizontal") * horizontalPower;
        body.AddForce(horizontalForce * Vector2.right);
        body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * Mathf.Min(Mathf.Abs(body.velocity.x), maxHorizontalVelocity),
                                        body.velocity.y);


        RaycastHit2D hit;
        Ray2D downRay = new Ray2D(transform.position, Vector2.down);
        hit = Physics2D.Raycast(downRay.origin, downRay.direction, hoverHeight);


        if (hit.collider != null)
        {
            float hoverDifference = hoverHeight - hit.distance;

            if (hoverDifference < 0.2f)
            {
                ascending = false;
                grounded = true;
                jumpNum = 0;
            }

            if (hoverDifference > 0)
            {
                // Subtract the damping from the lifting force and apply it to
                // the rigidbody.
                float upwardSpeed = body.velocity.y;
                float lift = hoverDifference * hoverPower - upwardSpeed * dampeningParam;
                body.AddForce(lift * Vector3.up);
            }
        }
        else
        {
            grounded = false;
        }
        //Debug.DrawRay(downRay.origin, downRay.direction, Color.red);
    }
}
