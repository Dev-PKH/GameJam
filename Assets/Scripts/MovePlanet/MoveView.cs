using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;

public class MoveView : MonoBehaviour
{
    public List<Planet> planetList = new List<Planet>();
    public Transform spawnPoint;
    public SplineContainer splineContainer;

    private void OnEnable()
    {
        
    }

    void Start()
    {
        
    }
     
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlanetSpawn(int distance, int planetIndex, int posIndex)
    {
        //Planet planet = Instantiate(InGameManager.Instance.planetPrefabs[planetIndex], spawnPoint.position, spawnPoint.rotation);
        Planet planet = Instantiate(InGameManager.Instance.planetPrefabs[planetIndex], spawnPoint);
        planet.SetDistance(distance);
        planet.OffIcon();

        Spline spline = splineContainer.Spline;
        var midLocal = splineContainer.Spline[posIndex].Position;
        var midWorld = splineContainer.transform.TransformPoint(midLocal);
        planet.transform.DOMove(midWorld, 0.25f);


        planetList.Add(planet);
    }

    public void ClearList()
    {
        foreach (var planet in planetList)
        {
            Destroy(planet.gameObject);
        }

        planetList.Clear();
    }
}
