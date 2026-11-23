using UnityEngine;

public class ConfirmacioUI : MonoBehaviour
{
    public GameObject panel;
    Inventari inventari;
    int itemId;
    public void Mostrar(Inventari inv, int id)
    {
        inventari = inv;
        itemId = id;
        panel.SetActive(true);
    }
    public void Confirmar()
    {
        inventari.TreureItem(itemId);
        inventari.ui.RefreshUI();
        panel.SetActive(false);
    }
    public void Cancelar()
    {
        panel.SetActive(false);
    }
}