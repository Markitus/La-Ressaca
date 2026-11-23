using UnityEngine;
using System.IO;
using System.Collections.Generic;
using TMPro;
public class Inventari : MonoBehaviour
{
    public TextMeshProUGUI pes;
    public ItemDatabase database;
    public InventariData inventari;
    public Salut salut;
    string pathInventari;
    public float pesMaxim = 20f;
    public InventoryUI ui;
    public GameObject inventoryPanel;
    bool inventariObert = false;
    public FirstPersonController playerController;
    void Start()
    {
        CarregarDatabase();
        CarregarInventari();
        inventoryPanel.SetActive(false);
    }
    void CarregarDatabase()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "data.json");
        string json = File.ReadAllText(path);
        database = JsonUtility.FromJson<ItemDatabase>(json);
    }
    void CarregarInventari()  // carrega l'inventari
    {
        pathInventari = Application.persistentDataPath + "/inventari.json"; // ../AppData/LocalLow/DefaultCompany/prova/inventari.json
        if (File.Exists(pathInventari))
        {
            string json = File.ReadAllText(pathInventari);
            inventari = JsonUtility.FromJson<InventariData>(json);
        }
        else
        {
            inventari = new InventariData(); // es crea inventari nou
            inventari.inventariLlista = new List<ItemStack>(); // nova llista d'items buida
        }
    }
    void GuardarInventari()
    {
        string json = JsonUtility.ToJson(inventari, true); // el true evita llistar en una sola linia
        File.WriteAllText(pathInventari, json);
    }
    public Item GetItemPerId(int id) // obtenir l'item respecte la id
    {
        for (int i = 0; i < database.items.Count; i++)
        {
            if (database.items[i].id == id) return database.items[i]; // per cada item i es comprova si i.id=id
        }
        return null;
    }
    bool PotAfegirItem(int id, int quantitat = 1)
    {
        Item item = GetItemPerId(id);
        float total = 0f;
        foreach (var stack in inventari.inventariLlista) // calculem el pes total actual
        {
            Item item2 = GetItemPerId(stack.id);
            total += item2.pes * stack.quantitat;
        }
        float pesAfegir = item.pes * quantitat;
        return (total + pesAfegir) <= pesMaxim;
    }
    public bool AfegirItem(int id, int quantitatQ = 1) // afegim item
    {
        if (PotAfegirItem(id) == false)
        {
            Debug.Log("no pots portar més pes!");
            return false;
        }
        ItemStack stack = FindStackById(id); // mirem si ja tenim l'item a l'inventari
        if (stack != null) // si ja hi és afegim quantitat
        {
            stack.quantitat++;
        }
        else
        {
            inventari.inventariLlista.Add(new ItemStack // si no hi és afegim la nova id i establim la quantitat a 1
            {
                id = id,
                quantitat = quantitatQ
            });
        }
        GuardarInventari();
        return true;
    }
    public void TreureItem(int id) // eliminar item de l'inventari
    {
        ItemStack stack = FindStackById(id);
        if (stack != null)
        {
            stack.quantitat--;
            if (stack.quantitat <= 0)
            {
                inventari.inventariLlista.Remove(stack);
            }
            float total = 0f;
            foreach (var stack2 in inventari.inventariLlista) // calculem el pes total actual
            {
                Item item2 = GetItemPerId(stack2.id);
                total += item2.pes * stack2.quantitat;
            }
            pes.text = "Pes: " + total + "/" + pesMaxim;
            GuardarInventari();
        }
    }
    ItemStack FindStackById(int id) // el mateix que item però per stack
    {
        for (int i = 0; i < inventari.inventariLlista.Count; i++)
        {
            if (inventari.inventariLlista[i].id == id) return inventari.inventariLlista[i];
        }
        return null;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventariObert = !inventariObert;
            inventoryPanel.SetActive(inventariObert);
            if (inventariObert)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                ui.RefreshUI(); // actualitza inventari
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            float total = 0f;
            foreach (var stack in inventari.inventariLlista) // calculem el pes total actual
            {
                Item item2 = GetItemPerId(stack.id);
                total += item2.pes * stack.quantitat;
            }
            pes.text = "Pes: " + total + "/" + pesMaxim;
            playerController.enabled = !inventariObert;
        }
    }
}