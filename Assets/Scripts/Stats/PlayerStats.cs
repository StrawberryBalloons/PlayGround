using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        //armour.AddModifier(newItem.armourModifier)
        if (newItem != null)
        {
            getArmour((int)newItem.equipSlot).AddModifier(newItem.armourModifier);
            damage.AddModifier(newItem.damageModifier);
        }
        if (oldItem != null)
        {
            getArmour((int)oldItem.equipSlot).RemoveModifier(oldItem.armourModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }


    public override void Die()
    {
        base.Die();
        //kill the player
        PlayerManager.instance.KillPlayer();
    }
}
