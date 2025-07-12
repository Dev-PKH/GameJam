using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public enum Planets
{
    // Prefix : Mon-    Di-     Tri-    Tet-
    // Suffix : -terra  -undine -aeris  -pyra
    Monterra,
    Monundine,
    Monaeris,
    Monpyra,
    Diterra,
    Diuundine,
    Diaeris,
    Dipyra,
    Triterra,
    Triundine,
    Triaeris,
    Tripyra,
    Tetterra,
    Tetundine,
    Tetaeris,
    Tetpyra,
    None
}

public class Planet : MonoBehaviour
{
    //public // tmp

    public TextMeshPro tmp;

    public Planets planetStatus;
    public Toys planetToys;
    public int distance;

    public Transform spriteTransform;
    public SpriteRenderer spriteRender;
    public SpriteRenderer toyIcon;

    private void Awake()
    {
       
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {

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
        if (GameManager.Instance.IsPause) return;
        InGameManager.Instance.SetPlanet(this);
    }

    public void SetDistance(int dis)
    {
        distance = dis;
        tmp.text = distance.ToString();
    }

    public void CheckPlanet(int index)
    {
        toyIcon.gameObject.SetActive(true);
        toyIcon.sprite = ToyManager.Instance.toys[index];

        if (ToyManager.Instance.checkToy[index])
        {
            toyIcon.color = Color.white;
        }
        else
        {
            toyIcon.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
    }

    public void OffIcon()
    {
        toyIcon.gameObject.SetActive(false);
    }

    public void SetSpriteSize(Vector3 vec)
    {
        spriteTransform?.DOScale(vec, 0.25f);
    }

    // 행성이 선택됬을 때
    public void SetPlanet(Planet planet)
    {
        planet.planetStatus = planetStatus;
        SetDistance(planet.distance);
        SetSpriteSize(new Vector3(1.2f, 1.2f, 1.2f));
    }
}
