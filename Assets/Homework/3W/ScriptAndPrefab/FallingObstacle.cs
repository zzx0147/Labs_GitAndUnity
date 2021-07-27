using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObstacle : MonoBehaviour
{
    private BoxCollider m_DetectTrigger;

    [Tooltip("떨어지는 장애물의 트랜스폼 정보")]
    [SerializeField] private Transform m_ObstacleTransform;

    [Tooltip("장애물이 하강하는 속도, 3이면 3m/s")]
    [SerializeField] private float m_FallingSpeed = 3.0f;

    [Tooltip("장애물이 상승하는 속도, 3이면 2m/s")]
    [SerializeField] private float m_RisingSpeed = 2.0f;

    [Tooltip("장애물이 완전 하강 후 대기시간, 0.7이면 0.7초 대기")]
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
