using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Movimiento
    public float thrustForce = 20f;
    public float rotationSpeed = 200f;
    public float maxAccel = 5f;

    // Balas
    public GameObject _gun, bulletPrefab;

    // Scoring
    public static int SCORE = 0;

    // Pause
    public static bool gamePaused;
    private GameObject resumeText;

    // Propiedades de la nave
    private Rigidbody _rigid;

    // Audio
    public AudioSource audioSource;

    void Start()
    {
        _rigid = GetComponent<Rigidbody>();

        // Busco el UI que tiene Tag "Resume" y lo empiezo a false para que no aparezca.
        resumeText = GameObject.FindGameObjectWithTag("Resume");
        if (resumeText != null)
        {
            resumeText.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        // Rotación y empuje
        float rotation = Input.GetAxis("Rotate") * Time.deltaTime;
        float thrust = Input.GetAxis("Thrust") * Time.deltaTime;

        // Dirección de la nave + incremento de fuerza de empuje y rotación 
        Vector3 thrustDirection = transform.right;
        _rigid.AddForce(thrust * thrustForce * thrustDirection);
        transform.Rotate(Vector3.forward, rotation * rotationSpeed);

        // Decido añadir una velocidad limite para que no se incremente infinitamente
        _rigid.velocity = Vector3.ClampMagnitude(_rigid.velocity, maxAccel);
    }


    void Update()
    {   
        // Input de Disparo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, _gun.transform.position, Quaternion.identity);

            Bullet _balaScript = bullet.GetComponent<Bullet>();

            _balaScript.targetVector = transform.right;
        }

        // Input de Pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = !gamePaused;
            PauseGame();
        }

    }

    // Colision con enemigo
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("He colisionado con otra cosa");
        }
    }

    // Pauso y Resumo Juego
    void PauseGame()
    {
        if (gamePaused)
        {
            Time.timeScale = 0f;
            if (resumeText != null)
            {
                resumeText.SetActive(true);
                audioSource.volume = 0.01f;
            }
        }
        else
        {
            Time.timeScale = 1f;

            if (resumeText != null)
            {
                resumeText.SetActive(false);
                audioSource.volume = 0.05f;
            }
        }
    }

}