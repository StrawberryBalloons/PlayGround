using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue;

    private List<int> modifiers = new List<int>();

    public int GetValue(){//this value means you get all the armour modifiers instead of just the one
    //or it could be fine...
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void AddModifier(int modifier){
        if(modifier != 0){
            modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(int modifier){
        if(modifier != 0){
            modifiers.Remove(modifier);
        }
    }

}
