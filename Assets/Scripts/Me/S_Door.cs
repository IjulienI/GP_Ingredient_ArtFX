using UnityEngine;

public class S_Door : MonoBehaviour
{
    private GameObject player;

    private bool canEnter = false;
    private bool fadeIn = false;

    private float delay;

    private GameObject playerCam;

    [SerializeField] Vector2 location;
    [SerializeField] GameObject fade;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && canEnter)
        {
            canEnter = false;
            fade.GetComponent<Animator>().SetTrigger("FadeIn");
            fadeIn = true;
        }
        if (fadeIn)
        {
            delay += 1 * Time.deltaTime;
            if (delay >= 0.8f)
            {
                fadeIn = false;
                player.transform.position = location;
                fade.GetComponent<Animator>().SetTrigger("FadeOut");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canEnter = false;
        }
    }
}
