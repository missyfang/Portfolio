using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningDisplay : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject clone;
    [SerializeField]
    GameObject WinningDisplayPanel;
    [SerializeField]
    private SpriteRenderer Fade;
    private bool HasWon = false;


    // Start is called before the first frame update
    void Start()
    {
        WinningDisplayPanel.SetActive(false);
        HasWon = false;
    }

    // Display winning screen
    void Update()
    {
        if (player.GetComponent<CollectTriForce>().HasTriForce && clone.GetComponent<CollectTriForce>().HasTriForce
            && !HasWon)
        {
            HasWon = true;

            WinningDisplayPanel.SetActive(true);

            AudioManager.PlayAudio("clearSfx");

            StartCoroutine(FadeToBlack());
        }

        if (Input.GetKey(KeyCode.Alpha3) && HasWon)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

    }

    IEnumerator FadeToBlack()
    {
        yield return new WaitForSeconds(2f);

        // Fade to black.
        while (Fade.color.a < 1)
        {
            Fade.color = new Color(Fade.color.r, Fade.color.b, Fade.color.g, Fade.color.a + 0.1f);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
