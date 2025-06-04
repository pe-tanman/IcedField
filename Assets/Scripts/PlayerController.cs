using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;


public class PlayerController : MonoBehaviour
{
    public float forceWeight = 0.5f;
    public float maxSpeed = 3f;
    public bool isControlEnabled = true;
    private Rigidbody2D rb;


    public Tilemap tilemap;
    public TileBase lightIce;  // tile to be replaced
    public TileBase heavyIce;
    public TileBase seaTile;
    public TileBase myIce;  // tile to replace with

    public Vector2 startPosition;  // Start position of the player
    bool isFirstEntered = true;

    //自分の色に一つでも乗っかってたらはやいっていう設定に

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(StartCountdown(60));

    }

    void Update()
    {
        Move();
        Rotate();
        ChangeColor();
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
        if (Keyboard.current.wKey.isPressed && Vector2.Dot(rb.linearVelocity, Vector2.up) < maxSpeed)
            inputDir.y += 1f;
        if (Keyboard.current.sKey.isPressed && Vector2.Dot(rb.linearVelocity, Vector2.down) < maxSpeed)
            inputDir.y -= 1f;
        if (Keyboard.current.dKey.isPressed && Vector2.Dot(rb.linearVelocity, Vector2.right) < maxSpeed)
            inputDir.x += 1f;
        if (Keyboard.current.aKey.isPressed && Vector2.Dot(rb.linearVelocity, Vector2.left) < maxSpeed)
            inputDir.x -= 1f;

        inputDir = inputDir.normalized;
        rb.AddForce(inputDir * forceWeight, ForceMode2D.Force);

    }
    void ChangeColor()
    {
        Vector3 playerWorldPos = transform.position;
        Vector3Int cellPos = tilemap.WorldToCell(playerWorldPos);
        TileBase currentTile = tilemap.GetTile(cellPos);

        if (currentTile == lightIce)
        {
            tilemap.SetTile(cellPos, myIce);
            maxSpeed = 2f;
        }
        else if (currentTile == heavyIce)
        {
            maxSpeed = 0.5f;
        }
        else if (currentTile == myIce)
        {
            maxSpeed = 8f;
            //画面揺らしモーションを入れたい
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


    private IEnumerator EnableControlAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = startPosition;
        isControlEnabled = true;
        rb.linearVelocity = Vector2.zero; // Reset velocity
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
        tilemap.SetTile(cellPos, myIce);

    }
    private IEnumerator StartCountdown(int time)
    {
        yield return new WaitForSeconds(time);
        rb.linearVelocity = Vector2.zero;
        isControlEnabled = false;
    }
}
