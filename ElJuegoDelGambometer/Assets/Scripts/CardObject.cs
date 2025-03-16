using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{
  private GenerateCardValues genCardValues;
  public float positive;
  public float negative;
  public string text;
  public Sprite cardSprite;

  void Awake()
  {
    
    genCardValues = this.GetComponentInChildren<GenerateCardValues>();
    
  }
  
  public void Initialize(int CardType)
  {
    // 1 : Flat Attack, 2: Mult Attack, 3: Shield, 4: Heal, 5: Double Attack, 6: Ignore Shield
    if (CardType == 1)
    {
        (float,float) values = genCardValues.GenFlatRed();
        positive =  values.Item1;
        negative = values.Item2;
        text = $" If you roll an even number, deal {values.Item1} damage. If you roll an odd number, deal {negative} damage to yourself";
        cardSprite = Resources.Load<Sprite>("Sprites/attack_card");
    }
    if (CardType == 2)
    {
        (float,float) values = genCardValues.GenMultRed();
        positive = (float) Mathf.Round(values.Item1*10)* 0.1f;
        negative =(float) Mathf.Round(values.Item2*10)*0.1f;
        text = $" If you roll an even number, deal {positive} times damage. If you roll an odd number, reduce your damage by a factor of {negative}";
        cardSprite = Resources.Load<Sprite>("Sprites/attack_card");
    }
    if (CardType == 3)
    {
        (float,float) values = genCardValues.GenShield();
       positive = values.Item1;
       negative = values.Item2;
        text = $" If you roll an even number, gain {positive} shield. If you roll an odd number, deal {negative} damage to yourself";
        cardSprite = Resources.Load<Sprite>("Sprites/shield_card");
    }
    if (CardType == 4)
    {
        (float,float) values = genCardValues.GenHeal();
        positive = (float) Mathf.Round(values.Item1*100)*0.01f;
        negative =(float) Mathf.Round(values.Item2*100)*0.01f;
        text = $" If you roll an even number, heal {positive}% of your health. If you roll an odd number, deal {negative}% of your health as self-damage";
        cardSprite = Resources.Load<Sprite>("Sprites/heal_card");
    }
    if (CardType == 5)
    {
        positive = 2.0f;
        negative =0.0f;
        text = $" Double your attack this turn";
        cardSprite = Resources.Load<Sprite>("Sprites/2x_attack_card");
    }
    if (CardType == 6){
      positive = 0.0f;
      negative = 0.0f;
      text = $"Ignore shields";
      cardSprite = Resources.Load<Sprite>("Sprites/ignore_shield_card");
    }
  }
  
}