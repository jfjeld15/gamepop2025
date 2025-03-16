using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemyStats : MonoBehaviour
{
    public (int,int) GenScrapStats(int round = 3){
        if (round<3){
            return (4000,0);
        }
        else
        {
            int health = (int) (4500 + (round)*(Random.value)*200);
            int shield = (int) ((round-2)*(Random.value)*40);
            return(health,shield);
        }
    }
    public (int,int) GenGoldStats(int round = 3){
        if (round<3){
            return (4000,0);
        }
        else
        {
            int health = (int) (5500 + (round)*(Random.value)*200);
            int shield = (int) ((round-2)*(Random.value)*40);
            return(health,shield);
        }
    }

    public (int,int) GenRedGoldStats(int round = 3){
        if (round<3){
            return (4000,0);
        }
        else
        {
            int health = (int) (7000 + (round)*(Random.value)*350);
            int shield = (int) ((round-2)*(Random.value)*500);
            return(health,shield);
        }
    }

    public (int,int) GenBossStats(int round = 12){
        if (round<3){
            return (4000,0);
        }
        else
        {
            int health = (int) (12500 + (round-10)*(Random.value)*250);
            int shield =  (int) (6000 + (Random.value)*500);
            return(health,shield);
        }
    }


    
}
