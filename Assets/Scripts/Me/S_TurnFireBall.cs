using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class S_TurnFireBall : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private int damage = 1;
    [SerializeField] private float startRotation;
    private Animator animator;

    private bool inside;
    private float delay;
    private float tempDelay;
    private GameObject victime;

    private void Awake()
    {
        startRotation /= 360;
        animator = GetComponent<Animator>();
        animator.speed = speed;
        animator.Play("TurnFire", 0, startRotation);
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
