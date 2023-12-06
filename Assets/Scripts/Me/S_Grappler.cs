using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_Grappler : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] DistanceJoint2D distanceJoint2D;

    [SerializeField] float maxDistance;

    private GameObject[] targets;
    private PlayerMovement playermovement;
    private Vector2 lessDistance;
    private bool canGrappling;


    private void Awake()
    {
        distanceJoint2D.enabled = false;
        playermovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !playermovement.isGrounded)
        {
            targets = GameObject.FindGameObjectsWithTag("Target");

            float tempLess = 1000;

            canGrappling = false;

            for (int i = 0; i < targets.Length; i++)
            {
                float distance = Vector3.Distance(targets[i].transform.position, transform.position);

                if (distance <= maxDistance)
                {
                    canGrappling = true;
                    if (distance < tempLess)
                    {
                        Debug.Log(distance);
                        tempLess = distance;
                        lessDistance = targets[i].transform.position;
                    }
                }
            }

            if (canGrappling)
            {
                lineRenderer.SetPosition(0, lessDistance);
                lineRenderer.SetPosition(1, transform.position);
                distanceJoint2D.connectedAnchor = lessDistance;
                distanceJoint2D.enabled = true;
                lineRenderer.enabled = true;
                playermovement.canMove = false;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) || playermovement.isGrounded)
        {
            distanceJoint2D.enabled = false;
            lineRenderer.enabled = false;
            playermovement.canMove = true;
        }
        if(distanceJoint2D.enabled)
        {
            lineRenderer.SetPosition(1, new Vector3(transform.position.x, transform.position.y + 1, 0));
        }
    }
}
