using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class UpDownRoundTripPlatform : MonoBehaviour
{
    private Transform m_Transform = null;
    private ThirdPersonController m_TPCRef = null;

    [Tooltip("�պ��ϴµ� �ɸ��� �ð�, ������ ��, 10�̸� ���ٰ� ���ƿ��µ� �ɸ��� �ð��� 10��")]
    [SerializeField]
    private float m_RoundTripTime = 0.0f;
    [SerializeField]
    [Tooltip("�պ��ϴ� �Ÿ�, ������ ����, 10�̸� �ְ����� �������� �Ÿ��� 10m, �ѹ� �պ��ϸ� �� ������ �Ÿ��� 20m")]
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
