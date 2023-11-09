using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneHasHealth : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void AlterPlayerHealth(int changeToHealth)
    {
        player.GetComponent<HasHealth>().AlterHealth(changeToHealth);
      
    }
}
