using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FireBallSpawner : MonoBehaviour
{
    [SerializeField] GameObject fireBall;
    [SerializeField] float delay;
    [SerializeField] Vector2 randomRange;

    private float time;

    private void Update()
    {
        time += 1 * Time.deltaTime;

        if(time > delay+Random.Range(randomRange.x,randomRange.y))
        { 
            Instantiate(fireBall,transform.position,transform.rotation);
            time = 0;
        }
    }
}
