using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : Interactable
{
    public Animator anim;
    public bool open;
    public bool bomb;
    public int kicks;
    int preKicks;
    float timeToExplode;

    private void Start() {
        
        timeToExplode = 5;
    }
    private void Update() {

        //for kicking door
        if(preKicks != kicks){
            if(kicks <= 0){
                open = true;
            }
            preKicks = kicks;
        }
        if(bomb){
            timeToExplode -= Time.deltaTime;
            if(timeToExplode <= 0){
                Destroy(gameObject);
            }
        }
        anim.SetBool("Open", open);
    }

    public override void Interact(int interactionValue)
    {
        //0 = open
        //1 = kick
        //2 = bomb

        if(interactionValue == 0){
            open = !open;
        }

        if(interactionValue == 1){
            kicks -= Random.Range(1, kicks);
        }
        if(interactionValue == 2){
            bomb = true;
        }
        base.Interact(interactionValue);
    }
}
