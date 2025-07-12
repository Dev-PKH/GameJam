using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;

public class SpawnPlanet : MonoBehaviour
{
    //public Planet[] planetPrefabs;
    [SerializeField] private SplineContainer splineContainer;

    public Transform spawnPos;

    public float moveSpeed; // 이동속도;

    public List<Planet> plants = new List<Planet>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlanets(int cnt, float distance, float steps)
    {
        StartCoroutine(ISpanwPlanets(cnt, distance, steps));
    }

    public IEnumerator ISpanwPlanets(int cnt, float distance, float steps)
    {
        // 1. 중복 없는 랜덤 인덱스 리스트 생성
        List<int> prefabIndices = new List<int>();
        while (prefabIndices.Count < cnt)
        {
            int rand = Random.Range(0, InGameManager.Instance.planetPrefabs.Length);
            
            if (rand == (int)InGameManager.Instance.curPlanetStatus) continue;
            if(prefabIndices.Count == 2) 
            {
                if (ToyManager.Instance.checkToy[rand]) continue;
            }
            if (!prefabIndices.Contains(rand))
            {
                prefabIndices.Add(rand);
            }
        }

        for (int i = 0; i < cnt; i++)
        {
            //Planet planet = Instantiate(InGameManager.Instance.planetPrefabs[prefabIndices[i]], spawnPos.position, spawnPos.rotation);
            Planet planet = Instantiate(InGameManager.Instance.planetPrefabs[prefabIndices[i]], spawnPos);
            planet.CheckPlanet(prefabIndices[i]);
            Spline spline = splineContainer.Spline;
            Vector3 splinePos;

            if (i == 0)
            {
                planet.SetDistance(Mathf.RoundToInt(distance));
                planet.SetSpriteSize(Vector3.one + new Vector3(0.4f, 0.4f, 0.4f));
                splinePos = spline.EvaluatePosition(0);
                planet.transform.DOMove(splinePos, 0.25f);
            }
            else
            {
                planet.SetDistance(Mathf.RoundToInt(distance + distance * steps * i * (4 - cnt)));
                if (cnt == i + 1)
                {
                    splinePos = spline.EvaluatePosition(1);
                    planet.transform.DOMove(splinePos, 0.25f);
                }
                else
                {
                    planet.SetSpriteSize(Vector3.one + new Vector3(0.2f, 0.2f, 0.2f));
                    splinePos = spline.EvaluatePosition(0.5f);
                    planet.transform.DOMove(splinePos, 0.25f);
                }
            }

            plants.Add(planet);

             yield return new WaitForSeconds(moveSpeed);
        }
    }

    public void ClearList()
    {
        foreach(var planet in plants)
        {
            Destroy(planet.gameObject);
        }

        plants.Clear();
    }
}
