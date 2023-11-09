using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public static bool godMode;
    Inventory inventory;
   
    // Start is called before the first frame update
    void Start()
    {
        godMode = false;

        inventory = GetComponent<Inventory>();
        if (inventory == null)
            Debug.LogWarning("WARNING: Gameobject collector has no inventory to store things in.");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !godMode){
            godMode = true;
            inventory.AddRupees(255);
            inventory.AddKeys(255);
            inventory.AddBombs(255);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && godMode)
            godMode = false;
    }
}
