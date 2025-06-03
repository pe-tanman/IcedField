using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using System.Collections;


public class TileCount : MonoBehaviour
{
    public Tilemap tilemap;  // Reference to the Tilemap component
    public TileBase firstTile;  // Tile to be replaced
    public TileBase secondTile;  // Tile to replace with

    public TextMeshProUGUI tileCounterText;  // UI Text to display the count of tiles


    private int firstTileCount = 0;  // Count of the first tile
    private int secondTileCount = 0;  // Count of the second tile
    public int time = 60;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        startCountdown();
        if (time <= 0)
        {
            getAllTiles();
        }

    }
    void startCountdown()
    {
        IEnumerator TimerCoroutine()
        {
            for (int i = time; i >= 0; i--)
            {
                time--;
                if (tileCounterText != null)
                {
                    tileCounterText.text = i.ToString();
                }
                yield return new WaitForSeconds(1f);
            }
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
            }
        }

        print("Count of first tile: " + firstTileCount);
        print("Count of second tile: " + secondTileCount);
    }
}