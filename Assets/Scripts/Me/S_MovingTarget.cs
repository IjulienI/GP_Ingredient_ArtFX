using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MovingTarget : MonoBehaviour
{
    [SerializeField] private float location;
    [SerializeField] private float speed = 2f;

    [SerializeField] private bool vertical;
    [SerializeField] private bool horizontal;

    private Vector2 goFrom;

    private bool up;
    private bool right;

    private void Awake()
    {
        goFrom = transform.position;

        if (vertical)
        {
            if(goFrom.y < location)
            {
                up = false;
            }
            else
            {
                up = true;
            }
        }
        if (horizontal)
        {
            if (goFrom.x < location)
            {
                right = true;
            }
            else
            {
                right = false;
            }
        }
    }

    private void Update()
    {
        if (horizontal)
        {
            if(right)
            {
                if (transform.position.x >= location)
                {
                    transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
                }
                else
                {
                    float locTemp = location;
                    float goTemp = goFrom.x;
                    location = goTemp;
                    goFrom.x = locTemp;
                    right = false;
                }
            }
            else
            {
                if (transform.position.x <= location)
                {
                    transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
                }
                else
                {
                    float locTemp = location;
                    float goTemp = goFrom.x;
                    location = goTemp;
                    goFrom.x = locTemp;
                    right = true;
                }
            }
        }
        if(vertical)
        {
            if(up)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
            }
        }
    }
}
