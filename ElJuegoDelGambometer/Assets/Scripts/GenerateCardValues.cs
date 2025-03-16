using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardValues : MonoBehaviour
{
    public (int,int) GenFlatRed(){
        //Generate flat attack values and corresponding negative
        
        int attack_val = Random.Range(10,26);
        float coeff = (Random.Range(25,76)/100);
        int debuff_val = (int) Mathf.Ceil((coeff*attack_val));
        return (attack_val,-1*debuff_val);

    }
    public (float,float) GenMultRed(){
        //Generate attack mult values and corresponding neg mult
         
        float sigma = (float) (10-6.5)/3; //Standard deviation
        float coeff = (float) ((Random.value)*sigma + 6.5); //should generate a value between 3 and 10 following a bell curve, too tired to do a weighted bell curve
        float debuff_mult = 1/(11-coeff);
        return (coeff,debuff_mult);

    }

    public (int,int) GenShield(){
        //Generate a Shield Value
        
        int shield_val = Random.Range(265,420);
        float coeff  = Random.Range(0.25f,0.75f);
        int debuff_val = (int) Mathf.Ceil((coeff*shield_val));
        return (shield_val,-1*debuff_val);
    }
    public (float,float) GenHeal(){
        
        float heal_val = Random.Range(0.05f,0.15f);
        float coeff  = Random.Range(0.25f,0.75f);
        int debuff_val = (int) Mathf.Ceil((coeff*heal_val));
        return (heal_val,-1*debuff_val);  
    }





}
