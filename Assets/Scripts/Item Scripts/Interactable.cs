using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 1.1f;

    bool isFocus = false;
    bool hasInteracted = false;
    public Transform interactionTransform;

    Transform player;


    public virtual void Interact(){
        //meant to be overriden;
        //Debug.Log("interacting with: " + interactionTransform.name);
    }
    public void FixedUpdate(){
        if(isFocus && hasInteracted == false){
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if(distance <= radius){
                Interact();
                Debug.Log("INTERACT ");
                hasInteracted = true;
            }
        }
    }
    public void OnFocused(Transform playerTransform){
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }
    public void OnUnFocus(){
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    void OnDrawGizmosSelected(){
        if(interactionTransform == null){
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
