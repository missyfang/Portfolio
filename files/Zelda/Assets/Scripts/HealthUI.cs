using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthUI : MonoBehaviour
{
    public GameObject player;
    public Image[] Hearts;

    [SerializeField]
    private Material White;
    private bool IsResetting = false;

    // Start is called before the first frame update
    void Start()
    {
        int playerHealth = player.GetComponent<HasHealth>().numHealth;
    }

    // Update is called once per frame
    void Update()
    {
        int playerHealth = player.GetComponent<HasHealth>().numHealth;

        // Restart secene.
        if (playerHealth <= 0 && SceneManager.GetActiveScene().name != "GoldScene")
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else if (playerHealth <= 0 
            && FindObjectOfType<HasCheckpoints>() != null
            && !IsResetting)
        {
            IsResetting = true;
            Debug.Log("reset");
            FindObjectOfType<HasCheckpoints>().GoBackToCheckPoint();
        }

        if (playerHealth > 0)
        {
            IsResetting = false;
        }

        // Disable or enable heart according to player health.
        for (int i = 0; i < Hearts.Length; i++)
        {
            if (i < playerHealth)
            {
                Hearts[i].material = null;
                Hearts[i].color = Color.white;
            }
            else
            {
                Hearts[i].material = White;
                Hearts[i].color = new Color32(250, 216, 216, 255);
            }
        }
    }
}
