using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEventFinish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.ToLower() == "player")
        {
            Debug.Log(string.Format("{0} has won the level!", other.gameObject.name));
            LevelManager.Instance.SetState(LevelManager.State_LevelManager.Completed);
        }
    }
}
