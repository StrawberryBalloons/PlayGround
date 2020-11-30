using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcane : MonoBehaviour
{
    /*
    *
    * THIS IS THE SUPER CLASS FOR ALL ELEMENTS
    * remove start and update, put them into individual element scripts
    */
    public int manaBuffer = 0;
    

    public void start(){
        //go through all the initilisers
        //e.g material
    }
    public void update(){
        //go through all the things to keep updates
        //e.g animation
    }
    //START MISC
    public void material(){

    }
    //UPDATE MISC
    public void lookAtTarget(){
        //gets xyz of target and rotates to look at it
    }

    public void animation(){

    }
    public void angleOfHit(){

    }
    //EXTERNAL INFLUENCES
    public void inertia(){

    }

    public void manaGravity(){

    }
    //EFFECTS 
    public void splash(){
        //default, fizzle on hit and apply element effect
    }
    public void multiply(){
        
    }
    public void spread(){
        
    }
    public void explode(){
        
    }
    public void push(){
        
    }
    public void grow(){

    }
    //WHEN EFFECTS TAKE PLACE
    public void onCast(){
        
    }
    public void onHit(){
        
    }
    public void onEnterArea(){
        
    }
    public void onDistanceTravelled(){
        
    }

}
