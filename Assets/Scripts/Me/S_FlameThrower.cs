using UnityEngine;

public class S_FlameThrower : MonoBehaviour
{
    private float delay;
    [SerializeField] private GameObject fire;
    [SerializeField] private int damage = 1;

    public bool startActivated;

    public float activatedDelay;
    public float desactivateDelay;

    private bool activated;
    private bool inside = false;
    private GameObject victime;
    private float tempDelay;
    private float activatedDelayAlias;
    private float desactivateDelayAlias;

    private void Awake()
    {
        activatedDelayAlias = activatedDelay + desactivateDelay;
        desactivateDelayAlias = desactivateDelay;
        if (!startActivated)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            fire.GetComponent<ParticleSystem>().Stop();
        }
    }

    private void Update()
    {
        delay += 1f * Time.deltaTime;

        if (activated)
        {
            if(delay >= activatedDelayAlias )
            {
                //Desactivate !!
                activatedDelayAlias = delay+activatedDelay + desactivateDelay; 
                activated = false;
                GetComponent<BoxCollider2D>().enabled = false;
                fire.GetComponent<ParticleSystem>().Stop();
            }
        }
        else
        {
            if (delay >= desactivateDelayAlias)
            {
                //Activate !!
                desactivateDelayAlias = delay + desactivateDelay + activatedDelay;
                activated = true;
                GetComponent<BoxCollider2D>().enabled = true;
                fire.GetComponent<ParticleSystem>().Play();
            }
        }

        if(inside)
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
