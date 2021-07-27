using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearPlatform : MonoBehaviour
{
    [Tooltip("��� â ������Ʈ�� �θ� ������Ʈ,SpearRoot ����")]
    [SerializeField] private Transform m_SpearRoot = null;

    [Tooltip("â�� Ƣ����� �ֱ�, ������ ��, 5�ʶ�� �ϸ� â�� ������ ���� ���� 5�� �ڿ� �ٽ� Ƣ���")]
    [SerializeField] private float m_TermTime = 5.0f;

    [Tooltip("â�� ������ �ö���µ� �ɸ��� �ð�, ������ ��")]
    [SerializeField] private float m_UpTime = 2.0f;

    [Tooltip("â�� ������ �������µ� �ɸ��� �ð�, ������ ��")]
    [SerializeField] private float m_DownTime = 2.0f;

    private float m_StartPosY;
    private float m_EndPosY = 0;
    void Start()
    {
        m_StartPosY = m_SpearRoot.localPosition.y;
        StartCoroutine(WaitingCoroutine());
    }

    private IEnumerator WaitingCoroutine()
    {
        Debug.Log("WaitingStart");
        yield return new WaitForSeconds(m_TermTime);
        Debug.Log("WaitingEnd");
        StartCoroutine(UpCoroutine());
    }

    private IEnumerator UpCoroutine()
    {
        Debug.Log("UpStart");
        float currentTime = 0.0f;
        float currentPosY = 0.0f;
        Debug.Log(m_StartPosY + " : " + m_EndPosY);
        while(true)
        {
            yield return null;
            currentTime += Time.deltaTime;

            currentPosY = Mathf.Lerp(m_StartPosY,m_EndPosY,currentTime / m_UpTime);
            m_SpearRoot.localPosition = new Vector3(m_SpearRoot.localPosition.x,currentPosY, m_SpearRoot.localPosition.z);
            if(currentTime >= m_UpTime)
            {
                break;
            }
        }

        StartCoroutine(DownCoroutine());
    }
    private IEnumerator DownCoroutine()
    {
        float currentTime = 0.0f;
        float currentPosY = 0.0f;

        while (true)
        {
            yield return null;
            currentTime += Time.deltaTime;
            currentPosY = Mathf.Lerp(m_EndPosY, m_StartPosY, currentTime / m_DownTime);
            m_SpearRoot.localPosition = new Vector3(m_SpearRoot.localPosition.x, currentPosY, m_SpearRoot.localPosition.z);
            if (currentTime >= m_DownTime)
            {
                break;
            }
        }
        StartCoroutine(WaitingCoroutine());
    }
}
