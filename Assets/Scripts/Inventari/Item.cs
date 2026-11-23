using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public int id;
    public string nom;
    public float pes;
    public int cura;
    public int energia;
    public bool consumible;
    public float valor;
}
[System.Serializable]
public class ItemDatabase
{
    public List<Item> items;
}
[System.Serializable]
public class ItemStack
{
    public int id;
    public int quantitat;
}
[System.Serializable]
public class InventariData
{
    public List<ItemStack> inventariLlista;
}