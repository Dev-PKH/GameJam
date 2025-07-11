using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;

public class SpawnPlanet : MonoBehaviour
{
    public Planet[] planetPrefabs;
    [SerializeField] private SplineContainer splineContainer;

    public float moveSpeed; // 이동속도;

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
        for (int i = 0; i < cnt; i++)
        {
            Planet planet = Instantiate(planetPrefabs[i], transform.position, transform.rotation);
            Spline spline = splineContainer.Spline;
            Vector3 splinePos;

            if (i == 0)
            {
                planet.distance = Mathf.RoundToInt(distance);
                planet.transform.DOScale(Vector3.one + new Vector3(0.2f, 0.2f, 0.2f), 0.25f);
                splinePos = spline.EvaluatePosition(0);
                planet.transform.DOMove(splinePos, 0.25f);
            }
            else
            {
                planet.distance = Mathf.RoundToInt(distance * steps * i);
                if (cnt != i - 1)
                {
                    planet.transform.DOScale(Vector3.one + new Vector3(0.1f, 0.1f, 0.1f), 0.25f);
                    splinePos = spline.EvaluatePosition(0.5f);
                    planet.transform.DOMove(splinePos, 0.25f);
                }
                else
                {
                    splinePos = spline.EvaluatePosition(1);
                    planet.transform.DOMove(splinePos, 0.25f);
                }
            }

           

            yield return new WaitForSeconds(moveSpeed);
        }
    }
}
