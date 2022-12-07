using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Collectable : MonoBehaviour
{
    public int score;
    static List<Transform> items;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {

            

        }
        Destroy(gameObject);
        
    }

    //public static void PlaceItem(Transform item)
    //{
    //    Transform newItem = item;
    //    if (items == null)
    //    {
    //        items = new List<Transform>();
    //    }
    //    items.Add(newItem);
    //}
}
