using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelSelect : MonoBehaviour
{
    
    public Image button1Image;
    public Image button2Image;
    public Image button3Image;
    public Button button1;
    public Button button2;
    public Button button3;

    public Sprite scrapBotSprite;
    public Sprite goldenBotSprite;
    public Sprite redGoldBotSprite;
    public Sprite bossBotSprite;

    private int currentLevel;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            currentLevel = GameManager.Instance.currentLevel;
            UpdateLevelSelection();
        }
        else
        {
            Debug.LogError("GameManager instance not found!");
        }
    }

    private void UpdateLevelSelection()
    {
        Sprite enemy1, enemy2, enemy3;

        if (currentLevel <= 2) // Rounds 1-2: All Scrap Bots
        {
            enemy1 = scrapBotSprite;
            enemy2 = scrapBotSprite;
            enemy3 = scrapBotSprite;
        }
        else if (currentLevel >= 3 && currentLevel <= 5) // Rounds 3-5: One Golden Bot, Two Scrap
        {
            enemy1 = scrapBotSprite;
            enemy2 = scrapBotSprite;
            enemy3 = goldenBotSprite;
        }
        else if (currentLevel >= 6 && currentLevel <= 11) // Rounds 6-11: Scrap, Gold, Red-Gold
        {
            enemy1 = scrapBotSprite;
            enemy2 = goldenBotSprite;
            enemy3 = redGoldBotSprite;
        }
        else if (currentLevel >= 12 && currentLevel <= 16) // Rounds 12-16: Boss, Gold, Red-Gold
        {
            enemy1 = bossBotSprite;
            enemy2 = goldenBotSprite;
            enemy3 = redGoldBotSprite;
        }
        else
        {
            Debug.LogError("Invalid level!");
            return;
        }

        // Set images
        button1Image.sprite = enemy1;
        button2Image.sprite = enemy2;
        button3Image.sprite = enemy3;

        // Assign button actions
        button1.onClick.AddListener(() => SelectNextLevel("BattleScene"));
        button2.onClick.AddListener(() => SelectNextLevel("BattleScene"));
        button3.onClick.AddListener(() => SelectNextLevel("BattleScene"));
    }

    void SelectNextLevel(string sceneName)
    {
        GameManager.Instance.currentLevel++;
        GameManager.Instance.LoadScene(sceneName);
    }


}
