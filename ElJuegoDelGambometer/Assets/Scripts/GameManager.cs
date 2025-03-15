using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  // Prevent duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);  // Keep across scenes

        // Initialize other singletons here
        DialogueManager.Initialize();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
