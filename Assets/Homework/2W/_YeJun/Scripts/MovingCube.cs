using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    private Vector3 m_pos;
    
    [SerializeField]
    private float m_frequency;

    void Start()
    {
        m_pos = transform.position;
    }

    void Update()
    {
        transform.position = m_pos + Vector3.right * Mathf.Abs(Mathf.Sin(Time.time * m_frequency)) * 5f;
    }

    private void OnCollision(Collision collision)
    {
        Debug.Log("OnCollision");
        collision.transform.position = transform.position + collision.transform.position;
    }

    private void OnTrigger(Collider other)
    {
        Debug.Log("OnTrigger");
        other.transform.position = transform.position + other.transform.position;
    }

}
