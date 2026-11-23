using UnityEngine;

public class Text : MonoBehaviour
{
    public Dialeg dialeg;
    public DialegManager dialegManager;
    public void Interactuar()
    {
        dialegManager.IniciDialeg(dialeg);
    }
}