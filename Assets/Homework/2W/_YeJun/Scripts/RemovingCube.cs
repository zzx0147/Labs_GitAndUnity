using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingCube : MonoBehaviour
{
    private Coroutine m_co;

  

    // Update is called once per frame
    void Update()
    {
        if (Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.up,Quaternion.identity, 0.25f))
        {
            if (m_co == null)
            {
                m_co = StartCoroutine(Fall());
            }
        }
    }

    IEnumerator Fall()
    {
        Vector3 originPos = transform.position;
        float time = 0f;

        while (time <= 1.2f)
        {
            transform.position = new Vector3(originPos.x + Random.Range(0.1f, 0.3f), originPos.y, originPos.z + Random.Range(0.1f, 0.3f));

            time += Time.deltaTime;
            yield return null;
        }

        transform.position += Vector3.right * 1000f;

        yield return new WaitForSeconds(2.0f);

    }

    private void OnDrawGizmos()
    {
       //Gizmos.DrawSphere
    }
}
