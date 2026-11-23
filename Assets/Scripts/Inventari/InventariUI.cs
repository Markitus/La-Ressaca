using UnityEngine;

public class InventoryUI : MonoBehaviour // InventariUIManager de canvas
{
    public Inventari inventari;
    public Transform content;
    public GameObject itemPrefab;
    public void RefreshUI()
    {
        foreach (Transform child in content) Destroy(child.gameObject);// borra els items visuals
        foreach (var stack in inventari.inventari.inventariLlista) // cada stack té id i quantitat
        {
            GameObject obj = Instantiate(itemPrefab); // crea la copia del prefab
            obj.transform.SetParent(content, false); // la copia va a content i adapta el text (false)
            InventariItemUI ui = obj.GetComponent<InventariItemUI>(); //s'accedeix a InventariItemUI.cs del item
            Item item = inventari.GetItemPerId(stack.id); // es passen les dades de la id (nom, pes,...)
            ui.Setup(item, stack.quantitat, inventari); //passa les dades per visualitzar-les
        }
    }
}