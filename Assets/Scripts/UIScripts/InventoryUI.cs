﻿using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public Transform itemsParent;
    ItemSlot[] slots;
    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<ItemSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Inventory")){
            inventoryUI.SetActive(!inventoryUI.activeSelf); 
        }        
    }
    void UpdateUI(){
        Debug.Log("updating Inventory");
        for(int i = 0; i < slots.Length; i++){
            if (i < inventory.items.Count){
                slots[i].AddItem(inventory.items[i]);
            } else{
                slots[i].ClearSlot();
            }
        }
    }
}
