using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instancia;
    public List<Quest> quests = new List<Quest>();
    void Awake() // s'assigna l'objecte únic a la instancia
    {
        instancia = this; 
    }
    public Quest GetQuest(string nom)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].nomQuest == nom)
            {
                return quests[i];
            }
        }
        return null; // si no hi ha missió
    }
    public void AfegirProgres(string nomQuest, int quantitat)
    {
        Quest quest = GetQuest(nomQuest);
        if (quest.estat == QuestEstat.EnProgres)
        {
            quest.quantitatActual += quantitat;
            Debug.Log("missió: " + nomQuest + ", " + quest.quantitatActual + "/" + quest.quantitatRequerida);
        }
    }
}