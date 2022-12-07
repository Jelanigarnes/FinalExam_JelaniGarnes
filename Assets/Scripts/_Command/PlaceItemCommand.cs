using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItemCommand : ICommand
{
    Vector3 position;
    Transform item;

    public PlaceItemCommand(Vector3 position, Transform item)
    {
        this.position = position;
        this.item = item;
    }

    public void Execute()
    {
        ItemPlace.PlaceItem(item);
    }

    public void Undo()
    {
        ItemPlace.RemoveItem(position);
        //Collectable.PlaceItem(item);
    }

}
