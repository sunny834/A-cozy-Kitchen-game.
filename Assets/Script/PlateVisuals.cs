using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateVisuals : MonoBehaviour
{
    [SerializeField] private Transform CounterTopPoint;
    [SerializeField] private Transform PlateVisualPrefrab;
    [SerializeField] private PlateCounter plateCounter;

    private List<GameObject> PlateVisualsList;

    private void Awake()
    {
       PlateVisualsList = new List<GameObject>();
    }
    private void Start()
    {
        plateCounter.OnPlateSpawn += PlateCounter_OnPlateSpawn;
        plateCounter.OnPlateRemove += PlateCounter_OnPlateRemove;
    }

    private void PlateCounter_OnPlateRemove(object sender, System.EventArgs e)
    {
        GameObject PlateGameObject = PlateVisualsList[PlateVisualsList.Count-1];
        PlateVisualsList.Remove(PlateGameObject);
        Destroy(PlateGameObject);
    }

    private void PlateCounter_OnPlateSpawn(object sender, System.EventArgs e)
    {
        //Debug.Log("sunny");
        Transform PlateVisualTransform = Instantiate(PlateVisualPrefrab,CounterTopPoint);
        float offset = .1f;
        PlateVisualTransform.localPosition = new Vector3(0,offset*PlateVisualsList.Count,0);
        PlateVisualsList.Add(PlateVisualTransform.gameObject);
    }
}
