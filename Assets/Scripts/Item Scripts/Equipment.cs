using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public SkinnedMeshRenderer skinnedMesh;
    public GameObject gameObject;
    public EquipmentMeshRegion[] coveredMeshRegions;

    public int armourModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();
        //equip item
        EquipmentManager.instance.Equip(this);
        //remove item from inven
        RemoveFromInventory();
    }
}


public enum EquipmentSlot {Head, Chest, Legs, Feet, MainHand, OffHand}
public enum EquipmentMeshRegion {Legs, Arms, Torso, RightHand, LeftHand}; //body blend shapes
