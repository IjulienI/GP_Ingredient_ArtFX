using UnityEngine;

public class S_FallingLoop : MonoBehaviour
{
    private GameObject player;
    private GameObject playerCam;

    [SerializeField] int loop = 0;
    [SerializeField] bool vertical;
    [SerializeField] float location;
    [SerializeField] bool camLock;
    [SerializeField] Vector2 CamLockMin;
    [SerializeField] Vector2 CamLockMax;
    [SerializeField] PhysicsMaterial2D fallMat;
    private Vector2 tempCamMin;
    private Vector2 tempCamMax;
    private int loopCount = 0;
    

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            if (camLock)
            {
                tempCamMin = playerCam.GetComponent<CameraMovement>().minPosition;
                tempCamMax = playerCam.GetComponent<CameraMovement>().maxPosition;

                playerCam.GetComponent<CameraMovement>().minPosition = CamLockMin;
                playerCam.GetComponent<CameraMovement>().maxPosition = CamLockMax;
            }
            else
            {
                playerCam.GetComponent<CameraMovement>().smoothingSpeed = 0;
            }
            if (loop != loopCount)
            {
                if(vertical)
                {
                    player.transform.position = new Vector2(player.transform.position.x, location);
                }
                else
                {
                    player.transform.position = new Vector2(location, player.transform.position.y);
                }
                if (vertical)
                {
                    player.GetComponent<PlayerMovement>().downGravity = 0f;
                    player.GetComponent<Rigidbody2D>().gravityScale = 0f;
                    player.GetComponent<Rigidbody2D>().sharedMaterial = fallMat;
                    player.GetComponent<CapsuleCollider2D>().sharedMaterial = fallMat;
                }
                loopCount++;
            }
            else if (camLock)
            {
                playerCam.GetComponent<CameraMovement>().minPosition = tempCamMin;
                playerCam.GetComponent<CameraMovement>().maxPosition = tempCamMax;
                Exit();
            }
            else
            {
                playerCam.GetComponent<CameraMovement>().smoothingSpeed = 2;
                Exit();
            }
        }
    }

    public void Exit()
    {
        if (vertical)
        {
            player.GetComponent<PlayerMovement>().downGravity = 4f;
            player.GetComponent<Rigidbody2D>().gravityScale = 1f;
            player.GetComponent<Rigidbody2D>().sharedMaterial = null;
            player.GetComponent<CapsuleCollider2D>().sharedMaterial = null;
            loopCount = 0;
        }
    }
}
