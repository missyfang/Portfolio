using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimatorWhileMoving : MonoBehaviour
{
    [SerializeField]
    private GameObject SwordSpark;
    [SerializeField]
    private float ExplosionSpeed;
    [SerializeField]
    private float SpawnDistance;

    private bool isExploding = false;

    // Update is called once per frame
    void Update()
    {
        // Check if the sword is moving and hence a beam.
        // If so, make the beam sound and play its animator.

        if (gameObject.GetComponent<Rigidbody>().velocity != Vector3.zero
            && !gameObject.GetComponent<Animator>().enabled)
        {
            AudioManager.PlayAudio("beamSfx");
            gameObject.GetComponent<Animator>().enabled = true;
        }
    }

    private void OnDestroy()
    {
        // If this sword is a beam and it hits something that is a wall or enemy...
        // Blow up.

        if (gameObject.GetComponent<Rigidbody>().velocity != Vector3.zero
            && !isExploding)
        {
            isExploding = true;
            SwordExplosion();
        }
    }

    void SwordExplosion()
    {
        // Get x and y from sword.
        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y;

        // Spawn Location and Rotation for Sparks.
        Vector3 SparkSpawn;
        Quaternion SparkRotation;

        // Explosion Holder.
        GameObject[] Explosion = new GameObject[4];

        // Spawn 4 sparks each rotated to be each 45 degree angle from the sword starting with up right.

        // Up Right
        SparkSpawn = new Vector3(x + SpawnDistance, y + SpawnDistance, 0f);
        Explosion[0] = Instantiate(SwordSpark, SparkSpawn, Quaternion.identity);
        Explosion[0].GetComponent<Rigidbody>().velocity = new Vector3(ExplosionSpeed, ExplosionSpeed, 0f);

        // Down Right
        SparkSpawn = new Vector3(x + SpawnDistance, y - SpawnDistance, 0f);
        SparkRotation = new Quaternion(0f, 0f, -90f, 0f);
        Explosion[1] = Instantiate(SwordSpark, SparkSpawn, SparkRotation);
        Explosion[1].gameObject.GetComponent<SpriteRenderer>().flipX = true;
        Explosion[1].GetComponent<Rigidbody>().velocity = new Vector3(ExplosionSpeed, -ExplosionSpeed, 0f);

        // Down Left
        SparkSpawn = new Vector3(x - SpawnDistance, y - SpawnDistance, 0f);
        SparkRotation = new Quaternion(0f, 0f, -180f, 0f);
        Explosion[2] = Instantiate(SwordSpark, SparkSpawn, SparkRotation);
        Explosion[2].GetComponent<Rigidbody>().velocity = new Vector3(-ExplosionSpeed, -ExplosionSpeed, 0f);

        // Up Left
        SparkSpawn = new Vector3(x - SpawnDistance, y + SpawnDistance, 0f);
        SparkRotation = new Quaternion(0f, 0f, -270f, 0f);
        Explosion[3] = Instantiate(SwordSpark, SparkSpawn, SparkRotation);
        Explosion[3].gameObject.GetComponent<SpriteRenderer>().flipY = true;
        Explosion[3].GetComponent<Rigidbody>().velocity = new Vector3(-ExplosionSpeed, ExplosionSpeed, 0f);

        // Destroy the Sparks after a certain time.
        for (int i = 0; i < Explosion.Length; ++i)
        { 
            Destroy(Explosion[i].gameObject, 0.5f);
        }

    }


}
