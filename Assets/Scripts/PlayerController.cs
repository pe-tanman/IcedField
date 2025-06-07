using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour
{
    public enum PlayerType { WASD, YGHJ, ArrowKeys, Mouse }
    public PlayerType playerType = PlayerType.WASD;
    float forceWeight = 600f;
    float maxSpeed = 3f;
    bool isControlEnabled = false;
    private Rigidbody2D rb;


    public Tilemap tilemap;
    public TileBase lightIce;  // tile to be replaced
    public TileBase heavyIce;
    public TileBase seaTile;
    public TileBase myIce;  // tile to replace with

    public Vector2 startPosition;  // Start position of the player

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(EnableControlAfterReady(3f));
        Debug.developerConsoleVisible = true;
    }

    void Update()
    {
        Move();
        Rotate();
        OnTile();
    }

    void Rotate()
    {
        if (rb.linearVelocity.sqrMagnitude > 0.001f)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        }
    }
    void Move()
    {
        if (!isControlEnabled) return;

        Vector2 inputDir = Vector2.zero;

        switch (playerType)
        {
            case PlayerType.WASD:
                if (Keyboard.current.wKey.isPressed)
                    inputDir.y += 1f;
                if (Keyboard.current.sKey.isPressed)
                    inputDir.y -= 1f;
                if (Keyboard.current.dKey.isPressed)
                    inputDir.x += 1f;
                if (Keyboard.current.aKey.isPressed)
                    inputDir.x -= 1f;
                break;
            case PlayerType.YGHJ:
                if (Keyboard.current.yKey.isPressed)
                    inputDir.y += 1f;
                if (Keyboard.current.hKey.isPressed)
                    inputDir.y -= 1f;
                if (Keyboard.current.gKey.isPressed)
                    inputDir.x -= 1f;
                if (Keyboard.current.jKey.isPressed)
                    inputDir.x += 1f;
                break;
            case PlayerType.ArrowKeys:
                if (Keyboard.current.upArrowKey.isPressed)
                    inputDir.y += 1f;
                if (Keyboard.current.downArrowKey.isPressed)
                    inputDir.y -= 1f;
                if (Keyboard.current.rightArrowKey.isPressed)
                    inputDir.x += 1f;
                if (Keyboard.current.leftArrowKey.isPressed)
                    inputDir.x -= 1f;
                break;
            case PlayerType.Mouse:
                inputDir = Gamepad.current.leftStick.ReadValue();
                break;
        }

        inputDir = inputDir.normalized;

        if (Vector2.Dot(rb.linearVelocity, inputDir) < maxSpeed)
        {
            rb.AddForce(inputDir * forceWeight * Time.deltaTime, ForceMode2D.Force); // is time.deltaTime necessary here?
        }
        return;



    }
    void OnTile()
    {
        Vector3 playerWorldPos = transform.position;
        Vector3Int cellPos = tilemap.WorldToCell(playerWorldPos);
        TileBase currentTile = tilemap.GetTile(cellPos);

        if (currentTile == lightIce)
        {
           
            changeTile(cellPos);
            maxSpeed = 3f;
        }
        else if (currentTile == heavyIce)
        {
            maxSpeed = 0.5f;
        }
        else if (currentTile == myIce)
        {
            maxSpeed = 10f;
        }
        else if (currentTile == seaTile)
        {
            if (isControlEnabled)
            {
                isControlEnabled = false;
                StartCoroutine(EnableControlAfterDelay(3f));
                StartCoroutine(ShrinkOverTime(3f, 10));
                rb.linearVelocity *= 0.5f;
            }
        }
        else
        {
            StartCoroutine(LateChangeColor(0.01f, cellPos));
            rb.linearVelocity = rb.linearVelocity * 0.5f;
            maxSpeed = 0.5f;
        }
    }

    private void changeTile(Vector3Int cellPos)
    {
        float zRotation = transform.rotation.eulerAngles.z;
        Vector3Int rightBack = Vector3Int.zero;
        Vector3Int leftBack = Vector3Int.zero;

        if (zRotation <= 22.5 || 337.5 < zRotation)
        {
            // Up
            rightBack = cellPos + new Vector3Int(1, -1, 0);
            leftBack = cellPos + new Vector3Int(-1, -1, 0);
        }
        else if (22.5 < zRotation && zRotation <= 67.5)
        {
            // Up-Left
            rightBack = cellPos + new Vector3Int(1, 0, 0);
            leftBack = cellPos + new Vector3Int(0, -1, 0);
        }
        else if (67.5 < zRotation && zRotation <= 112.5)
        {
            // Left
            rightBack = cellPos + new Vector3Int(1, 1, 0);
            leftBack = cellPos + new Vector3Int(1, -1, 0);
        }
        else if (112.5 < zRotation && zRotation <= 157.5)
        {
            // Down-Left
            rightBack = cellPos + new Vector3Int(1, 0, 0);
            leftBack = cellPos + new Vector3Int(0, 1, 0);
        }
        else if (157.5 < zRotation && zRotation <= 202.5)
        {
            // Down
            rightBack = cellPos + new Vector3Int(1, 1, 0);
            leftBack = cellPos + new Vector3Int(-1, 1, 0);
        }
        else if (202.5 < zRotation && zRotation <= 247.5)
        {
            // Down-Right
            rightBack = cellPos + new Vector3Int(0, 1, 0);
            leftBack = cellPos + new Vector3Int(-1, 0, 0);
        }
        else if (247.5 < zRotation && zRotation <= 292.5)
        {
            // Right
            rightBack = cellPos + new Vector3Int(-1, -1, 0);
            leftBack = cellPos + new Vector3Int(-1, 1, 0);
        }
        else if (292.5 < zRotation && zRotation <= 337.5)
        {
            // Up-Right
            rightBack = cellPos + new Vector3Int(-1, 0, 0);
            leftBack = cellPos + new Vector3Int(0, -1, 0);
        }

        tilemap.SetTile(cellPos, myIce);
        tilemap.SetTile(rightBack, myIce);
        tilemap.SetTile(leftBack, myIce);
    }
    private IEnumerator EnableControlAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = startPosition;
        isControlEnabled = true;
        rb.linearVelocity = Vector2.zero; // Reset velocity
    }
    private IEnumerator EnableControlAfterReady(float delay)
    {
        yield return new WaitForSeconds(delay);
        isControlEnabled = true;
        StartCoroutine(StartCountdown(60)); // Start countdown for 60 seconds


    }
    private IEnumerator ShrinkOverTime(float duration, int steps)
    {
        Vector3 originalScale = transform.localScale;
        for (int i = 0; i < steps; i++)
        {
            float t = (float)i / (steps - 1);
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            yield return new WaitForSeconds(duration / steps);
        }
        transform.localScale = new Vector3(1, 1, 1); // Reset scale
    }
    private IEnumerator LateChangeColor(float duration, Vector3Int cellPos)
    {
        yield return new WaitForSeconds(duration);
        changeTile(cellPos);

    }
    private IEnumerator StartCountdown(int time)
    {
        yield return new WaitForSeconds(time);
        rb.linearVelocity = Vector2.zero;
        isControlEnabled = false;
        transform.GetComponentInChildren<Camera>().enabled = false;
    }

}
