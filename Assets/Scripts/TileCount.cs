using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using System.Collections;
using System;


public class TileCount : MonoBehaviour
{
    public Tilemap tilemap;  // Reference to the Tilemap component
    public TileBase firstTile;  // Tile to be replaced
    public TileBase secondTile;  // Tile to replace with
    public TileBase seaTile;  // Tile to replace with

    public TextMeshProUGUI timeCounterText1; 
    public TextMeshProUGUI timeCounterText2;  // UI Text to display the countdown
    public TextMeshProUGUI Player1ScoreText;  // UI Text to display the countdown
    public TextMeshProUGUI Player2ScoreText;  // UI Text to display the countdown
    public GameObject resultBoard;  // Reference to the result board
    public GameObject readyBoard1;
    public GameObject readyBoard2;


    private int firstTileCount = 0;  // Count of the first tile
    private int secondTileCount = 0;  // Count of the second tile
    private int fieldTileCount = 0;
    public int time = 60;
    public int readyTime = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        startCountdown();
    }
    void startCountdown()
    {

        IEnumerator TimerCoroutine()
        {
            for (int i = readyTime; i >= 0; i--)
            {
                readyTime--;
                    timeCounterText1.text = i.ToString();
                    timeCounterText2.text = i.ToString();
                yield return new WaitForSeconds(1f);
            }
            readyBoard1.SetActive(false);
            readyBoard2.SetActive(false);
            for (int i = time; i >= 0; i--)
            {
                time--;
                timeCounterText1.text = i.ToString();
                timeCounterText2.text = i.ToString();
                yield return new WaitForSeconds(1f);
            }
                getAllTiles();
            
        }
        StartCoroutine(TimerCoroutine());
    }
    void getAllTiles()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile == firstTile)
                {
                    firstTileCount++;
                }
                else if (tile == secondTile)
                {
                    secondTileCount++;
                }

                if(tile != seaTile)
                fieldTileCount++;
            }
        }

        float convertedFirstTileCount = (float)firstTileCount;
        float convertedSecondTileCount = (float)secondTileCount;
        float totalTiles = (float)fieldTileCount;

        float player1Score = (convertedFirstTileCount / totalTiles) * 100f;
        float player2Score = (convertedSecondTileCount / totalTiles) * 100f;


        Player1ScoreText.text = Math.Round(player1Score, 1).ToString() + "%";
        Player2ScoreText.text = Math.Round(player2Score, 1).ToString() + "%";

        resultBoard.SetActive(true);
    }
}