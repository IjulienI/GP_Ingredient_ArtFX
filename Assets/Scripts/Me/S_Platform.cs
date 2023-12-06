using Unity.VisualScripting;
using UnityEngine;

public class S_Platform : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayers;
    private Transform feet;

    private GameObject player;
    private Vector2 playerHeight;
    private Vector2 platformHeight;
    private bool activate = true;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        feet = player.transform.GetChild(0).transform;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            CheckGround();
        }

        playerHeight = player.GetComponent<CapsuleCollider2D>().size/4f;
        platformHeight = GetComponent<BoxCollider2D>().size/2;

        if (player.transform.localPosition.y + playerHeight.y >= transform.position.y + platformHeight.y) 
        { 
            if(activate)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        else if (gameObject.GetComponent<BoxCollider2D>().enabled == true)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            activate = true;
        }
    }

    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(feet.transform.position, Vector2.down, 0.1f, groundLayers);
        if(hit)
        {
            if(hit.collider.tag == "Platform")
            {
                activate = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
