using UnityEngine;

public class CaixaUI : MonoBehaviour
{
    public Transform content;
    public GameObject itemPrefab;
    public Caixa caixa;
    public Inventari inventari;
    public void RefreshUI()
    {
        foreach (Transform child in content) Destroy(child.gameObject);
        foreach (var stack in caixa.items)
        {
            GameObject obj = Instantiate(itemPrefab, content);
            InventariItemUI ui = obj.GetComponent<InventariItemUI>();
            Item item = inventari.GetItemPerId(stack.id);
            ui.SetupCaixa(item, stack.quantitat, caixa, inventari);
        }
    }
    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}