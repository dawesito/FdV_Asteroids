using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Meteorite meteoritePrefab;

    // Nº de meteoritos en pantalla
    public int meteoriteCount = 0;

    // Nivel en el q se está
    private int level = 0;

    // Audio tras pasar de nivel
    public AudioSource audioSource;

    private void Update()
    {
        // En cuanto haya 0 meteoritos, terminamos el nivel.
        if (meteoriteCount == 0)
        {
            //Incremento de nivel, UI de nivel y Play de audio
            level++;
            UpdateLvl();
            if (level != 1)
            {
                audioSource.Play();
            }

            // LVLs: 1=4, 2=6, 3=8, 4=10
            int numAsteroids = 2 + (2 * level);
            for (int i = 0; i < numAsteroids; i++)
            {
                SpawnMeteor();
            }
        }
    }

    // El spawn se basa en una aparición de meteoritos en una las 4 esquinas del escenario.
    private void SpawnMeteor()
    {
        // Esquinas del mapa
        float offset = Random.Range(0f, 1f);
        Vector2 viewportSpawnPosition = Vector2.zero;

        // Seleccion de esquina
        int edge = Random.Range(0, 4);
        if (edge == 0)
        {
            viewportSpawnPosition = new Vector2(offset, 0);
        }
        else if (edge == 1)
        {
            viewportSpawnPosition = new Vector2(offset, 1);
        }
        else if (edge == 2)
        {
            viewportSpawnPosition = new Vector2(0, offset);
        }
        else if (edge == 3)
        {
            viewportSpawnPosition = new Vector2(1, offset);
        }

        // Creación.
        Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
        Meteorite meteor = Instantiate(meteoritePrefab, worldSpawnPosition, Quaternion.identity);
        meteor.gameManager = this;
    }

    // Aumento de nivel
    private void UpdateLvl()
    {
        GameObject go = GameObject.FindGameObjectWithTag("LVL");
        go.GetComponent<Text>().text = "LVL: " + level;

    }

}