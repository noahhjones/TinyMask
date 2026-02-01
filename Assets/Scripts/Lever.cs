using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    public bool isPulled { get; private set; }
    [SerializeField] public bool oneUse;

    public Sprite unpulledSprite;
    public Sprite pulledSprite;

    public bool CanInteract()
    {
        if(!oneUse) return true;

        return !isPulled;
    }

    public void Interact()
    {
        if(!CanInteract()) return;

        PullLever();
    }

    private void PullLever()
    {
        SetPulled(!isPulled);

        //Whatever the lever does
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
