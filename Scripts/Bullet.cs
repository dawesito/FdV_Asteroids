using UnityEngine;
public class Bullet : MonoBehaviour
{

    public float speed = 200f;
    public float maxLifeTime = 2f;
    public Vector3 targetVector;

    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    void Update()
    {
        transform.Translate(speed * targetVector * Time.deltaTime);
    }

    //Muevo scoring a Meteorite
}