using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float forceWeight = 0.5f;
    public float maxSpeed = 3f;
    private Rigidbody2D rb;

    public Tilemap tilemap;
    public TileBase lightIce;  // tile to be replaced
    public TileBase heavyIce;
    public TileBase myIce;  // tile to replace with

    public Vector2 startPosition;  // Start position of the player

    //自分の色に一つでも乗っかってたらはやいっていう設定に

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (currentTile == myIce)
        {
            maxSpeed = 8f;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            transform.position = startPosition;
        }
    }
}
