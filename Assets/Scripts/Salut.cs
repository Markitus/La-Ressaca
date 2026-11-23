using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
//using UnityEngine.UIElements;

public class Salut : MonoBehaviour
{
    public float maxSalut = 100;
    public float maxEnergia = 100;
    public float salutActual;
    public float energiaActual;
    public float quantitatSalut; // quants punts en total cura
    public int tempsCura;
    public int tempsGana = 100;
    private Coroutine curantCoroutine;
    private Coroutine energiaCoroutine;
    private Coroutine danyCoroutine;
    public GameObject finestraUI;
    public TMP_Text textSalut;
    public TMP_Text textEnergia;
    public GameObject panel;
    public FirstPersonController playerController;
    public Image sang;
    public Image fam;
    bool b = true;
    void Start()
    {
        Color color = sang.color;
        color.a = 0;
        sang.color = color;
        Color colorf = fam.color;
        colorf.a = 0;
        fam.color = colorf;
        if (energiaCoroutine != null) return;
        energiaCoroutine = StartCoroutine(Energia());
    }
    public void UsarMedecina(float i)
    {
        quantitatSalut = i;
        if (curantCoroutine != null) return;
        curantCoroutine = StartCoroutine(Curar());
    }
    IEnumerator Curar()
    {
        float curat = 0;
        while (curat < quantitatSalut)
        {
            salutActual += 1;
            salutActual = Mathf.Clamp(salutActual, 0, maxSalut); // no supera el màxim de salut
            curat++;
            Debug.Log("vida: " + Mathf.FloorToInt(salutActual));
            yield return new WaitForSeconds(tempsCura);
        }
        curantCoroutine = null;
    }
    public void RebreDany(float dany)
    {
        salutActual -= dany;
        salutActual = Mathf.Clamp(salutActual, 0, maxSalut);
        StartCoroutine(FadeOut());
        Debug.Log("dany: " + dany + " vida: " + Mathf.FloorToInt(salutActual));
    }
    IEnumerator FadeOut()
    {
        float time = 0f;
        Color color = sang.color;
        while (time < 1) //1 segon
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(0.5f , 0, time / 1);//time/1, 1 segon
            sang.color = color;
            yield return null;
        }
    }
    public void PerdreEnergia(float perdua)
    {
        energiaActual -= perdua;
        energiaActual = Mathf.Clamp(energiaActual, 0, maxEnergia);
        Debug.Log("dany: " + perdua + " vida: " + Mathf.FloorToInt(energiaActual));
    }
    IEnumerator Energia()
    {
        float energia = 0;
        while (energia <= 0)
        {
            energiaActual -= 1;
            energiaActual = Mathf.Clamp(energiaActual, 0, maxEnergia);
            energia--;
            Debug.Log("energia: " + energiaActual);
            yield return new WaitForSeconds(tempsGana);
        }
        energiaCoroutine = null;
    }
    private void Update()
    {
        if (energiaActual <= 0 && b == true) // el jugador passa gana i es comença a morir
        {
            b = false;
            if (danyCoroutine == null)
            {
                danyCoroutine = StartCoroutine(Gana());
            }
        }
        if (energiaActual > 0 && b == false)
        {
            b = true;
            if (danyCoroutine != null)
            {
                StopCoroutine(danyCoroutine);
                danyCoroutine = null;
            }
        }
        if (energiaActual < 10)
        {
            StartCoroutine(FadeFam());
        }
        else { 
            StopCoroutine(FadeFam());
            Color color = fam.color;
            color.a = 0;
            fam.color = color;
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            float j = Input.GetAxis("Vertical");
            finestraUI.GetComponent<RectTransform>().anchoredPosition = 
                Vector3.MoveTowards(finestraUI.GetComponent<RectTransform>().anchoredPosition, 
                new Vector3(0, -50, 0), 500 * Time.deltaTime);
            textSalut.text = "Salut: " + Mathf.FloorToInt(salutActual);
            textEnergia.text = "Energia: " + Mathf.FloorToInt(energiaActual);
        }
        else
        {
            finestraUI.GetComponent<RectTransform>().anchoredPosition =
                Vector3.MoveTowards(finestraUI.GetComponent<RectTransform>().anchoredPosition,
                new Vector3(0, -400, 0), 500 * Time.deltaTime);
        }
        if (salutActual <= 0)
        {
            playerController.enabled = false;
            panel.SetActive(true);
        }
    }
    IEnumerator FadeFam()
    {
        float time = 0f;
        Color color = fam.color;
        while (time < 2) //1 segon
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(0, 0.5f, time / 2);//time/1, 1 segon
            fam.color = color;
            yield return null;
        }
    }
    IEnumerator Gana()
    {
        float dany = 0;
        while (dany <= 0)
        {
            salutActual -= 1;
            salutActual = Mathf.Clamp(salutActual, 0, maxSalut);
            dany--;
            Debug.Log("dany: " + salutActual);
            yield return new WaitForSeconds(1);
        }
        danyCoroutine = null;
    }
    public void Menjar(float i)
    {
        energiaActual += i;
        Debug.Log("energia: " + energiaActual);
    }
}