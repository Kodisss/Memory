using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class Memory : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private TMP_Text scoreDisplay;
    [SerializeField] private TMP_Text currentPlayerDisplay;

    private int[] cardImages; // Array of card images (front faces)
    private Vector3[] positions; // Array with every card positions
    private Card[] gameBoard;

    private int numberOfCardsPlayed; // tracks the number of cards turned in one turn
    private int firstCardPlayed;
    private int secondCardPlayed;

    private string currentPlayer;
    private int[] playerScores;

    [SerializeField] private bool debug;

    private void Start()
    {
        numberOfCardsPlayed = 0;
        firstCardPlayed = 0;
        secondCardPlayed = 0;

        currentPlayer = "Player 1";
        playerScores = new int[2];
        playerScores[0] = 0;
        playerScores[1] = 0;

        cardImages = new int[12];
        positions = new Vector3[12];
        gameBoard = new Card[12];

        UpdateUIDisplay();
        CalculatePositions();
        GenerateNumbers();
        GenerateCards();
    }

    private void Update()
    {
        CheckForWinner();
    }

    private void CheckForWinner()
    {
        string winner = string.Empty;

        if (playerScores[0] + playerScores[1] == 6)
        {
            if (playerScores[0] > playerScores[1])
            {
                winner = "Player 1";
            }
            else if (playerScores[0] < playerScores[1])
            {
                winner = "Player 2";
            }
            else 
            {
                winner = "No one";
            }
            PlayerPrefs.SetString("Winner", winner);
            SceneManager.LoadScene("EndScreen");
        }
    }

    public void InputReceiver(int cardIndex)
    {
        if(numberOfCardsPlayed == 0)
        {
            firstCardPlayed = cardIndex;
            gameBoard[firstCardPlayed].DeactivateBoxCollider();
            numberOfCardsPlayed = 1;
        }
        else
        {
            secondCardPlayed = cardIndex;
            DeactivateAllBoxColliders();

            CheckIfSameCards();
            Invoke(nameof(ResetEveryNonFoundCards), 1f);
            numberOfCardsPlayed = 0;

            SwapPlayer();
            UpdateUIDisplay();
        }
    }

    private void DeactivateAllBoxColliders()
    {
        for(int i = 0; i < 12; i++)
        {
            gameBoard[i].DeactivateBoxCollider();
        }
    }

    private void CheckIfSameCards()
    {
        if(cardImages[firstCardPlayed] == cardImages[secondCardPlayed])
        {
            gameBoard[firstCardPlayed].IWasFound();
            gameBoard[secondCardPlayed].IWasFound();

            AddScoreToCurrentPlayer();
        }
    }

    private void AddScoreToCurrentPlayer()
    {
        if (currentPlayer == "Player 1")
        {
            playerScores[0]++;
        }
        else
        {
            playerScores[1]++;
        }
    }

    private void UpdateUIDisplay()
    {
        currentPlayerDisplay.text = currentPlayer + " is playing";
        scoreDisplay.text = playerScores[0] + " | " + playerScores[1];
    }

    private void SwapPlayer()
    {
        if (currentPlayer == "Player 1")
        {
            currentPlayer = "Player 2";
        }
        else
        {
            currentPlayer = "Player 1";
        }
    }

    // generate an array of 12 random number from 0 to 5 with each number appearing exactly two times
    private void GenerateNumbers()
    {
        int[] counts = new int[6]; // array to keep track of the number of time a number is used

        for (int i = 0; i < 6; i++)
        {
            int number = i; // number we're tracking
            for (int j = 0; j < 2; j++)
            {
                bool foundEmptySlot = false; 
                while (!foundEmptySlot) // check for an empty slot
                {
                    int index = Random.Range(0, 12); // generate a random number
                    if (cardImages[index] == 0 && counts[number] < 2) // if the number isn't more than once in the array
                    {
                        cardImages[index] = number; // instantiate the number
                        counts[number]++; // add it to the list of numbers already in the list
                        foundEmptySlot = true; // get out of the while loop
                    }
                }
            }
        }

        // debug purposes
        if (debug)
        {
            for (int i = 0;i < 12;i++)
            {
                Debug.Log(cardImages[i]);
            }
        }

    }

    private void GenerateCards()
    {
        for(int i = 0; i < 12 ; i++)
        {
            // Instantiate a new card object
            GameObject cardObject = Instantiate(cardPrefab, transform);

            // Get the Card script component attached to the card object
            gameBoard[i] = cardObject.GetComponent<Card>();

            gameBoard[i].SetMySprite(cardImages[i], i);

            cardObject.transform.position = positions[i];
        }
    }

    private void CalculatePositions()
    {
        int index = 0;

        // indexes to place the cards
        float x = -5f;
        float y = 0f;

        // things to help with the placements of the cards
        float yHelp1 = 3.6f;
        float yHelp2 = 1.3f;

        for(int i = 0; i < 4 ; i++)
        {
            if (i == 0) y = yHelp1;
            else if (i == 1) y = yHelp2;
            else if (i == 2) y = -yHelp2;
            else if (i == 3) y = -yHelp1;

            for(int j = 0; j < 3 ; j++) 
            {
                positions[index] = new Vector3(x,y,0);
                x += 5f;
                index++;
            }
            x = -5f;
        }

        // debug purposes
        if (debug)
        {
            for (int i = 0; i < 12; i++)
            {
                Debug.Log(positions[i]);
            }
        }
    }

    private void ResetEveryNonFoundCards()
    {
        for(int i = 0;i < 12;i++)
        {
            if (!gameBoard[i].GetFound() && !gameBoard[i].GetHidden())
            {
                gameBoard[i].TurnCard();
            }

            if (!gameBoard[i].GetFound()) gameBoard[i].ActivateBoxCollider(); // activate all the non found card box collider so we can play
        }
    }
}
