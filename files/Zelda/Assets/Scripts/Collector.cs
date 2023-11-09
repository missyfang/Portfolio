using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    Inventory inventory;
    [SerializeField]
    private Sprite CollectionSprite;

    // Clone game Object 
    [SerializeField]
    private GameObject Clone = null;



    // Try and grab a ref to the Inventory component on this game object
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("player").GetComponent<Inventory>();
        if (inventory == null)
            Debug.LogWarning("WARNING: Gameobject collector has no inventory to store things in.");
    }
    private void OnTriggerEnter(Collider coll)
    {
        Debug.Log("Searching For Item");

        GameObject object_collided_with = coll.gameObject;

        if (object_collided_with.tag == "rupee")
        {
            // Check to see if game object inventory exists bf adding a rupee to it.
            if (inventory != null)
                inventory.AddRupees(1);
           
            Destroy(object_collided_with);

            // Play sound effect
            AudioManager.PlayAudio("RupeeSfx");
        }

        if (object_collided_with.tag == "heart")
        {
            // Check to see if game object inventory exists bf adding a rupee to it.
            if (inventory.GetComponent<HasHealth>() != null)
                inventory.GetComponent<HasHealth>().AlterHealth(2);

            Destroy(object_collided_with);

            // Play sound effect
            AudioManager.PlayAudio("HeartSfx");
        }

        if (object_collided_with.tag == "bomb")
        {
            // Check to see if game object inventory exists bf adding a bomb to it.
            if (inventory != null)
                inventory.AddBombs(1);

            Destroy(object_collided_with);

            // Play sound effect
            AudioManager.PlayAudio("ItemSfx");
        }

        if (object_collided_with.tag == "key")
        {
            // Check to see if game object inventory exists bf adding a key to it.
            if (inventory != null)
                inventory.AddKeys(1);

            Destroy(object_collided_with);

            // Play sound effect
            AudioManager.PlayAudio("ItemSfx");
        }

        if (object_collided_with.tag == "bow" && gameObject.GetComponent<Weapon>() == null)
        {
            GameObject Arrow = Resources.Load("Arrow") as GameObject;
            inventory.addAltWeapon(Arrow.GetComponent<Weapon>());
            StartCoroutine(CollectAnim(object_collided_with));
        }

        if (object_collided_with.tag == "brang" && gameObject.GetComponent<Weapon>() == null)
        {
            GameObject Brang = Resources.Load("Boomerang") as GameObject;
            inventory.addAltWeapon(Brang.GetComponent<Weapon>());
            Destroy(object_collided_with);

            // Play sound effect
            AudioManager.PlayAudio("ItemSfx");
        }

        if (object_collided_with.tag == "powerUp")
        {
            StartCoroutine(PowerUpAnim(object_collided_with));
        }

    }
    
    IEnumerator CollectAnim(GameObject item)
    {
        Debug.Log(item);

        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("enemy");

        // Disable all movement.
        for (int i = 0; i < Enemies.Length; i++)
        {
            if (Enemies[i].GetComponent<Movement>() != null)
            {
                Enemies[i].GetComponent<Movement>().enabled = false;
            }
        }

        ArrowKeyMovement.playerControl = false;

        // Set collection sprite.
        gameObject.GetComponent<Animator>().enabled = false;
        Sprite normalSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = CollectionSprite;

        // Relocate collected item.
        item.tag = "Untagged";
        item.transform.position = new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y + 1f, gameObject.transform.position.z);
        AudioManager.PlayAudio("ItemPickupSfx");

        yield return new WaitForSeconds(1.8f);

        // Enable all movement.
        for (int i = 0; i < Enemies.Length; i++)
        {
            if (Enemies[i].GetComponent<Movement>() != null)
            {
                Enemies[i].GetComponent<Movement>().enabled = true;
            }
        }
        ArrowKeyMovement.playerControl = true;

        // Reset sprite.
        gameObject.GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;


        // Destroy collectable.
        Destroy(item);

    }


    IEnumerator PowerUpAnim(GameObject item)
    {
       // disable control
        ArrowKeyMovement.playerControl = false;

        // Set collection sprite.
        gameObject.GetComponent<Animator>().enabled = false;
        Sprite normalSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        gameObject.GetComponent<SpriteRenderer>().sprite = CollectionSprite;

        // Relocate collected item.
        item.tag = "Untagged";
        item.transform.position = new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y + 1f, gameObject.transform.position.z);
        AudioManager.PlayAudio("ItemPickupSfx");

        yield return new WaitForSeconds(1.8f);

     
        ArrowKeyMovement.playerControl = true;

        // Reset sprite.
        gameObject.GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;


        // Destroy collectable.
        Destroy(item);

        // add sound effect for while moving apart.

        float time = 0;
        float duration = 3;

        AudioManager.PlayAudio("CloneSfx");

        // Enable clone
        Clone.transform.position = transform.position;
        Clone.SetActive(true);

        Vector3 playerStartPostion = transform.position;
        Vector3 playerTargetPostion = transform.position + Vector3.right;
        Vector3 cloneTargetPostion = transform.position + Vector3.left;

        // Lerp player  to the right and the clone to the left.
        while (time < duration)
        {
            transform.position = Vector3.Lerp(playerStartPostion, playerTargetPostion, time / duration);
            Clone.transform.position = Vector3.Lerp(playerStartPostion, cloneTargetPostion, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = playerTargetPostion;
        Clone.transform.position = cloneTargetPostion;

    }
}
