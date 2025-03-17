using System;
using System.Collections;
using System.Collections.Generic;
// using Unity.VisualScripting;
using UnityEngine;
using Holoville.HOTween;
using System.Linq;

public class HorizontalCardHolder : MonoBehaviour
{

    [SerializeField] private Card selectedCard;
    [SerializeReference] private Card hoveredCard;

    [SerializeField] private GameObject slotPrefab;
    private RectTransform rect;

    [Header("Spawn Settings")]
    [SerializeField] private int cardsToSpawn = 7;
    public List<Card> cards;

    bool isCrossing = false;
    [SerializeField] private bool tweenCardReturn = true;

    public int maxSelectedCards = 3;

    public List<Card> selectedCards;

    void Start()
    {
        for (int i = 0; i < cardsToSpawn; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, transform);
            Debug.Log(slotObj.name);
            Card card = slotObj.GetComponentInChildren<Card>();
            Debug.Log(card);
            CardObject cardObj = slotObj.GetComponentInChildren<CardObject>();
            Debug.Log(cardObj);

            if (cardObj != null)
            {
                int cardType = UnityEngine.Random.Range(1, 7);
                Debug.Log("Selected card type: " + cardType.ToString());
                cardObj.Initialize(cardType);
            }
        }

        rect = GetComponent<RectTransform>();
        cards = GetComponentsInChildren<Card>().ToList();

        int cardCount = 0;

        foreach (Card card in cards)
        {
            card.PointerEnterEvent.AddListener(CardPointerEnter);
            card.PointerExitEvent.AddListener(CardPointerExit);
            card.BeginDragEvent.AddListener(BeginDrag);
            card.EndDragEvent.AddListener(EndDrag);
            card.PointerUpEvent.AddListener(CardPointerUp);
            card.name = cardCount.ToString();
            cardCount++;
        }

        StartCoroutine(Frame());

        IEnumerator Frame()
        {
            yield return new WaitForSecondsRealtime(.1f);
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].cardVisual != null)
                    cards[i].cardVisual.UpdateIndex(transform.childCount);
            }
        }
    }

    private void BeginDrag(Card card)
    {
        selectedCard = card;
    }


    void EndDrag(Card card)
    {
        if (selectedCard == null)
            return;

        HOTween.To(selectedCard.transform, tweenCardReturn ? .15f : 0, "localPosition", selectedCard.selected ? new Vector3(0,selectedCard.selectionOffset,0) : Vector3.zero);

        rect.sizeDelta += Vector2.right;
        rect.sizeDelta -= Vector2.right;

        selectedCard = null;

    }

    void CardPointerEnter(Card card)
    {
        hoveredCard = card;
    }

    void CardPointerExit(Card card)
    {
        hoveredCard = null;
    }

    void CardPointerUp(Card card, bool selected)
    {
        if (!selectedCards.Contains(card)){
            if (selectedCards.Count < maxSelectedCards)
            {
                selectedCards.Add(card);
                card.selected = true;
            }
        }
        else
        {
            selectedCards.Remove(card);
            card.selected = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            if (hoveredCard != null)
            {
                Destroy(hoveredCard.transform.parent.gameObject);
                cards.Remove(hoveredCard);

            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            foreach (Card card in cards)
            {
                card.Deselect();
            }
        }

        if (selectedCard == null)
            return;

        if (isCrossing)
            return;

        for (int i = 0; i < cards.Count; i++)
        {

            if (selectedCard.transform.position.x > cards[i].transform.position.x)
            {
                if (selectedCard.ParentIndex() < cards[i].ParentIndex())
                {
                    Swap(i);
                    break;
                }
            }

            if (selectedCard.transform.position.x < cards[i].transform.position.x)
            {
                if (selectedCard.ParentIndex() > cards[i].ParentIndex())
                {
                    Swap(i);
                    break;
                }
            }
        }
    }

    void Swap(int index)
    {
        isCrossing = true;

        Transform focusedParent = selectedCard.transform.parent;
        Transform crossedParent = cards[index].transform.parent;

        cards[index].transform.SetParent(focusedParent);
        cards[index].transform.localPosition = cards[index].selected ? new Vector3(0, cards[index].selectionOffset, 0) : Vector3.zero;
        selectedCard.transform.SetParent(crossedParent);

        isCrossing = false;

        if (cards[index].cardVisual == null)
            return;

        bool swapIsRight = cards[index].ParentIndex() > selectedCard.ParentIndex();
        cards[index].cardVisual.Swap(swapIsRight ? -1 : 1);

        //Updated Visual Indexes
        foreach (Card card in cards)
        {
            card.cardVisual.UpdateIndex(transform.childCount);
        }
    }

    public void SubmitSelectedCards()
    {
        float totalDamage = 0f;

        // Loop through all cards in your HorizontalCardHolder
        foreach (Card card in cards)
        {
            if (card.selected)
            {
                CardObject co = card.GetComponentInChildren<CardObject>();
                if (co != null)
                {
                    // For example, you might sum the "positive" damage values
                    totalDamage += co.positive;
                }
            }
        }

        // Now do something with the total damage, like apply it to an enemy
        Debug.Log("Total Damage Dealt: " + totalDamage);
    }

    public void ClearSelectedCards()
    {
        foreach (Card card in selectedCards)
        {
            cards.Remove(card);
            Destroy(card.transform.parent.gameObject);
        
            GameObject slotObj = Instantiate(slotPrefab, transform);
            Card newCard = slotObj.GetComponentInChildren<Card>();
            CardObject cardObj = slotObj.GetComponentInChildren<CardObject>();

            if (cardObj != null)
            {
                int cardType = UnityEngine.Random.Range(1, 7);
                Debug.Log("Selected card type: " + cardType.ToString());
                cardObj.Initialize(cardType);
            }
        }

        selectedCards.Clear();

        rect = GetComponent<RectTransform>();
        cards = GetComponentsInChildren<Card>().ToList();

        int cardCount = 0;

        foreach (Card card in cards)
        {
            card.PointerEnterEvent.AddListener(CardPointerEnter);
            card.PointerExitEvent.AddListener(CardPointerExit);
            card.BeginDragEvent.AddListener(BeginDrag);
            card.EndDragEvent.AddListener(EndDrag);
            card.PointerUpEvent.AddListener(CardPointerUp);
            card.name = cardCount.ToString();
            cardCount++;
        }

        StartCoroutine(Frame());

        IEnumerator Frame()
        {
            yield return new WaitForSecondsRealtime(.1f);
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].cardVisual != null)
                    cards[i].cardVisual.UpdateIndex(transform.childCount);
            }
        }

    }

}