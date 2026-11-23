using UnityEngine;

public class CanviArmes : MonoBehaviour
{
    public GameObject armaMele;
    public GameObject pistola;
    public GameObject modelM;
    public GameObject modelP;
    public bool mele;
    public bool arma;
    private void Awake()
    {
        mele = false;
        arma = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && mele == true)
        {
            EquipMele();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && arma == true)
        {
            EquipPistola();
        }
    }
    void EquipMele()
    {
        Debug.Log("ganivet");
        armaMele.SetActive(true);
        modelM.SetActive(true);
        pistola.SetActive(false);
        modelP.SetActive(false);
    }
    void EquipPistola()
    {
        Debug.Log("pistola");
        armaMele.SetActive(false);
        modelM.SetActive(false);
        pistola.SetActive(true);
        modelP.SetActive(true);
    }
}