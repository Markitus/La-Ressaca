using UnityEngine;

public class ObjecteMissio : MonoBehaviour
{
    public string nomQuest;
    public void Recollir()
    {
        QuestManager.instancia.AfegirProgres(nomQuest, 1);
        Destroy(gameObject);
    }
}