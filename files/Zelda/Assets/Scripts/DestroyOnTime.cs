using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    [SerializeField]
    private float destroyTime;
    private void OnEnable()
    {
        Destroy(gameObject, destroyTime);
        Invoke("TurnOnAnimator", 0.5f);
    }

    void TurnOnAnimator()
    {
        int i = 0;
        while (gameObject.transform.GetChild(i) != null)
        {
            gameObject.transform.GetChild(i).GetComponent<Animator>().enabled = true;
            ++i;
        }
    }
}
