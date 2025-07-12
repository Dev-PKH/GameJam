using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public Image Panel;
    float time = 0f;
    float F_time = 1f;

    public void FadeIn()
    {
        StartCoroutine(FadeInFlow());
    }
    public void FadeOut()
    {
        StartCoroutine(FadeOutFlow());
    }

    IEnumerator FadeInFlow()
    {
        time = 0f;
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        // ���̵� �� ���� ��
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        yield return null; 
    }

    IEnumerator FadeOutFlow()
    {
        time = 0f;
        Color alpha = Panel.color;
        // ���̵� �ƿ� ���� ��
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }
        // ���̵� �ƿ� �Ϸ�
        Panel.gameObject.SetActive(false);
        yield return null;
    }
}
