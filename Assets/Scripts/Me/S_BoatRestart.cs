using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BoatRestart : MonoBehaviour
{
    [SerializeField] GameObject boat;
    [SerializeField] GameObject logic;
    private float originePos;

    private void Awake()
    {
        originePos = transform.position.x;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !logic.GetComponent<S_BoatStart>().finish)
        {
            boat.transform.position = new Vector2 (originePos, transform.position.y);
        }
    }
}
