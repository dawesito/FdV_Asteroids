using UnityEngine;
using UnityEngine.UI;

public class Meteorite : MonoBehaviour
{
    // Tamaño del Meteorito
    public float size = 0.6f;
    // GameManager
    public GameManager gameManager;

    private void Start()
    {
        // Meteoritos
        transform.localScale = 0.5f * size * Vector3.one;
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector2 direction = new Vector2(Random.value, Random.value).normalized;
        float spawnSpeed = Random.Range(4f - size, 5f - size);
        rb.AddForce(direction * spawnSpeed, ForceMode.Impulse);

        gameManager.meteoriteCount++;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Disminuye el contador de meteoritos por cada uno eliminado.
            gameManager.meteoriteCount--;
            IncreaseScore();
            // Destruye la bala.
            Destroy(collision.gameObject);
            // El tamaño es 0.6, así que aseguro una división solo.
            if (size > 0.5)
            {
                for (int i = 0; i < 2; i++)
                {
                    Meteorite newMeteorite = Instantiate(this, transform.position, Quaternion.identity);
                    newMeteorite.size = size / 2;    // El nuevo tamaño es 0.3
                    newMeteorite.gameManager = gameManager;
                }
            }
            Destroy(gameObject);
        }
    }
    

    // Scoring que trasladé desde Bullet.cs
    private void IncreaseScore()
    {
        Player.SCORE++;
        Debug.Log(Player.SCORE);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
            go.GetComponent<Text>().text = Player.SCORE * 10 + " Pts";
    }

}
