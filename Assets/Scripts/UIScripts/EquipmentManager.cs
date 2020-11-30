using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    Equipment[] currentEquipment;
    public SkinnedMeshRenderer targetMesh;

    SkinnedMeshRenderer[] currentMeshes;
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    Inventory inventory;
    public Transform offHand;
    public Transform mainHand;

    void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;

        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = Unequip(slotIndex);
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
        SetEquipmentBlendShapes(newItem, 100);

        currentEquipment[slotIndex] = newItem;


        SkinnedMeshRenderer newSkinnedMesh;
        // MAYBE REPLACE WITH A BLANK PLACEHOLDER

        //unless i do some serious debugging, only regular meshes work for the new items
        //also it would be better to use them because they have colliders
        //I THINK, JUST THIS AND EQUIPMENT NEED TO BE CHANGED
        if (newItem.equipSlot == EquipmentSlot.MainHand)
        {
            newSkinnedMesh = newItem.skinnedMesh;
            GameObject newMesh = Instantiate<GameObject>
            (newItem.gameObject, mainHand.transform);
            newMesh.transform.localPosition = new Vector3(0.00000f, -0.00074f, -0.00029f);
            newMesh.transform.Rotate(0,0,-90);
        }
        else if (newItem.equipSlot == EquipmentSlot.OffHand)
        {
            newSkinnedMesh = newItem.skinnedMesh;
            GameObject newMesh = Instantiate<GameObject>
            (newItem.gameObject, offHand.transform);
            newMesh.transform.localPosition = new Vector3(0.00039f, -0.00158f, -0.00072f);
            newMesh.transform.Rotate(138.299f,-177.891f,-456.76f);
        }
        else
        {
            newSkinnedMesh = Instantiate<SkinnedMeshRenderer>(newItem.skinnedMesh);
            newSkinnedMesh.transform.parent = targetMesh.transform;
            newSkinnedMesh.bones = targetMesh.bones;
            newSkinnedMesh.rootBone = targetMesh.rootBone;
        }
        currentMeshes[slotIndex] = newSkinnedMesh;
    }

    public Equipment Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }
            Equipment oldItem = currentEquipment[slotIndex];
            SetEquipmentBlendShapes(oldItem, 0);
            inventory.Add(oldItem);
            currentEquipment[slotIndex] = null;
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
            return oldItem;
        }
        return null;

    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
