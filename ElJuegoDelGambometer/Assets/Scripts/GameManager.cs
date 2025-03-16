using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int currentLevel = 1;
    public DiceRoller diceRoller;
    public HorizontalCardHolder horizontalCardHolder;
    private float maxPlayerHealth = 2000f;
    private float playerHealth = 2000f;
    private float maxEnemyHealth = 3500f;
    private float enemyHealth = 3500f;
    private float maxPlayerShield = 200f;
    private float playerShield = 200f;
    private float maxEnemyShield = 1000f;
    private float enemyShield = 1000f;
    private float gambometer = 0f;

    public Slider playerHealthSlider;
    public Slider enemyHealthSlider;
    public Slider playerShieldSlider;
    public Slider enemyShieldSlider;
    public Slider gambometerSlider;

    private void Awake()
    {
        UpdateSliders();
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

    public void PlayTurn()
    {
        if (horizontalCardHolder.selectedCards.Count == 0)
        {
            Debug.Log("No cards selected");
            return;
        }
        diceRoller.StartRoll();
    }

    public void PlayCards(int rolledNumber)
    {
        bool even = rolledNumber % 2 == 0;
        bool gambomified = gambometer >= 40f;

        float damage = 0f;
        float healing = 0f;
        float shield = 0f;
        float mult = 1f;

        bool ignoreShield = false;

        foreach (Card card in horizontalCardHolder.selectedCards)
        {
            CardObject cardObject = card.GetComponent<CardObject>();

            if (even)
            {
                gambometer += rolledNumber;
                switch (cardObject.cardType)
                {
                    case 1:
                        damage += cardObject.positive;
                        break;
                    case 2:
                        mult *= cardObject.positive;
                        break;
                    case 3:
                        shield += cardObject.positive;
                        break;
                    case 4:
                        healing += cardObject.positive * maxPlayerHealth;
                        break;
                    case 5:
                        mult *= cardObject.positive;
                        break;
                    case 6:
                        ignoreShield = true;
                        break;
                }
                Debug.Log(ignoreShield);

                if (gambomified) mult *= 10f;

                if (ignoreShield) enemyHealth -= damage * mult;
                else
                {
                    if (enemyShield > 0)
                    {
                        if (enemyShield >= damage * mult)
                        {
                            enemyShield -= damage * mult;
                        }
                        else
                        {
                            enemyHealth -= damage * mult - enemyShield;
                            enemyShield = 0;
                        }
                    }
                    else enemyHealth -= damage * mult;
                }
                playerHealth += healing * mult;
                playerShield += shield * mult;
            }
            else // odd
            {
                gambometer -= rolledNumber;
                switch (cardObject.cardType)
                {
                    case 1:
                        damage += cardObject.negative;
                        break;
                    case 2:
                        mult *= cardObject.negative;
                        break;
                    case 3:
                        damage += cardObject.negative;
                        break;
                    case 4:
                        damage += cardObject.negative;
                        break;
                    case 5:
                        mult *= cardObject.negative;
                        break;
                    case 6:
                        damage += cardObject.negative;
                        break;
                }

                if (gambomified) mult *= 10f;

                if (ignoreShield) playerHealth += damage * mult;
                else
                {
                    if (playerShield > 0)
                    {
                        if (playerShield >= damage * mult)
                        {
                            playerShield += damage * mult;
                        }
                        else
                        {
                            playerHealth += damage * mult - playerShield;
                            playerShield = 0;
                        }
                    }
                    else playerHealth += damage * mult;
                }
                playerHealth += healing * mult;
                playerShield += shield * mult;
            }
        }

        if (playerHealth > maxPlayerHealth) playerHealth = maxPlayerHealth;
        if (playerShield > maxPlayerShield) playerShield = maxPlayerShield;
        if (enemyHealth > maxEnemyHealth) enemyHealth = maxEnemyHealth;
        if (enemyShield > maxEnemyShield) enemyShield = maxEnemyShield;
        if (gambometer < 0f) gambometer = 0f;

        if (gambomified) gambometer = 0f;
        UpdateSliders();

        horizontalCardHolder.ClearSelectedCards();
    }

    public void UpdateSliders()
    {
        playerHealthSlider.value = playerHealth;
        enemyHealthSlider.value = enemyHealth;
        playerShieldSlider.value = playerShield;
        enemyShieldSlider.value = enemyShield;
        gambometerSlider.value = gambometer;
    }
}
