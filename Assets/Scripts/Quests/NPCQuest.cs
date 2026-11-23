using UnityEngine;
public class NPCQuest : MonoBehaviour
{
    public string nomQuest;
    public int premi;
    public int quantitat;
    public bool ganivet;
    public bool pistola;
    public Dialeg iniciQuest;
    public Dialeg enProgres;
    public Dialeg completat;
    public DialegManager manager;
    public void Interactuar()
    {
        Quest quest = QuestManager.instancia.GetQuest(nomQuest);
        if (quest.estat == QuestEstat.NoIniciada)
        {
            manager.IniciDialeg(iniciQuest);
            GameObject[] objectes = GameObject.FindGameObjectsWithTag(nomQuest);
            foreach (GameObject obj in objectes) // destrueix cada un dels objectes que protegeixen els objectes de missió
            {
                Destroy(obj);
            }
            quest.estat = QuestEstat.EnProgres;
        }
        else if (quest.estat == QuestEstat.EnProgres)
        {
            if (quest.Completat())
            {
                manager.IniciDialeg(completat);
                quest.estat = QuestEstat.Completat;
                DonarRecompensa();
            }
            else
            {
                manager.IniciDialeg(enProgres);
            }
        }
        else if (quest.estat == QuestEstat.Completat)
        {
            manager.IniciDialeg(completat);
        }
    }
    public void AfegirProgres(string nomQuest, int quantitat)
    {
        Quest quest = QuestManager.instancia.GetQuest(nomQuest);
        if (quest.estat == QuestEstat.EnProgres)
        {
            quest.quantitatActual += quantitat;
        }
        Debug.Log("progrés: " + quest.quantitatActual + "/" + quest.quantitatRequerida);
    }
    void DonarRecompensa()
    {
        Debug.Log("recompensa donada");
        Inventari huh = FindFirstObjectByType<Inventari>();
        huh.AfegirItem(premi, quantitat);
        CanviArmes eee = FindFirstObjectByType<CanviArmes>();
        if (ganivet == true) eee.mele = true;
        if (pistola == true) eee.arma = true;
    }
}