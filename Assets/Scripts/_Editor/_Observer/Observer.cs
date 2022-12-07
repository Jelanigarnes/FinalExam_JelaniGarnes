using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Wants to know when another object does something and wants to initiate an action 
public abstract class Observer
{
    public abstract void OnNotify();
}

public class Pallet : Observer
{
    //The box gameobject which will do something
    GameObject palletObj;
    //What will happen when this box gets an event
    PalletEditorEvent palletEvent;

    public Pallet(GameObject palletObj, PalletEditorEvent palletEvent)
    {
        this.palletObj = palletObj;
        this.palletEvent = palletEvent;
    }

    //What the box will do if the event fits it (will always fit but you will probably change that on your own)
    public override void OnNotify()
    {
        PalletColor(palletEvent.PalletEditorColor());
    }

    //The box will always jump in this case
    void PalletColor(Color mat)
    {
        //If the box is close to the ground
        if (palletObj) { palletObj.GetComponent<Renderer>().materials[0].color = mat; }

    }
}
