using System.Collections.Generic;
using UnityEngine;

public class Caixa : MonoBehaviour
{
    public List<ItemStack> items;
    public bool potGuardar = true;
    public void Obrir(CaixaUI ui)
    {
        ui.caixa = this; //s'assigna la UI a aquesta caixa
        ui.gameObject.SetActive(true);
        ui.RefreshUI();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void TreureItem(int id) // treiem item de la caixa
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == id)
            {
                items[i].quantitat--;
                if (items[i].quantitat <= 0)
                {
                    items.RemoveAt(i);
                }
                break;
            }
        }
    }
    public void AfegirItem(int id) // posem item a la caixa
    {
        if (potGuardar == false) return;
        foreach (ItemStack stack in items)
        {
            if (stack.id == id)
            {
                stack.quantitat++;
                return;
            }
        }
        items.Add(new ItemStack { id = id, quantitat = 1 });
    }
}