using UnityEngine;
using UnityEngine.SceneManagement;
public class InteraccioJugador : MonoBehaviour
{
    public float distanciaInteraccio = 3f;
    private Camera cam;
    public CaixaUI caixaUI;
    public FirstPersonController playerController;
    public Caixa caixaPropera; //es fa servir a deixaritem() de inventariitemui.cs
    public DialegManager dialegManager;
    bool caixaOberta = false;
    void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TornarAlMenu();
        }
        if (Input.GetKeyDown(KeyCode.E)) // tanquem caixa amb la mateixa tecla e
        {
            if (caixaOberta)
            {
                TancarCaixa();
            }
            else
            {
                Interactua();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dialegManager != null && dialegManager.esDialegActiu)
            {
                dialegManager.SeguentLinia();
            }
        }
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distanciaInteraccio))
        {
            Caixa caixa = hit.collider.GetComponent<Caixa>(); // s'assigna la caixa per raycast
            caixaPropera = caixa;
        }
    }
    void Interactua()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distanciaInteraccio))
        {
            GameObject obj = hit.collider.gameObject;
            if (obj.CompareTag("Quest"))
            {
                obj.GetComponent<NPCQuest>().Interactuar();
            }
            else if (obj.CompareTag("QuestItem"))
            {
                obj.GetComponent<ObjecteMissio>().Recollir();
            }
            else if (obj.CompareTag("Caixa"))
            {
                Caixa caixa = hit.collider.GetComponent<Caixa>();
                caixaUI.caixa = caixa;
                caixaOberta = true;
                caixaUI.gameObject.SetActive(true);
                caixaUI.RefreshUI();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                playerController.enabled = false;
                return;
            }
            else if (obj.CompareTag("Text") || obj.CompareTag("NPCinici"))
            {
                Text t = obj.GetComponent<Text>();
                DialegManager dm = FindFirstObjectByType<DialegManager>(); // aquí s'assigna el dialeg despres d'una cerca
                if (dm.esDialegActiu == false)
                {
                    t.Interactuar(); // interactuar de text.cs
                }
            }
        }
    }
    void TornarAlMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    void TancarCaixa()
    {
        caixaOberta = false;
        caixaUI.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerController.enabled = true;
    }
}