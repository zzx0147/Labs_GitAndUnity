using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class UpDownRoundTripPlatform : MonoBehaviour
{
    private Transform m_Transform = null;
    private ThirdPersonController m_TPCRef = null;

    [Tooltip("왕복하는데 걸리는 시간, 단위는 초, 10이면 갔다가 돌아오는데 걸리는 시간이 10초")]
    [SerializeField]
    private float m_RoundTripTime = 0.0f;
    [SerializeField]
    [Tooltip("왕복하는 거리, 단위는 미터, 10이면 최고점과 최저점의 거리가 10m, 한번 왕복하면 총 움직인 거리는 20m")]
    private float m_RoundTripDistance = 0.0f;

    private Vector3 m_StartPos;
    private Vector3 m_TopPos;
    private Vector3 m_BottomPos;
    private Vector3 m_Speed;

    // Start is called before the first frame update
    void Start()
    {
        m_Transform = transform;
        m_StartPos = m_Transform.position;
        m_StartPos = new Vector3(m_StartPos.x, m_StartPos.y + m_RoundTripDistance / 2.0f, m_StartPos.z);

        m_TopPos = new Vector3(m_Transform.position.x, m_Transform.position.y + m_RoundTripDistance / 2.0f, m_Transform.position.z);
        m_BottomPos = new Vector3(m_Transform.position.x, m_Transform.position.y - m_RoundTripDistance / 2.0f, m_Transform.position.z);

        m_Speed = new Vector3(0.0f,m_RoundTripDistance/m_RoundTripTime,0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        m_Transform.position += (m_Speed * Time.deltaTime);
        if (m_Transform.position.y >= m_TopPos.y)
        {
            m_Speed *= -1;
            m_Transform.position = m_TopPos;
        }
        else if (m_Transform.position.y <= m_BottomPos.y)
        {
            m_Speed *= -1;
            m_Transform.position = m_BottomPos;
        }
    }
}
