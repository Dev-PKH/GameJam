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
    public int distance;

    public Transform spriteTransform;
    public SpriteRenderer spriteRender;
    public SpriteRenderer toyIcon;

    private void OnMouseDown()
    {
        // 일단 Select가 아닐때는 클릭 못하는게 논리적으로는 맞는데 안전하게 할거면
        // 이 함수 실행이 SetPlane이고 Move일때만 하니까 Move가 아니면으로 바꾸는게 실행엔 문제가 없을듯
        if (GameManager.Instance.IsPause || InGameManager.Instance.status != GameStatus.Select) return;
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
