using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth;
    public float currentHealth { get; private set; }
    public int str; //how hard you hit and jump height
    public int dex; //how fast you are 
    public int vit; // health modifier?
    public Stat damage;
    public Stat armourHead;
    public Stat armourTorso;
    public Stat armourLegs;
    public Stat armourFoot;

    public event System.Action<int, float> OnHealthChanges;
    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(100, -1);
        }
    }

    public Stat getArmour(int slot)
    {
        if (slot == 0)
        {
            return armourHead;
        }
        if (slot == 1)
        {
            return armourTorso;
        }
        if (slot == 2)
        {
            return armourLegs;
        }
        if (slot == 3)
        {
            return armourFoot;
        }
        return null;
    }

    public void TakeDamage(float damage, int armourSlot)
    {
        if (armourSlot >= 0)
        {
            damage -= getArmour(armourSlot).GetValue(); //armour.GetValue();
        }
        damage = Mathf.Clamp(damage, 0, int.MaxValue);


        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if(OnHealthChanges != null){
            OnHealthChanges(maxHealth, currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void TakeDamage(float damage, int armourSlot, float modifier)
    {
        damage = damage * modifier;
        if (armourSlot >= 0)
        {
            damage -= getArmour(armourSlot).GetValue(); //armour.GetValue();
        }
        damage = Mathf.Clamp(damage, 0, int.MaxValue);


        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int heal)
    {
        heal = Mathf.Clamp(heal, 0, int.MaxValue);
        currentHealth += heal;
        Debug.Log(transform.name + " Heals " + heal + " damage.");

    }
    public void Heal(float heal, float modifier)
    {
        heal = heal* modifier;
        heal = Mathf.Clamp(heal, maxHealth, int.MinValue);
        currentHealth += heal;
        Debug.Log(transform.name + " Heals " + heal + " damage.");

    }

    public virtual void Die()
    {
        //die
        //overide this
    }

}