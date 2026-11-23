using UnityEngine;

public class EnemicQuest : MonoBehaviour
{
    public string nomQuest;
    public void Morir()
    {
        QuestManager.instancia.AfegirProgres(nomQuest, 1); // afegir progrés a la missió
        Destroy(gameObject);
    }
}