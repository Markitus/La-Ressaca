using UnityEngine;
using TMPro;
public class DialegManager : MonoBehaviour // a canvas
{
    public GameObject dialegPanel;
    public TextMeshProUGUI dialegText;
    private string[] linies;
    private int currentLinia;
    public bool esDialegActiu = false;
    public FirstPersonController playerController;
    void Update()
    {
        if (esDialegActiu && Input.GetKeyDown(KeyCode.Space)) SeguentLinia();
    }
    public void IniciDialeg(Dialeg dialeg)
    {
        playerController.enabled = false;
        esDialegActiu = true;
        linies = dialeg.linies; // es guarden les linies
        currentLinia = 0;
        dialegPanel.SetActive(true);  // mostra la UI
        MostraLinia();
    }
    public void MostraLinia()
    {
        if (currentLinia < linies.Length) dialegText.text = linies[currentLinia];
    }
    public void SeguentLinia()
    {
        currentLinia++;
        if (currentLinia < linies.Length)
        {
            MostraLinia();
        }
        else
        {
            EndDialeg();
        }
    }
    void EndDialeg()
    {
        playerController.enabled = true;
        esDialegActiu = false;
        dialegPanel.SetActive(false);  // amaga la UI
        GameObject porta = GameObject.FindWithTag("Porta");
        porta.transform.eulerAngles = new Vector3(0, 0, 0);
        GameObject npc = GameObject.FindWithTag("NPCinici");
        npc.transform.position = new Vector3(0, -10, 0); 
    }
}