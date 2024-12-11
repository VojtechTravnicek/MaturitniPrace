using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Nastavení základní rychlosti
    [SerializeField] private float jumpForce = 10f; // Nastavení síly skoku
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Získání vstupu na horizontální ose
        float horizontalInput = Input.GetAxis("Horizontal");

        // Nastavení horizontální rychlosti
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        // Obracení postavy podle směru pohybu
         if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(0.2f, 0.2f, 1); // Menší velikost
         else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-0.2f, 0.2f, 1); // Menší velikost se záporným X pro otočení

        // Skákání při stisknutí mezerníku
        if (Input.GetKey(KeyCode.Space) && Mathf.Abs(body.linearVelocity.y) < 0.01f)
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
    }
}