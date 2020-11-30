using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();

        PickupItem();
    }

    void PickupItem(){
        
        Debug.Log("picking up: " + item.name);
        //add to inven
        bool wasPickedUp = Inventory.instance.Add(item);
        //destroy item
        if(wasPickedUp){
        Destroy(gameObject);}
    }

}
