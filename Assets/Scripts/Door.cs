using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool isOpened = false;

    [SerializeField] float doorSpeedTime = 5f;
    [SerializeField] Vector2 position1;
    [SerializeField] Vector2 position2 = Vector2.zero;

    Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        position1 = tf.position;
    }

    public void ToggleDoor()
    {
        StopAllCoroutines();

        Vector2 target;
        if (isOpened)
        {
            target = position1;
        }
        else
        {
            target = position2;
        }

        isOpened = !isOpened;
        StartCoroutine(MoveDoor(target));
    }

    IEnumerator MoveDoor(Vector2 targetPosition)
    {
        Vector2 startPosition = tf.position;
        float timeElapsed = 0f;

        while (timeElapsed < doorSpeedTime)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, timeElapsed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; 
    }
}
