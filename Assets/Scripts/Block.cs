using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //Configuration parameters
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject destroyVFX;
    [SerializeField] int maxHits = 3;
    [SerializeField] Sprite[] hitSprites;

    //Cached reference
    Level level;
    GameStatus status;

    //State variables
    [SerializeField] int timesHit = 0; //Serialized for debugging

    void Start()
    {
        level = FindObjectOfType<Level>();
        status = FindObjectOfType<GameStatus>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length;
        if (timesHit >= maxHits)
            DestroyBlock();
        else
            ShowNextHitSprite();
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit;
        if (hitSprites[spriteIndex] != null)
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        else
            Debug.LogError("Block sprite is missing from array: " + gameObject.name);
    }

    void DestroyBlock()
    {
        status.AddToScore();
        TriggerDestroySFX();
        TriggerDestroyVFX();
        level.BlockDestroyed();
        Destroy(gameObject);
    }

    private void TriggerDestroySFX()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerDestroyVFX()
    {
        GameObject sparkles = Instantiate(destroyVFX,transform.position,transform.rotation);
        Destroy(sparkles, 1f);
    }
}
