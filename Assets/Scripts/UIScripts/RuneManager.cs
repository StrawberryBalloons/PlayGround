using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RuneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform PanelStates; //Will assign our panel to this variable so we can enable/disable it
    public InputField inputSequence;
    public string sequence;
    public GameObject player;
    List<string> listOfStrings;
    void Start()
    {
        panelOff(); //make sure our pause menu is disabled when scene starts
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
            openRune();
        else if (Input.GetKeyDown(KeyCode.RightShift))
            closeRune();
    }
    public void openRune()
    {
        Cursor.lockState = CursorLockMode.None;
        setPanel(PanelStates.gameObject, true);
    }
    public void closeRune()
    {
        Cursor.lockState = CursorLockMode.Locked;
        setPanel(PanelStates.gameObject, false);
    }
    public void panelOff()
    {
        setPanel(PanelStates.gameObject, false);
    }
    public void setPanel(GameObject obj, bool boo)
    {
        obj.SetActive(boo);
    }
    //GENERIC SPELL STARTUP
    public void addGenericShiftingScripts(GameObject spell)
    {
        spell.AddComponent<Rigidbody>();
    }
    public void addGenericStaticScripts(GameObject spell)
    {

    }

    public void onSequencePressed()
    {
        sequence = inputSequence.text;
        GameObject spell = new GameObject();
        if (validateSequence(spell))
        {
            //close sequence ui otherwise spell failure and close
        }
    }

    public bool validateSequence(GameObject spell)
    {
        sequence = sequence.Replace(" ", ""); //remove spaces
        sequence = sequence.ToLower(); //convert to uppercase
        listOfStrings = new List<string>();
        /*Conditions
        * Has an E and an S and a C for every F or has a letter for each one
        */
        for (int i = 0; i < sequence.Length; i++)
        {
            spellComponents(i);
        }
        for(int i = 0; i < listOfStrings.Count -1; i++){
            spellSequence(spell, listOfStrings[i]);
        }

        return false;
    }

    public void spellSequence(GameObject spell, string block){
        if (block[0] == 'E')
        {//add element script
        //script index = block[1] -> block.length -1
        
        addElementScript(spell, int.Parse(block.Substring(1)));
        }
        else if (block[0] == 'S')
        {//add element script
        //script index = block[1] -> block.length -1
        addShape(spell, int.Parse(block.Substring(1)));
        }
        else if (block[0] == 'M')
        {//add element script
        //script index = block[1], script values = block[2] -> block.Length -1
        string temp = block.Substring(1,4);
        addMisc(spell, int.Parse(temp), block);
        }
        else if (block[0] == 'F')
        {

        }
        else if (block[0] == 'C')
        {
            
        }
        else
        {
            
        }
    }
    public void spellComponents(int i)
    {
        if (sequence[i] == 'P' || sequence[i] == 'T')
        {
            //if it is projectile or static
        }
        else if (sequence[i] == 'E')
        {
            listOfStrings.Add("E");
        }
        else if (sequence[i] == 'S')
        {
            listOfStrings.Add("S");
        }
        else if (sequence[i] == 'M')
        {
            listOfStrings.Add("M");
        }
        else if (sequence[i] == 'F')
        {
            listOfStrings.Add("F");
        }
        else if (sequence[i] == 'C')
        {
            listOfStrings.Add("C");
        }
        else
        {
            string temp = listOfStrings[listOfStrings.Count - 1];
            listOfStrings.RemoveAt(listOfStrings.Count - 1);
            temp = temp + sequence[i];
            listOfStrings.Add(temp);
            //add sequence[i] to latest entry (will have number and letters)
        }
    }
    /* One # is a single selection, two is a selection for a selection
    * all indexing numbers are in ## format after letter
    * Projectile Spell = P (to start), Stationary spell = T (to start)
    * Element = E#
    * Shape = S#
    * Misc = M##
    *   M (colour) = M1#
    *   M (Dimension) = M2#
    *   M (Speed) = M3#
    *   M (Space) = M4#
    * Effect = F##
    *   F 1-22 
    *   FF11 -> can outline a new spell starting with T
    * Cause = C##
    *   C 1-7
    *   C 2/3 #
    */





    //SPELL ESSENTIALS
    public void addElementScript(GameObject spell, int select)
    {

    }
    public void addShape(GameObject spell, int select)
    {
        spell.AddComponent<Shape>();
        spell.AddComponent<MeshFilter>();
        spell.AddComponent<MeshRenderer>();
        spell.GetComponent<Shape>().addShape(select);
    }



    //SPELL MISC FUNCTIONS
    public void addMisc(GameObject spell, int index, string block){
        if(index == 0){
            setColour(spell, int.Parse(block.Substring(4,7)), int.Parse(block.Substring(7, 10)),
            int.Parse(block.Substring(10)));
            //M 001 256 256 256
            //0 123 456 789 101112
        } else if(index == 1){
            setDimensions(spell, int.Parse(block.Substring(4,7)),
            int.Parse(block.Substring(7, 10)), int.Parse(block.Substring(10)));
        }
        else if (index == 2){
            setSpeed(spell, int.Parse(block.Substring(4)));
        }else if (index == 3){
            assignToSpace(spell, int.Parse(block.Substring(4)));
        }else if (index == 4){
            setDirection(spell, int.Parse(block.Substring(4)));
        }else {
            Debug.Log("OOPS addMisc: " + block);
        }
    }
    public void assignToSpace(GameObject spell, int select)//default world
    {
        if (select == 1)
        {
            spell.transform.parent = player.transform;
            //added in this part here, not sure if i need it or not
            spell.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        }
        else
        {
            spell.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        }
    }
    public void setSpeed(GameObject spell, float select) //default max
    {
        spell.AddComponent<Speed>();
        //spell.GetComponent<Speed>().setSpeed(select * player.GetComponent<PlayerController>().mana);
    }
    public void setDirection(GameObject spell, int select)//default line
    {
        spell.AddComponent<Direction>();
        spell.GetComponent<Direction>().setSelect(select);
        spell.GetComponent<Direction>().getPlayer(player);
    }
    public void setColour(GameObject spell, int r, int g, int b)
    {

    }
    public void setDimensions(GameObject spell, int x, int y, int z){
        
    }

    //SPELL CAUSE AND EFFECT

    public void setCauseAndEffect(GameObject Spell, int effect, int cause){
        //set element cause and effect scripts active
    }
}