using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    // Created using "How to make a Dialogue System in Unity" by Brackeys https://www.youtube.com/watch?v=_nRzoTzeyxU&ab_channel=Brackeys

    // Turn this script into a singleton
    public static DialogueManager Instance { get; private set; }

    private Button textContinueButton;
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI dialogueText;

    private Queue<string> sentences;

    // Singleton stuff, from chatGPT to help me understand <3
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  // Destroy duplicate instances
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Keeps it across scenes
    }
    public static void Initialize()
    {
        if (Instance == null)
        {
            GameObject obj = new GameObject("DialogueManager");
            Instance = obj.AddComponent<DialogueManager>();
            DontDestroyOnLoad(obj);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();

        // Find the dialogue box objects dynamically (for use in different scenes)
        // NOTE: THESE NAMES MUST BE UNIQUE IN THE SCENE!
        textContinueButton = GameObject.Find("ContinueButton")?.GetComponent<Button>();
        nameText = GameObject.Find("NameText")?.GetComponent<TextMeshProUGUI>();
        dialogueText = GameObject.Find("DialogueText")?.GetComponent<TextMeshProUGUI>();
        Debug.Log("GAME OBJECTS FOUND");

        if (textContinueButton != null)
        {
            textContinueButton.onClick.AddListener(DisplayNextSentence);
        }
        else
        {
            Debug.LogWarning("Continue Button not found in the scene.");
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();  // Stop existing coroutine if one is currently happening
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void EndDialogue()
    {
        Debug.Log("End of conversation");
    }
}
