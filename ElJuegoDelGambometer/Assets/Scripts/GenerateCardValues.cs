using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardValues : MonoBehaviour
{
    public (int,int) GenFlatRed(){
        //Generate flat attack values and corresponding negative
        Random getrnd = new Random();
        int attack_val = getrnd.Next(10,26);
        float coeff = getrnd.Range(0.25f,0.75f);
        int debuff_val = (int) Mathf.Ceil((coeff*attack_val));
        return Tuple.Create(attack_val,-1*debuff_val);

    }
    public (float,float) GenMultRed(){
        //Generate attack mult values and corresponding neg mult
         
        float sigma = (10-6.5)/3; //Standard deviation
        float coeff = (Random.value)*sigma + 6.5; //should generate a value between 3 and 10 following a bell curve, too tired to do a weighted bell curve
        float debuff_mult = 1/(11-coeff);
        return Tuple.Create(coeff,debuff_mult);

    }

    public (int,int) GenShield(){
        //Generate a Shield Value
        Random getrnd = new Random();
        int shield_val = getrnd.Next(265,420);
        float coeff  = getrnd.Range(0.25f,0.75f);
        int debuff_val = (int) Mathf.Ceil((coeff*shield_val));
        return Tuple.Create(shield_val,-1*debuff_val);
    }
    public (float,float) GenHeal(){
        Random getrnd = new Random();
        float heal_val = getrnd.Next(0.05f,0.15f);
        float coeff  = getrnd.Range(0.25f,0.75f);
        int debuff_val = (int) Mathf.Ceil((coeff*heal_val));
        return Tuple.Create(heal_val,-1*debuff_val);  
    }





}
