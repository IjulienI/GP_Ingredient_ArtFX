using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class S_BoatStart : MonoBehaviour
{
    [SerializeField] GameObject boat;
    [SerializeField] GameObject padle;
    [SerializeField] float location;
    [SerializeField] float speed;

    private bool isInside = false;
    private bool isPadle = false;
    public bool finish = false;
    private int index;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        padle.SetActive(false);
    }


    private void Update()
    {
        if(transform.position.x <= location)
        {
            if (isInside && isPadle)
            {
                boat.transform.position = new Vector2(boat.transform.position.x + speed * Time.deltaTime, boat.transform.position.y);
                player.transform.position = new Vector2(player.transform.position.x + speed * Time.deltaTime, player.transform.position.y);
            }
        }
        else
        {
            padle.GetComponent<Animator>().SetBool("Activated", false);
            finish = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isInside = true;
            if (S_ItemsVerifier.VerifieItem("PADLE") != -1)
            {
                index = (S_ItemsVerifier.VerifieItem("PADLE"));

                PlayerInventory.Instance.items.RemoveAt(index);
                InventoryUI.Instance.UpdateUI();

                padle.SetActive(true);
                padle.GetComponent<Animator>().SetBool("Activated", true);
                isPadle = true;
            }
            if(isPadle)
            {
                padle.GetComponent<Animator>().SetBool("Activated", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInside = false;
            padle.GetComponent<Animator>().SetBool("Activated", false);
        }
    }
}
