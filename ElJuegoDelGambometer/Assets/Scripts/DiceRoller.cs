using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditorInternal;
using System.Data.Common;

public enum DiceState
{
    Ready,
    Rolling,
    Finishing,
    Finished,
}

public class DiceRoller : MonoBehaviour
{
    public TextMeshPro number;
    public float spinSpeed = 1000f;
    public DiceState state = DiceState.Ready;
    private float slowDownRate;
    private int rolledNumber;
    private float rollTime;
    private float changeNumberTimer;
    private float changeNumberInterval;
    
    private void Update()
    {
        if (state == DiceState.Rolling)
        {
            RotateDie();
        }
        else if (state == DiceState.Finishing)
        {
            StopRolling();
        }
        else  if (state == DiceState.Finished)
        {
            // Align the die to the nearest 90 degrees
            if (System.Math.Abs(this.gameObject.transform.eulerAngles.z / 5) < 1)
            {
                this.gameObject.transform.eulerAngles = new Vector3(0, 0, Mathf.Round(this.gameObject.transform.eulerAngles.z / 90) * 90);
            }

            // TODO: 
            // Send signal to return number and continue turn
            // Set state to ready again
        }
    }

    public void StartRoll()
    {
        if (state == DiceState.Ready)
        {
            state = DiceState.Rolling;
            spinSpeed = 1000f;
            rollTime = Random.Range(1.5f, 3.5f);
            changeNumberTimer = 0f;
            changeNumberInterval = 0.05f;
            slowDownRate = spinSpeed / rollTime;
        }
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
            StopRolling();
        }
    }

    private void StopRolling()
    {
        //isRolling = false;
        state = DiceState.Finishing;
        if (System.Math.Abs(this.gameObject.transform.eulerAngles.z / 5) < 1)
        {
            state = DiceState.Finished;
            spinSpeed = 0;
            Debug.Log(System.Math.Abs(this.gameObject.transform.eulerAngles.z / 5));
            Debug.Log("You rolled: " + rolledNumber);
        }
        else
        {
            transform.Rotate(0, 0, 300 * Time.deltaTime);
        }
    }
}
