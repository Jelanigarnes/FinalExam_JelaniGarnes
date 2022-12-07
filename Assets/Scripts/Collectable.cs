using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectable : MonoBehaviour
{
    public int score;
  
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
          
                //ScoreManager.instance.ChangeScore(score);          
                //Call method in characterstat
                //PlayerManager.instance.player.GetComponent<CharacterStats>().RecoverHealth(heal);

        }
        Destroy(gameObject);
        
    }
}
