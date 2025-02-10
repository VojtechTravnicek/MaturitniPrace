using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Rychlost pohybu
    [SerializeField] private float jumpForce = 10f; // Síla skoku
    [SerializeField] private int maxLives = 1; // Maximální počet životů

    private int currentLives; // Aktuální počet životů
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        currentLives = maxLives; // Nastavení životů na maximum při startu hry
    }

    private void Update()
    {
        // Pohyb hráče
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Obracení postavy podle směru pohybu
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(0.2f, 0.2f, 1);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-0.2f, 0.2f, 1);

        // Skákání při stisknutí mezerníku, pokud je hráč na zemi
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(body.velocity.y) < 0.01f)
            body.velocity = new Vector2(body.velocity.x, jumpForce);
    }

    // Funkce na odečítání životů
    public void TakeDamage()
    {
        currentLives--;
        Debug.Log("Životy: " + currentLives);

        if (currentLives <= 0)
        {
            Debug.Log("Hráč zemřel!");
            RestartLevel();
        }
    }

    // Detekce kolize s ostny nebo jinými smrtícími objekty
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Deadly"))
        {
            Debug.Log("Hráč se dotkl ostnů!");
            TakeDamage();
        }
    }

    // Restart úrovně po smrti
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
