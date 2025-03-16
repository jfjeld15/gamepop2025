using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceRoller : MonoBehaviour
{
    public TextMeshPro number;
    public float spinSpeed = 1000f;
    private bool isRolling = false;
    private float slowDownRate;
    private int rolledNumber;
    private float rollTime;
    private float changeNumberTimer;
    private float changeNumberInterval;
    private bool finishing;  // This is for aligning the die

    private void Update()
    {
        if (isRolling)
        {
            RotateDie();
        }
        if (finishing)
        {
            StopRolling();
        }
    }

    public void StartRoll()
    {
        isRolling = true;
        spinSpeed = 1000f;
        rollTime = Random.Range(1.5f, 3.5f);
        changeNumberTimer = 0f;
        changeNumberInterval = 0.05f;
        slowDownRate = spinSpeed / rollTime;
        Invoke("StopRolling", rollTime);  // Stop rolling in random number of seconds between 1.5 and 3 secs
    }

    private void RotateDie()
    {
        transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
        changeNumberTimer += Time.deltaTime;

        if (changeNumberTimer >= changeNumberInterval)
        {
            while (rolledNumber.ToString() == number.text)
            {
                // This rerolls if you get the same thing twice in a row so you dont get 6 then 6 then 6 then it looks like it just stays on 6
                rolledNumber = Random.Range(1, 7);
            }
            number.text = rolledNumber.ToString();
            changeNumberTimer = 0f;

            // Increase interval to slow down number changing as spin slows
            // Thanks chatGPT
            float t = 1 - Mathf.Pow(spinSpeed / 1000f, 2f);
            changeNumberInterval = Mathf.Lerp(0.05f, 0.5f, t);
        }

        spinSpeed -= slowDownRate * Time.deltaTime;
        if (spinSpeed < 0f)
        {
            spinSpeed = 0f;
        }
    }

    private void StopRolling()
    {
        isRolling = false;
        if (System.Math.Abs(this.gameObject.transform.eulerAngles.z / 5) < 1)
        {
            finishing = false;
            spinSpeed = 0;
            Debug.Log(System.Math.Abs(this.gameObject.transform.eulerAngles.z / 5));
            Debug.Log("You rolled: " + rolledNumber);
        }
        else
        {
            finishing = true;
            transform.Rotate(0, 0, 1000 * Time.deltaTime);
        }
    }
}
