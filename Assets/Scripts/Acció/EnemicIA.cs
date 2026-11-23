using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Threading;
public class EnemicIA : MonoBehaviour
{
    enum Comportament { Patrulla, Fugir, Perseguir, Atacar}
    Comportament estat;
    public Transform player;
    NavMeshAgent agent;
    public bool presa = false; //  false = ataca, true = fuig
    public float rangDeteccio = 10f;
    public float rangAtac = 2f;
    public float rangPerdua = 15f;
    public float distanciaFugida = 12f;
    public float tempsPerdua = 5f;
    public float cooldownAtac = 1.5f;
    public float radiPatrulla = 10f;
    public float esperaPatrulla = 2f;
    float tempsUltimAtac;
    bool espera;
    public Transform zonaCentre;
    public float radiZona = 10f;
    public bool pistola = false;
    public float distanciaDispar = 10f;
    public float cooldownDispar = 1f;
    public int dany = 10;
    [Range(0f, 1f)] public float tempsResposta = 0.5f; //  0 s'ho pensa massa i 1 decideix disparar sempre
    [Range(0f, 1f)] public float precisio = 0.8f; //  0 no encerta cap tret i 1 els encerta tots
    public AudioClip so;
    private AudioSource audioSource;
    float temps = 0;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        estat = Comportament.Patrulla;
        player = GameObject.FindWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        so = Resources.Load<AudioClip>("Sons/tretenemic");
    }
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);  // distància entre enemic i jugador
        switch (estat)
        {
            case Comportament.Patrulla:
                if (agent.hasPath == false  && espera == false) StartCoroutine(Patrullar());
                if (dist < rangDeteccio) // comportament de l'enemic si el jugador està a prop
                {
                    if (presa == true)
                        estat = Comportament.Fugir;
                    else
                    {
                        estat = Comportament.Perseguir;
                    }
                }
                break;
            case Comportament.Perseguir:
                if (pistola == true)
                {
                    if (dist < distanciaDispar) estat = Comportament.Atacar; // atac a distància
                }
                else
                {
                    if (dist < rangAtac) estat = Comportament.Atacar; //  cos a cos
                }
                if (dist > rangPerdua)
                {
                    agent.destination = agent.transform.position;
                    temps += Time.deltaTime;
                    if(temps >= 10f) TornarZona();
                }
                else agent.destination = player.position;
                break;
            case Comportament.Atacar:
                agent.isStopped = true;
                Vector3 lookPos = player.position - transform.position;
                lookPos.y = 0;
                transform.rotation = Quaternion.LookRotation(lookPos); // gira l'enemic
                if (pistola == true) //  ataca a distància
                {
                    if (dist > distanciaDispar) // no es troba a l'abast
                    {
                        agent.isStopped = false;
                        estat = Comportament.Perseguir;
                        break;
                    }
                    if (Time.time > tempsUltimAtac + cooldownDispar) // temps entre atacs
                    {
                        Debug.Log("wa");
                        if (Random.value < tempsResposta)
                        {

                            Disparar();
                            tempsUltimAtac = Time.time;
                        }
                    }
                }
                else
                {
                    if (dist > rangAtac) //  cos a cos
                    {
                        agent.isStopped = false;
                        estat = Comportament.Perseguir;
                        break;
                    }
                    if (Time.time > tempsUltimAtac + cooldownAtac)
                    {
                        player.GetComponent<Salut>().RebreDany(10);
                        tempsUltimAtac = Time.time;
                    }
                }
                break;
            case Comportament.Fugir:
                Vector3 dir = transform.position - player.transform.position;
                Vector3 newPos = transform.position + dir;
                agent.SetDestination(newPos);
                if (dist > rangPerdua) estat = Comportament.Patrulla;
                break;
        }
    }
    IEnumerator Patrullar()
    {
        espera = true;
        yield return new WaitForSeconds(esperaPatrulla);
        Vector3 centre = zonaCentre.position;
        Vector3 puntRandom = centre + Random.insideUnitSphere * radiZona; // valor aleatori d'una esfera de radi 1
        NavMeshHit hit;
        if (NavMesh.SamplePosition(puntRandom, out hit, radiZona, NavMesh.AllAreas)) agent.destination = hit.position;
        espera = false;
    }
    void TornarZona()
    {
        agent.destination = zonaCentre.position;
        temps = 0;
        if (agent.hasPath == false && agent.remainingDistance < 1f) estat = Comportament.Patrulla;
    }
    void Disparar()
    {
        Debug.Log("Pam!");
        audioSource.PlayOneShot(so);
        Vector3 origen = transform.position + Vector3.up;
        Vector3 direccio = (player.position - origen).normalized; //  direcció cap al jugador
        RaycastHit hit;
        if (Physics.Raycast(origen, direccio, out hit, distanciaDispar)) // impacte sobre gameobject
        {
            if (hit.transform.CompareTag("Player")) // impacte sobre jugador
            {
                if (Random.value < precisio) // probabilitat d'encertar
                {
                    hit.transform.GetComponent<Salut>().RebreDany(dany);
                }
            }
        }
    }
}