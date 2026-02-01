using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour, IInteractable
{
    bool climbAnim = false;
    public bool isPulled { get; private set; }
    [SerializeField] public bool oneUse;

    public Sprite unpulledSprite;
    public Sprite pulledSprite;

    public UnityEvent onPull;

    public bool CanInteract()
    {
        if(!oneUse) return true;

        return !isPulled;
    }

    public bool GetClimbAnim()
    {
        return climbAnim; 
    }

    public void Interact()
    {
        if(!CanInteract()) return;

        PullLever();
    }

    private void PullLever()
    {
        SetPulled(!isPulled);

        //Debug.Log("Pulled");
        onPull?.Invoke();
    }

    public void SetPulled(bool pulled)
    {

        if (isPulled = pulled)
        {
            GetComponent<SpriteRenderer>().sprite = pulledSprite;
        } else
        {
            GetComponent<SpriteRenderer>().sprite = unpulledSprite;
        }
    }
}
