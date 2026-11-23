using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class InventariItemUI : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI text;
    public GameObject panel;
    int itemId;
    int curar;
    int energia;
    bool consumible;
    Inventari inventari;
    Salut salut;
    Caixa caixa;
    bool esCaixa = false;
    void Awake()
    {
        salut = FindFirstObjectByType<Salut>();
    }
    public void Setup(Item item, int quantitat, Inventari inv)
    {
        itemId = item.id;
        curar = item.cura;
        energia = item.energia;
        consumible = item.consumible;
        inventari = inv;
        text.text = item.nom + " x" + quantitat + " Pes/pes total: " + item.pes + "/" + item.pes * quantitat + " Cura: " + item.cura 
            + " Energia: " + item.energia + " Valor: " + item.valor;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            UsarItem();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if(esCaixa == false) DeixarItem();
        }
    }
    void DeixarItem()
    {
        InteraccioJugador jugador = FindFirstObjectByType<InteraccioJugador>();
        ConfirmacioUI confirmUI = FindFirstObjectByType<ConfirmacioUI>();
        if (jugador.caixaPropera != null) // es destrueix l'item apretant el botó dret si no volem intercanviar coses amb una caixa
        {
            if (jugador.caixaPropera.potGuardar)
            {
                jugador.caixaPropera.AfegirItem(itemId);
                inventari.TreureItem(itemId);
                inventari.ui.RefreshUI();
            }
        }
        else
        {
            confirmUI.Mostrar(inventari, itemId);
        }
    }
    void UsarItem()
    {
        if (esCaixa == true)
        {
            if (inventari.AfegirItem(itemId) == true) // els objectes desapareixen de la caixa només si s'agafen
            {
                caixa.TreureItem(itemId);
            }
        }
        else
        {
            if (consumible == true)
            {
                salut.Menjar(energia);
                salut.UsarMedecina(curar);
                inventari.TreureItem(itemId);
                inventari.ui.RefreshUI();
            }
        }
        CaixaUI cUI = FindAnyObjectByType<CaixaUI>();
        if (cUI != null) cUI.RefreshUI();
        InventoryUI iUI = FindAnyObjectByType<InventoryUI>();
        if (iUI != null) iUI.RefreshUI();
    }
    public void SetupCaixa(Item item, int quantitat, Caixa c, Inventari inv)
    {
        itemId = item.id;
        caixa = c;
        inventari = inv;
        esCaixa = true;
        text.text = item.nom + " x" + quantitat;
    }
}