using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �O���\�����̕`��
/// ���R�R��
/// </summary>

public class ThrowArc : MonoBehaviour
{
    [SerializeField] PlayerMove playerMove; // �����x�␶�����W�����R���|�[�l���g

    
    private bool drawArc = true;            // ��������`�悵�Ă��邩
    private int segmentCount = 60;          // ���������\����������̐�
    private float predictionTime = 6.0f;    // �����������b���v�Z���邩
    private float arkWidth = 0.02f;         // �������̕�
    private LineRenderer[] lineRenderers;   // ���������\������LineRenderer
    private Vector3 initialVelocity;        // �����x
    [SerializeField] Vector3 startPosition;
    Vector3 endPosition;

    LineRenderer lineRenderer;
    List<Vector3> renderLinePoints = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        
        // LineRenderer��p��
        CreateLineRengererObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        // ��������\��
        float timeStep = predictionTime / segmentCount;
        bool draw = false;
        float hitTime = float.MaxValue;
        if (playerMove.nowThrow && drawArc)
        {
            startPosition = playerMove.savePointRd.position;
            for (int i = 0; i < segmentCount; i++)
            {
                // ���̍��W���X�V
                float startTime = timeStep * i;
                float endTime = startTime + timeStep;
                SetLineRendererPosition(i, startTime, endTime, !draw);

                /*
                // �Փ˔���
                if (!draw)
                {
                    hitTime = GetArcHitTime(startTime, endTime);
                    if (hitTime != float.MaxValue)
                    {
                        draw = true; // �Փ˂����炻�̐�̕������͕\�����Ȃ�
                    }
                }*/
            }//for
        }
        else
        {
            // �������ƃ}�[�J�[��\�����Ȃ�
            for (int i = 0; i < lineRenderers.Length; i++)
            {
                lineRenderers[i].enabled = false;
            }
        }//if-else
        
    }

    /// <summary>
    /// t�b��̕��̂̈ʒu���v�Z
    /// </summary>
    /// <param name="time">t�b��</param>
    /// <param name="startPosition">���W�̊J�n�ʒu</param>
    /// <returns>���W</returns>
    Vector3 CalcPositionFromForce(float time, Vector3 startPosition)
    {
        float mass = 1f;
        Vector3 force = playerMove.angle * playerMove.throwPower;
        Vector3 gravity = new Vector2(0f, -9.8f);

        Vector3 speed = (force / mass) * Time.fixedDeltaTime;
        Vector3 position = (speed * time) + (gravity * 0.5f * Mathf.Pow(time, 2));

        return startPosition + position;
    }

    /// <summary>
    /// LineRenderer�̍��W���X�V
    /// </summary>
    /// <param name="index"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    private void SetLineRendererPosition(int index, float startTime, float endTime, bool draw = true)
    {

        if (index == 0) 
        {
            startPosition = CalcPositionFromForce(startTime, startPosition);
            lineRenderers[index].SetPosition(0, startPosition);
            endPosition = CalcPositionFromForce(endTime, startPosition);
            lineRenderers[index].SetPosition(1, endPosition);
            lineRenderers[index].enabled = draw;
            return;
        }

        startPosition = endPosition;
        endPosition = CalcPositionFromForce(startTime, startPosition);
        lineRenderers[index].SetPosition(0, endPosition);
        lineRenderers[index].SetPosition(1, CalcPositionFromForce(endTime, endPosition));
        lineRenderers[index].enabled = draw;
    }

    /// <summary>
    /// �������`��p��LineRenderer���쐬
    /// </summary>
    private void CreateLineRengererObjects()
    {
        // �e���쐬
        GameObject arcObjectsParent = new GameObject("ArcObject");
        lineRenderers = new LineRenderer[segmentCount];
        // LineRenderer�����q���쐬
        for (int i = 0; i < segmentCount; i++)
        {
            GameObject newObject = new GameObject("LineRenderer_" + i);
            newObject.transform.SetParent(arcObjectsParent.transform);
            lineRenderers[i] = newObject.AddComponent<LineRenderer>();

            // ���̕�
            lineRenderers[i].startWidth = arkWidth;
            lineRenderers[i].endWidth = arkWidth;
            lineRenderers[i].numCapVertices = 5;    // �p���ۂ�����
            lineRenderers[i].enabled = false;
        }
    }

    /*
    /// <summary>
    /// 2�_�Ԃ̐����ŏՓ˔��肵�A�Փ˂��鎞�Ԃ�Ԃ�
    /// </summary>
    /// <returns>�Փ˂�������(���ĂȂ��ꍇ��float.MaxValue)</returns>
    private float GetArcHitTime(float startTime, float endTime)
    {
        // Linecast��������̎n�I�_�̍��W
        Vector3 startPosition = CalcPositionFromForce(startTime);
        Vector3 endPosition = GetArcPositionAtTime(endTime);

        // �Փ˔���
        RaycastHit hitInfo;
        if (Physics.Linecast(startPosition, endPosition, out hitInfo))
        {
            // �Փ˂���Collider�܂ł̋���������ۂ̏Փˎ��Ԃ��Z�o
            float distance = Vector3.Distance(startPosition, endPosition);
            return startTime + (endTime - startTime) * (hitInfo.distance / distance);
        }
        return float.MaxValue;
    }
    */

}
