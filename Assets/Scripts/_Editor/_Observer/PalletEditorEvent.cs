using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PalletEditorEvent
{
    public abstract Color PalletEditorColor();
}


public class WhiteMat : PalletEditorEvent
{
    public override Color PalletEditorColor()
    {
        return Color.white;
    }
}