using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObstacle : MonoBehaviour
{
    private BoxCollider m_DetectTrigger;

    [Tooltip("�������� ��ֹ��� Ʈ������ ����")]
    [SerializeField] private Transform m_ObstacleTransform;

    [Tooltip("��ֹ��� �ϰ��ϴ� �ӵ�, 3�̸� 3m/s")]
    [SerializeField] private float m_FallingSpeed = 3.0f;

    [Tooltip("��ֹ��� ����ϴ� �ӵ�, 3�̸� 2m/s")]
    [SerializeField] private float m_RisingSpeed = 2.0f;

    [Tooltip("��ֹ��� ���� �ϰ� �� ���ð�, 0.7�̸� 0.7�� ���")]
    [SerializeField] private float m_WaitingTime = 0.7f;

    private float m_ObstacleStartPosY = 0.0f;
    void Start()
    {
        m_DetectTrigger = GetComponent<BoxCollider>();
        m_ObstacleStartPosY = m_ObstacleTransform.localPosition.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<CharacterController>() != null)
        {
            m_DetectTrigger.enabled = false;
            StartCoroutine(StartFallingCoroutine());
        }
    }

    IEnumerator StartFallingCoroutine()
    {
        float currentObstaclePosY = m_ObstacleStartPosY;
        while(true)
        {
            currentObstaclePosY -= m_FallingSpeed * Time.deltaTime;
            m_ObstacleTransform.localPosition = new Vector3(m_ObstacleTransform.localPosition.x, currentObstaclePosY, m_ObstacleTransform.localPosition.z);
            if(currentObstaclePosY <= 1.0f)
            {
                m_ObstacleTransform.localPosition = new Vector3(m_ObstacleTransform.localPosition.x, 1.0f, m_ObstacleTransform.localPosition.z);
                StartCoroutine(StartWaitingCoroutine());
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator StartRisingCoroutine()
    {
        float currentObstaclePosY = 1.0f;
        while (true)
        {
            currentObstaclePosY += m_RisingSpeed * Time.deltaTime;
            m_ObstacleTransform.localPosition = new Vector3(m_ObstacleTransform.localPosition.x, currentObstaclePosY, m_ObstacleTransform.localPosition.z);
            if (currentObstaclePosY >= m_ObstacleStartPosY)
            {
                m_ObstacleTransform.localPosition = new Vector3(m_ObstacleTransform.localPosition.x, m_ObstacleStartPosY, m_ObstacleTransform.localPosition.z);
                m_DetectTrigger.enabled = true;
                yield break;
            }
            yield return null;
        }
    }
    IEnumerator StartWaitingCoroutine()
    {
        yield return new WaitForSeconds(m_WaitingTime);
        StartCoroutine(StartRisingCoroutine());
    }
}
