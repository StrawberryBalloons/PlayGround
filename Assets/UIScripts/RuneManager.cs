using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RuneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform PanelStates; //Will assign our panel to this variable so we can enable/disable it
    public Transform PanelShape;
    public Transform PanelStatic;
    public Transform PanelShifting;
    public GameObject player;

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
        setPanel(PanelShape.gameObject, false);
        setPanel(PanelStatic.gameObject, false);
        setPanel(PanelShifting.gameObject, false);
    }
    public void setPanel(GameObject obj, bool boo)
    {
        obj.SetActive(boo);
    }

    public void shifting()
    {
        if (PanelShifting.gameObject.activeSelf == false)
        {
            panelOff();
            setPanel(PanelShifting.gameObject, true);
        }
    }
    public void stationary()
    {
        if (PanelShifting.gameObject.activeSelf == false)
        {
            panelOff();
            setPanel(PanelStatic.gameObject, true);
        }

    }
    public void Shape()
    {
        if (PanelShifting.gameObject.activeSelf == false)
        {
            panelOff();
            setPanel(PanelShape.gameObject, true);
        }

    }

    public void buttonShifting()
    {
        Dropdown shiftingElement = PanelShifting.transform.GetChild(0).GetComponent<Dropdown>();
        Dropdown shiftingShape = PanelShifting.transform.GetChild(1).GetComponent<Dropdown>();
        Slider shiftingSpeed = PanelShifting.transform.GetChild(2).GetComponent<Slider>();
        Dropdown shiftingDirection = PanelShifting.transform.GetChild(3).GetComponent<Dropdown>();
        GameObject shiftingSpell = new GameObject("shiftingSpell");
        addGenericShiftingScripts(shiftingSpell);
        assignToSpace(shiftingSpell, 0);//always world   
        addShape(shiftingSpell, shiftingShape.value);
        addElement(shiftingSpell, shiftingElement.value);
        addDirection(shiftingSpell, shiftingDirection.value);
        addSpeed(shiftingSpell, shiftingSpeed.value * player.GetComponent<PlayerController>().mana);//need to change from dropdown to value

        setPanel(PanelShifting.gameObject, false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void buttonStatic()
    {// where exactly? vector less than INT (if local)
        Dropdown staticElement = PanelStatic.transform.GetChild(0).GetComponent<Dropdown>();
        Dropdown staticShape = PanelStatic.transform.GetChild(1).GetComponent<Dropdown>();
        Dropdown staticRotation = PanelStatic.transform.GetChild(2).GetComponent<Dropdown>();
        Dropdown staticSpace = PanelStatic.transform.GetChild(3).GetComponent<Dropdown>();
        GameObject staticSpell = new GameObject("staticSpell");
        addElement(staticSpell, staticElement.value);
        assignToSpace(staticSpell, staticSpace.value);
        addShape(staticSpell, staticShape.value);
        addRotation(staticSpell, 0);
        addGenericStaticScripts(staticSpell);

        setPanel(PanelStatic.gameObject, false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void buttonShape()
    {
        Dropdown shapeElement = PanelShape.transform.GetChild(0).GetComponent<Dropdown>();
        Dropdown shapeTarget = PanelShape.transform.GetChild(1).GetComponent<Dropdown>();
        Dropdown shapeChange = PanelShape.transform.GetChild(2).GetComponent<Dropdown>();
        GameObject shapeSpell = new GameObject("shapeSpell");
        addElement(shapeSpell, shapeElement.value);
        addTarget(shapeSpell, shapeTarget.value);
        addChange(shapeSpell, shapeChange.value);
        addGenericShapeScripts(shapeSpell);

        setPanel(PanelShape.gameObject, false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void addGenericShapeScripts(GameObject spell)
    {

    }
    public void addGenericShiftingScripts(GameObject spell)
    {
        spell.AddComponent<Rigidbody>();
    }
    public void addGenericStaticScripts(GameObject spell)
    {

    }
    public void addElement(GameObject spell, int select)
    {
        spell.AddComponent<Element>();

        //TEMPORARY
        //Get the Renderer component from the new cube
        var render = spell.GetComponent<MeshRenderer>();

        //Call SetColor using the shader property name "_Color" and setting the color to red
        switch (select)
        {
            case 4:
                render.material.SetColor("_Color", Color.green);
                break;
            case 3:
                render.material.SetColor("_Color", Color.blue);
                break;
            case 2:
                render.material.SetColor("_Color", Color.yellow);
                break;
            case 1:
                render.material.SetColor("_Color", Color.red);
                break;
            default:
                print("Not an option Error");
                break;
        }
    }
    public void addShape(GameObject spell, int select)
    {
        spell.AddComponent<Shape>();
        spell.AddComponent<MeshFilter>();
        spell.AddComponent<MeshRenderer>();
        spell.GetComponent<Shape>().addShape(select);
    }
    public void assignToSpace(GameObject spell, int select)
    {
        if (select == 1)
        {
            spell.transform.parent = player.transform;
        }
        else
        {
            spell.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        }
    }
    public void addSpeed(GameObject spell, float select)
    {
        spell.AddComponent<Speed>();
        spell.GetComponent<Speed>().setSpeed(select);
    }
    public void addDirection(GameObject spell, int select)
    {
        spell.AddComponent<Direction>();
        spell.GetComponent<Direction>().setSelect(select);
        spell.GetComponent<Direction>().getPlayer(player);
    }
    public void addRotation(GameObject spell, int select)
    {

    }
    public void addTarget(GameObject spell, int select)
    {
        spell.AddComponent<Target>();
        switch (select)
        {
            case 4:
                break;
            case 3:
                break;
            case 2:
                break;
            case 1:
                break;
            default:
                print("Not an option Error");
                break;
        }
    }
    public void addChange(GameObject spell, int select)
    {
        spell.AddComponent<Change>();
        switch (select)
        {
            case 4:
                break;
            case 3:
                break;
            case 2:
                break;
            case 1:
                break;
            default:
                print("Not an option Error");
                break;
        }
    }

}