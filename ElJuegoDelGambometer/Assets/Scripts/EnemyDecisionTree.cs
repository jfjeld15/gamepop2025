using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDecisionTree : MonoBehaviour
{
    /*
    *LEGEND:
    * 1 = Attack, 2 = Healing, 3 = Recharge, 4 = Buff Self (lasts 3 turns), 5 = Debuff Player (lasts 3 turns)
    *Need to account for if buff or debuff was triggered in the past 2 turns then the same effect cannot be triggered again
    *Would prefer it if we forced it to do 1-3 if buffed/debuffed in the past 3 turns
    *BUFFED = False, TURNS = 0, if (buffed && turns>2){buffed = false && turns = 0} else if(buffed){while(decision[0]>3) decision = Decision()
    * turns+=1}
    */
    public (int,int) ScrapDecision(int round,int MaxHealth, int Health){
        int damage = (int) (350+(Random.value)*25 + round*10);
        return (1,damage);
    }
    public (int,int) GoldDecision(int round,int MaxHealth, int Health){
        float sigma = (float) (700-350)/3;
        int coeff =  (int) Mathf.Ceil(((Random.value)*sigma + 350));
        int damage = (int) (Mathf.Clamp(coeff,250,700) +(Random.value)*25 + round*10);
        int healing = (int) (Random.Range(0.05f,0.15f)*MaxHealth);
        if (Health<((0.05)*MaxHealth))
        {
            return(2,healing);
        }
        else if (Health<((0.15)*MaxHealth))
        {
            float decision = Random.value;
            if (decision<0.25)
            {
                return(2,healing);
            }
            else
            {
                return (1,damage);
            }
        }
        else
        {
            return (1,damage);
        }
    }
     public (int,int) RedGoldDecision(int round,int MaxHealth, int Health){
        float sigma = (float) (700-350)/3;
        int coeff =  (int) Mathf.Ceil(((Random.value)*sigma + 350));
        int damage = (int) (Mathf.Clamp(coeff,250,700) +(Random.value)*25 + round*10);
        int healing = (int) (Random.Range(0.05f,0.15f)*MaxHealth);
        if (Health<((0.05)*MaxHealth))
        {
            return(2,healing);
        }
        else if (Health<((0.20)*MaxHealth))
        {
            float decision = Random.value;
            if (decision<0.45)
            {
                return(2,healing);
            }
            else
            {
                return (1,damage);
            }
        }
        else
        {
            return (1,damage);
        }
    }

     public (int,int) BossDecision(int round,int MaxHealth, int Health, int MaxShield, int Shield){
        float sigma = (float) (700-350)/3;
        int coeff =  (int) Mathf.Ceil(((Random.value)*sigma + 350));
        int damage = (int) (Mathf.Clamp(coeff,250,700) +(Random.value)*25 + round*10);
        int healing = (int) ((Random.Range(0.05f,0.15f)*MaxHealth) + round);
        int recharge = (int) (Mathf.Clamp(5500,500,(MaxShield-Shield)) + round);
        int buff_percent = (int) ((Random.value)*2*100);
        if (Health<((0.05)*MaxHealth))
        {
            return (2,healing);
        }
        else if (Health<((0.15)*MaxHealth))
        {
            float decision = Random.value;
            if (decision<0.4)
            {
                return (2,healing);
            }
            else
            {
                return (1,damage);
            }
        }
        else if((Shield<3500)&&(Shield>0)){
            return (3,recharge);
        }
        else{ 
            float decision = Random.value;
            if (decision<0.4)
            {
                float decision2 = Random.value;
                if (decision2<0.6)
                {
                    return (4,buff_percent);
                }
                else
                {
                    return (5, (int) (0.35*buff_percent));
                }
            }
            else
            {
                return (1,damage);
            }
        }
    }

    
    
}
