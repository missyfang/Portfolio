using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsReturningWeapon : MonoBehaviour
{

    private GameObject player;
    [SerializeField]
    private float range = 0.0f;
    private Vector3 originalPos;
    [SerializeField]
    private float stunTime = 0.0f;
    private float currentTime = 0f;

    void Start()
    {
        originalPos = transform.position;
        player = GameObject.FindGameObjectWithTag("player");

    }
    void Update()
    {
        currentTime += Time.deltaTime;

        if (isReturning)
        {
            gameObject.GetComponent<Rigidbody>().velocity =
                (player.transform.position - gameObject.transform.position).normalized * gameObject.GetComponent<Weapon>().speed;
        }

        if (!isReturning && Vector3.Distance(originalPos, transform.position) >= range)
        {
            isReturning = true;
        }

        if (currentTime >= 0.175f)
        {
            currentTime = 0f;
            AudioManager.PlayAudio("brangSfx");
        }
    }

    public bool isReturning;
    private void OnTriggerEnter(Collider other)
    {
        GameObject player = GameObject.FindGameObjectWithTag("player");

        if (other.tag == "enemy" && stunTime > 0 && other.GetComponent<AquaMovement>() == null)
        {
            if (other.GetComponent<KeeseMovement>() != null || other.GetComponent<GelMovement>() != null)
            {
                Destroy(other.gameObject);
            }

            player.GetComponent<CanStunEnemies>().StartCoroutine(player.GetComponent<CanStunEnemies>().StunEnemy(stunTime, other));
        }

        if (!isReturning && (other.tag == "wall" || other.tag == "enemy"))
        {
            isReturning = true;

            gameObject.GetComponent<Rigidbody>().velocity =
                (player.transform.position - gameObject.transform.position).normalized * gameObject.GetComponent<Weapon>().speed;
        }

        if (isReturning && other.tag == "player")
        {
            other.GetComponent<Inventory>().AddBrang(1);

            Destroy(gameObject);
        }
    }
}
