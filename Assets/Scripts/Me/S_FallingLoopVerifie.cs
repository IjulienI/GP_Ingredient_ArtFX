using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FallingLoopVerifie : MonoBehaviour
{
    [SerializeField] GameObject fallingLoop;
    private void OnTriggerExit2D(Collider2D collision)
    {
        fallingLoop.GetComponent<S_FallingLoop>().Exit();
    }
}
