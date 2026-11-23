[System.Serializable]
public class Quest
{
    public string nomQuest;
    public QuestEstat estat;
    public int quantitatRequerida;
    public int quantitatActual;
    public bool Completat()
    {
        return quantitatActual >= quantitatRequerida;
    }
}