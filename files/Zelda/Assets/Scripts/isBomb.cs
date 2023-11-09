using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isBomb : MonoBehaviour
{
    public GameObject explosion;
    private GameObject explosionHolder;

    private void OnEnable()
    {
        Debug.Log(gameObject.transform.position);
        StartCoroutine(ExplodeBomb());
    }

    IEnumerator ExplodeBomb()
    {
        yield return new WaitForSeconds(1.0f);

        AudioManager.PlayAudio("bombSfx");

        Vector3 pos = gameObject.transform.position;
        pos.y += 2.35f;

        explosionHolder = Instantiate(explosion, pos, gameObject.transform.rotation);

        var ToDamage = FindObjectsOfType<HasHealth>();

        for (int i = 0; i < ToDamage.Length; i++)
        {
            if (Vector3.Distance(ToDamage[i].transform.position, gameObject.transform.position) <= 1.5f)
            {
                ToDamage[i].AlterHealth(gameObject.GetComponent<Weapon>().damage);
            }
        }

        yield return new WaitForSeconds(0.3f);

        Destroy(gameObject);
    }
}
