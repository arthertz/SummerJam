using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    private GameObject topSprite;
    private GameObject bottomSprite;

    private bool active = false;
    private float sizeLerp = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        topSprite = transform.Find("top sprite").gameObject;
        bottomSprite = transform.Find("bottom sprite").gameObject;
        Begin();
    }

    // Update is called once per frame
    void Update()
    {
        if (active == true)
        {
            sizeLerp += Time.deltaTime / 2f;
            bottomSprite.transform.localScale = Vector3.one * sizeLerp * 10f;
            
            if (sizeLerp >= 0.3f)
            {
                topSprite.transform.localScale = Vector3.one * (sizeLerp * 12f - 3f); // used dumb magic numbers will go back and fix this
            }
            
            if (sizeLerp >= 1.0f)
            {
                topSprite.transform.localScale = Vector3.one;
                bottomSprite.transform.localScale = Vector3.one;
                topSprite.SetActive(false);
                bottomSprite.SetActive(false);
                active = false;
            }
        }
    }

    public void Begin()
    {
        topSprite.SetActive(true);
        bottomSprite.SetActive(true);
        active = true;
    }
}
