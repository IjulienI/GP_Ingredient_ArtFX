using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class S_Lever : MonoBehaviour
{
    [SerializeField] GameObject bridge;
    [SerializeField] GameObject InteractCanvas;

    private bool canInteract = false;
    private bool haveItem = false;
    private int index;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canInteract && haveItem)
            {
                InteractCanvas.SetActive(false);

                bridge.GetComponent<Animator>().SetBool("Down", true);
                gameObject.GetComponent<Animator>().SetBool("Activate", true);

                haveItem = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canInteract = true;

            if (S_ItemsVerifier.VerifieItem("LEVERHANDLE") != -1)
            {
                index = (S_ItemsVerifier.VerifieItem("LEVERHANDLE"));

                PlayerInventory.Instance.items.RemoveAt(index);
                InventoryUI.Instance.UpdateUI();

                haveItem = true;

                gameObject.GetComponent<Animator>().SetBool("Repair", true);
            }
            if (haveItem)
            {
                InteractCanvas.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(InteractCanvas.activeInHierarchy)
            {
                InteractCanvas.SetActive(false);

                canInteract = false;
            }
        }
    }
}
