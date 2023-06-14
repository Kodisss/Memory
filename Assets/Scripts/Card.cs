using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Cards Sprites")]
    [SerializeField] private Sprite cardBack;
    [SerializeField] private Sprite card0;
    [SerializeField] private Sprite card1;
    [SerializeField] private Sprite card2;
    [SerializeField] private Sprite card3;
    [SerializeField] private Sprite card4;
    [SerializeField] private Sprite card5;

    // link with engine and other scripts
    private Memory game;
    private PauseMenu pauseMenu;
    private BoxCollider2D boxCollider;

    private Sprite[] spriteArray = new Sprite[6];

    private SpriteRenderer spriteRenderer;
    private int mySprite;

    // game logic gestion
    private bool found;
    private bool hidden;
    private int myIndex;
    private bool alreadyFlipped;

    // Start is called before the first frame update
    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Memory>();
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        InitializeSpriteArray();
        hidden = true;
        alreadyFlipped = false;
    }

    private void OnMouseDown()
    {
        if (!pauseMenu.GetPaused())
        {
            TurnCard();
            game.InputReceiver(myIndex);
        }
    }

    public void DeactivateBoxCollider()
    {
        boxCollider.enabled = false;
    }

    public void ActivateBoxCollider()
    {
        boxCollider.enabled = true;
    }

    private void InitializeSpriteArray()
    {
        spriteArray[0] = card0;
        spriteArray[1] = card1;
        spriteArray[2] = card2;
        spriteArray[3] = card3;
        spriteArray[4] = card4;
        spriteArray[5] = card5;
    }

    public void SetMySprite(int spriteNumber, int positionInArray)
    {
        mySprite = spriteNumber;
        myIndex = positionInArray;
    }

    public void TurnCard()
    {
        if (found) return;

        if (hidden)
        {
            spriteRenderer.sprite = spriteArray[mySprite];
            alreadyFlipped = true;
            hidden = false;
        }
        else
        {
            spriteRenderer.sprite = cardBack;
            hidden = true;
        }
    }

    public int GetSprite()
    {
        return mySprite;
    }

    public bool IsAlreadyFlipped()
    {
        return alreadyFlipped;
    }

    public bool GetFound()
    {
        return found;
    }

    public bool GetHidden()
    {
        return hidden;
    }

    // tag the card as found and deactivate the box collider
    public void IWasFound()
    {
        found = true;
        boxCollider.enabled = false;
    }
}
