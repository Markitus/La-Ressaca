using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;
public class Pistola : MonoBehaviour
{
    public float abast = 100f;
    public float dany = 10f;
    public Camera fpsCam;
    public AudioClip so;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.GetButtonDown("Fire1"))   // clic esquerre
        {
            Disparar();
        }
    }
    void Disparar()
    {
        audioSource.PlayOneShot(so);
        RaycastHit cop;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out cop, abast))
        {
            Target target = cop.transform.GetComponent<Target>();
            if (target != null)
            {
                Debug.Log("has impactat: " + cop.transform.name);
                target.PrenMal(dany);
            }
        }
    }
}