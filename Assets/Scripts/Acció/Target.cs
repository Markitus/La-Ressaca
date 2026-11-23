using UnityEngine;
public class Target : MonoBehaviour
{
    public float vida = 100f;
    public string nomQuest;
    public void PrenMal(float quantitat)
    {
        vida -= quantitat;
        if (vida <= 0f)
        {
            Morir();
        }
    }
    void Morir()
    {
        if (QuestManager.instancia != null && !string.IsNullOrEmpty(nomQuest))
        {
            QuestManager.instancia.AfegirProgres(nomQuest, 1);
        }

        Destroy(gameObject);
    }
}