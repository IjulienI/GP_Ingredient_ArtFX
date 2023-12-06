using UnityEngine;
using UnityEngine.SceneManagement;

public class S_Flag : MonoBehaviour
{
    [SerializeField] Object nextLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (S_ItemsVerifier.VerifieItem("REDGEM") != -1)
            {
                SceneManager.LoadScene(nextLevel.name);
            }
        }
    }
}
