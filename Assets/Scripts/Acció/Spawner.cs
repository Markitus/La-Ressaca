using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemic;
    public float tempsRespawn = 5f;
    GameObject enemicActual;
    float temps;
    void Start()
    {
        Spawn();
    }
    void Update()
    {
        if (enemicActual == null) // quan es mor l'enemic espera uns segons
        {
            temps += Time.deltaTime;
            if (temps >= tempsRespawn)
            {
                Spawn();
                temps = 0;
            }
        }
    }
    void Spawn()
    {
        enemicActual = Instantiate(enemic, transform.position, Quaternion.identity); // crea la copia (clon) sense girar-lo
        EnemicIA ia = enemicActual.GetComponent<EnemicIA>();
        ia.zonaCentre = transform;
    }
}