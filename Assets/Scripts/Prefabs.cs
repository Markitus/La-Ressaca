using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour
{
    public Transform player;
    public List<GameObject> prefabs;
    public float distancia = 100;
    void Update()
    {
        foreach (GameObject obj in prefabs)
        {
            float dist = Vector3.Distance(player.position, obj.transform.position);
            if (obj.activeSelf == true && dist > distancia)
            {
                obj.SetActive(false);
            }
            if (obj.activeSelf == false && dist < distancia)
            {
                obj.SetActive(true);
            }
        }
    }
}