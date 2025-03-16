using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.StartDialogue(dialogue);
        }
        else
        {
            Debug.LogError("DialogueManager instance not found!");
        }
    }
}
