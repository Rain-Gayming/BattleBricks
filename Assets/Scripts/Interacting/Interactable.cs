using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public List<Interaction> interactions;
    public bool destroy;
    public virtual void Interact(int interactionValue)
    {
        if(destroy){
            Destroy(gameObject);
        }
    }
}
[System.Serializable]
public class Interaction
{
    public string interactionName;
}
