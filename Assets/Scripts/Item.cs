using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemSO itemSO;

    private AudioSource collectAudio;

    private float fallSpeed = 10f;

    private Transform itemTransform;
    private Vector2 moveDown = Vector2.down;

    private Score score;

    public ItemSO GetItemSO()
    {
        return itemSO;
    }

    private void Awake()
    {
        
        itemTransform = transform;
        score = FindAnyObjectByType<Score>();

        if (score != null)
        {
            //Debug.Log(score.gameObject);
        }
        else
        {
            Debug.Log("Score class not found!");
        }

        collectAudio = gameObject.GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        itemTransform.Translate(moveDown * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<Player>())
        {
            StartCoroutine(Delay(1));
            float newScore = itemSO.score;

            score.UpdatePlayerScore(newScore);

            collectAudio.Play();

            
            //Debug.Log("Destroy " + itemSO.objectName);
            Destroy(gameObject);
        }
    }

    IEnumerator Delay(int delayTime)
    {
        yield return new WaitForSeconds(1);
    }
}
