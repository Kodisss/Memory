using System.Collections;
using System.Collections.Generic;
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

    private Sprite[] spriteArray = new Sprite[6];

    private SpriteRenderer spriteRenderer;

    private Memory game;
    private PauseMenu pauseMenu;

    private bool hidden;

    private int mySprite;

    // Start is called before the first frame update
    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Memory>();
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        InitializeSpriteArray();
        hidden = true;
    }

    private void OnMouseDown()
    {
        if (!pauseMenu.GetPaused()) TurnCard();
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

    public void SetMySprite(int input)
    {
        mySprite = input;
    }

    private void TurnCard()
    {
        if (hidden)
        {
            spriteRenderer.sprite = spriteArray[mySprite];
            hidden = false;
        }
        else
        {
            spriteRenderer.sprite = cardBack;
            hidden = true;
        }
    }
}
