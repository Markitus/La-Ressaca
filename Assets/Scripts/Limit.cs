using UnityEngine;

public class Limit : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Salut salut = other.GetComponent<Salut>();
            if (salut != null)
            {
                salut.RebreDany(1f * Time.deltaTime);
                salut.PerdreEnergia(1f * Time.deltaTime);
            }
        }
    }
}
