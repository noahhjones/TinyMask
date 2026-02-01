using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour, IInteractable
{
    bool isMoving = false;
    [SerializeField] Transform topPoint;
    [SerializeField] Transform bottomPoint;
    [SerializeField] float duration = 2f;
    public bool CanInteract()
    {
        return !isMoving;
    }

    public void Interact()
    {
        if(!CanInteract()) return;
        
        GameObject player = GameObject.FindWithTag("Player");
        if(player == null) return;

        PlayerMovement playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        Rigidbody2D playerRb = playerMovement.GetComponent<Rigidbody2D>();
        //set velocity to 0 and disable movement
        playerMovement.enabled = false;
        playerRb.velocity = Vector2.zero;
        playerRb.Sleep();

        Vector3 distToTop = topPoint.position - player.transform.position;
        Vector3 distToBottom = bottomPoint.position - player.transform.position;
        if(distToTop.magnitude < distToBottom.magnitude)
        {
            StartCoroutine(MoveLadder(player.transform, topPoint.position, bottomPoint.position));
        } else
        {
            StartCoroutine(MoveLadder(player.transform, bottomPoint.position, topPoint.position));
        }
    }
    IEnumerator MoveLadder(Transform playerTransform, Vector2 startPosition, Vector2 targetPosition)
    {
        isMoving = true;
        playerTransform.position = startPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            playerTransform.position = Vector2.Lerp(startPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        playerTransform.position = targetPosition;
        isMoving = false;
        playerTransform.GetComponent<Rigidbody2D>().WakeUp();
        playerTransform.GetComponent<PlayerMovement>().enabled = true;
    }
}
