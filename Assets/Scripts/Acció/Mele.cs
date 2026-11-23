using UnityEngine;
using UnityEngine.EventSystems;

public class Mele : MonoBehaviour
{
    public float abast = 2f;       // distància curta
    public float dany = 20f;
    public float atacRate = 1f;       // cops per segon; 0.5f 1 cop per segon i 2f dos cops per segon
    private float tempsSeguentAtac = 0f;
    public Camera fpsCam;
    public AudioClip so;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return; // evita actuar sobre UI
        if (Input.GetButtonDown("Fire1") && Time.time >= tempsSeguentAtac) // si ha passat temps suficient des de l'anterior atac llavors pot atacar
        {
            tempsSeguentAtac = Time.time + 1f / atacRate;
            Atac();
        }
    }
    void Atac()
    {
        audioSource.PlayOneShot(so);
        RaycastHit cop;
        if (Physics.SphereCast(fpsCam.transform.position, 0.5f, fpsCam.transform.forward, out cop, abast)) // 0.5f és el gruix del cilindre, ja no és puntual
        {
            Target target = cop.transform.GetComponent<Target>();
            if (target != null)
            {
                target.PrenMal(dany);
            }
        }
    }
}