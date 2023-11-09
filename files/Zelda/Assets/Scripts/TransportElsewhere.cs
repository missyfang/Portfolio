using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransportElsewhere : MonoBehaviour
{
    [SerializeField]
    private GameObject TravelPosition;
    [SerializeField]
    private GameObject CameraPosition;
    [SerializeField]
    private Image Fade;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Detecting 2D Bow Room Transfer");

        if (other.tag == "player")
        {
            ArrowKeyMovement.playerControl = false;
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            StartCoroutine(FadeToBlackAndTransition(other.gameObject));


        }
    }

    IEnumerator FadeToBlackAndTransition(GameObject Player)
    {
        // Fade to black.
        while (Fade.color.a < 1)
        {
            Fade.color = new Color(Fade.color.r, Fade.color.b, Fade.color.g, Fade.color.a + 0.1f);
            yield return new WaitForSeconds(0.2f);
        }

        // Move Camera and Player.
        Camera.main.transform.position = CameraPosition.transform.position;
        Player.transform.position = TravelPosition.transform.position;

        // Fade to white.
        while (Fade.color.a > 0)
        {
            Fade.color = new Color(Fade.color.r, Fade.color.b, Fade.color.g, Fade.color.a - 0.2f);
            yield return new WaitForSeconds(0.2f);
        }

        ArrowKeyMovement.playerControl = true;
        Player.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }


}
