using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DiceChance : MonoBehaviour
{
    public int chanceIndex;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateExistence()
    {
        if(!gameObject.activeSelf)
        {
            return;
        }

        // 남은 갯수 표시가 올바른지 확인하고 다른 경우 변화시킴
        if (chanceIndex <= InGameManager.Instance.curRollCnt)
        {
            // 아직은 보이는 중
            StartCoroutine(ChanceActivate());
        }
        else
        {
            // 보이지 말아야 함
            StartCoroutine(ChanceDeactivate());
        }
    }

    private IEnumerator ChanceActivate()
    {
        float time = 0f;
        Vector3 scale = gameObject.transform.localScale;
        if (scale.y > 0.5f )// Already Activated
        {
            yield break;
        }

        while (time < 0.3f)
        {
            float t = time / 0.3f;
            scale.y = Mathf.Lerp(0, 1, t);
            gameObject.transform.localScale = scale;
            time += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator ChanceDeactivate()
    {
        float time = 0f;
        Vector3 scale = gameObject.transform.localScale;
        if (scale.y < 0.5f) // Already Deactivated
        {
            yield break;
        }

        while (time < 0.3f)
        {
            float t = time / 0.3f;
            scale.y = Mathf.Lerp(1, 0, t);
            gameObject.transform.localScale = scale;
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
