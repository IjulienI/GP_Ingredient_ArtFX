using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class S_FireBall : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float height;

    private bool inside;
    private float delay;
    private float tempDelay;
    private GameObject victime;
    private Rigidbody2D rb;
    private float destroyLoc;

    private void Awake()
    {
        destroyLoc = transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2 (0, height);
    }
    private void Update()
    {
        if (inside)
        {
            if (delay > tempDelay)
            {
                tempDelay = delay + 0.5f;
                if (victime.gameObject.CompareTag("Player"))
                {
                    victime.gameObject.GetComponent<PlayerLife>().Hurt(damage);
                }
            }
        }
        else
        {
            tempDelay = delay++;
        }
        if(transform.position.y < destroyLoc)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inside = true;
        victime = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inside = false;
        victime = null;
    }
}
