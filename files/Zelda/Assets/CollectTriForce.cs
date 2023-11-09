using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTriForce : MonoBehaviour
{
    public bool HasTriForce = false;

    [SerializeField]
    private Sprite CollectionSprite;

    private void OnTriggerEnter(Collider coll)
    {
       
        GameObject object_collided_with = coll.gameObject;

        if (object_collided_with.tag == "TriForce")
        {
            StartCoroutine(FinaleAnim(object_collided_with));
        }

    }


    IEnumerator FinaleAnim(GameObject item)
    {
        // disable control
        ArrowKeyMovement.playerControl = false;

        // Set collection sprite.
        gameObject.GetComponent<Animator>().enabled = false;
        Sprite normalSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        gameObject.GetComponent<SpriteRenderer>().sprite = CollectionSprite;

        // Relocate collected item.
        item.tag = "Untagged";
        item.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z);

        item.GetComponent<SpriteRenderer>().sortingLayerName = "foreGround";
        item.GetComponent<SpriteRenderer>().sortingOrder = 6;

        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "foreGround";
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 6;


        yield return new WaitForSeconds(0.1f);

        //// Reset sprite.
        //gameObject.GetComponent<Animator>().enabled = true;
        //gameObject.GetComponent<SpriteRenderer>().flipX = false;
        //gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;


        //// Destroy collectable.
        //Destroy(item);

        HasTriForce = true;
    }
}
