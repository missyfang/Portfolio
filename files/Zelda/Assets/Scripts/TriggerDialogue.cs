using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TriggerDialogue : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI DialogueBox;
    [SerializeField]
    private string dialogue;
    [SerializeField]
    private float dialogueSpeed;
    private bool isInRoom = false;

    void Update()
    {
        if (!IsInCameraView.Instance.IsTargetVisible(gameObject))
        {
            DialogueBox.text = "";
            isInRoom = false;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Detected Dialogue");

        if (other.tag == "player" && !isInRoom)
        {
            isInRoom = true;
            ArrowKeyMovement.playerControl = false;
            StartCoroutine(PlayDialogue());

        }
    }

    IEnumerator PlayDialogue()
    {
        for (int i = 0; i < dialogue.Length; ++i)
        {
            DialogueBox.text += dialogue[i];
            AudioManager.PlayAudio("dialogueSfx");
            yield return new WaitForSeconds(dialogueSpeed);
        }

        ArrowKeyMovement.playerControl = true;
    }
}
