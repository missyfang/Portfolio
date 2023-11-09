using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsLoot
{
    public static void DropLoot(Transform dropLocation)
    {
        bool willDropLoot = Random.Range(1, 100) > 50;

        if (willDropLoot)
        {
            int lootNum = Random.Range(0, 2);
            string lootTag;

            if (lootNum == 0)
            {
                lootTag = "rupee";
            }

            else if (lootNum == 1)
            {
                lootTag = "heart";
            }

            else
            {
                lootTag = "bomb";
            }

            GameObject loot = GameObject.Instantiate(Resources.Load(lootTag) as GameObject, dropLocation.position, Quaternion.identity);
            GameObject.Destroy(loot, 9.0f);
        }

    }

    public static void DropKey(Transform dropLocation)
    {
        GameObject.Instantiate(Resources.Load("key") as GameObject, dropLocation.position, Quaternion.identity);
    }

    public static void DropBoomerang(Transform dropLocation)
    {
        GameObject.Instantiate(Resources.Load("brang") as GameObject, dropLocation.position, Quaternion.identity);
    }
}
