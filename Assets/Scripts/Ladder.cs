using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour, IInteractable
{
    bool climbAnim = true;

    bool isMoving = false;
    [SerializeField] Transform topPoint;
    [SerializeField] Transform bottomPoint;
    [SerializeField] float duration = 2f;
    [SerializeField] AudioSource audioSource;

    public bool CanInteract()
    {
        return !isMoving;
    }

    public bool GetClimbAnim()
    {
        return climbAnim;
    }

    public void Interact()
    {
        if(!CanInteract()) return;
        
        GameObject player = GameObject.FindWithTag("Player");
        if(player == null) return;

        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        Animator playerAnimator = player.GetComponent<Animator>();
        //set velocity to 0 and disable movement
        playerAnimator.SetBool("IsClimbing", true);

        playerMovement.enabled = false;
        playerRb.velocity = Vector2.zero;
        playerRb.Sleep();

        Vector3 distToTop = topPoint.position - player.transform.position;
        Vector3 distToBottom = bottomPoint.position - player.transform.position;
        if(distToTop.magnitude < distToBottom.magnitude)
        {
            StartCoroutine(MoveLadder(player.transform, playerAnimator, topPoint.position, bottomPoint.position));
        } else
        {
            StartCoroutine(MoveLadder(player.transform, playerAnimator, bottomPoint.position, topPoint.position));
        }
    }
    IEnumerator MoveLadder(Transform playerTransform, Animator playerAnimator, Vector2 startPosition, Vector2 targetPosition)
    {
        audioSource.Play();
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
        playerAnimator.SetBool("IsClimbing", false);
        playerTransform.GetComponent<Rigidbody2D>().WakeUp();
        playerTransform.GetComponent<PlayerMovement>().enabled = true;
        audioSource.Stop();
    }
}
