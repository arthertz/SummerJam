using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLerp : MonoBehaviour
{
    private bool spriteLerping = false;

    private float spriteLerp = 0.0f;

    
    private GameObject sprite;

    private Vector3 originalSpriteScale;

    // Start is called before the first frame update
    void Start()
    {
        sprite = transform.Find("sprite").gameObject; // pls don't rename the child
        originalSpriteScale = sprite.transform.localScale;
    }


    public void JumpLerp () {
        // shit for graphics lerping
            spriteLerping = true;
            spriteLerp = 0.0f;
            sprite.transform.localScale = originalSpriteScale;
    }

    // Update is called once per frame
    void Update()
    {
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
}
