using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public enum Planets
{
    Monalpterra,
    Dibetund,
    Trigmaer,
    None
}

public class Planet : MonoBehaviour
{
    //public // tmp

    public TextMeshPro tmp;

    public Planets planetStatus;
    public int distance;

    public Transform spriteTransform;

    private void Awake()
    {
       
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        spriteTransform.DOScale(Vector3.one, 0.1f);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        InGameManager.Instance.SetPlanet(this);
    }

    public void SetDistance(int dis)
    {
        distance = dis;
        tmp.text = distance.ToString();
    }

    public void SetSpriteSize(Vector3 vec)
    {
        spriteTransform.DOScale(vec, 0.25f);
    }
}
