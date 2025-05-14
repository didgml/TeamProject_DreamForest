using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    private bool isCollidedWithPlayer = false;
    private Transform playerTransform;
    private bool isAttachedToPlayer = false;
    private bool isHolding = false;

    private Transform bodyAttachPoint;
    private Transform leftHand;
    private Transform rightHand;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCollidedWithPlayer = true;
            playerTransform = collision.transform;
            bodyAttachPoint = playerTransform.Find("BodyAttachPoint");
            leftHand = playerTransform.Find("LeftHand");
            rightHand = playerTransform.Find("RightHand");
            Debug.Log("Interaction with the player started.");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCollidedWithPlayer = false;
            playerTransform = null;
            DetachFromPlayer();
            Debug.Log("Interaction with the player ended.");
        }
    }

    private void Update()
    {
        if (isCollidedWithPlayer && playerTransform != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isHolding = !isHolding;
            }

            if (isHolding)
            {
                AttachToPlayerObject();
            }
            else
            {
                DetachFromPlayer();
            }
        }
    }

    private void AttachToPlayerObject()
    {
        if (!isAttachedToPlayer)
        {
            transform.position = bodyAttachPoint.position;
            transform.rotation = bodyAttachPoint.rotation;
            isAttachedToPlayer = true;
            Debug.Log("Mouse attached to the player.");
        }
        else
        {
            transform.position = Vector3.Lerp(leftHand.position, rightHand.position, 0.5f);
        }
    }

    private void DetachFromPlayer()
    {
        if (isAttachedToPlayer)
        {
            isAttachedToPlayer = false;
            Debug.Log("Mouse detached from the player.");
        }
    }
}
