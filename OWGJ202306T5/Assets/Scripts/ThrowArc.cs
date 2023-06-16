using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ThrowArc : MonoBehaviour
{
    [SerializeField] PlayerMove playerMove; // �����x�␶�����W�����R���|�[�l���g

    private bool drawArc = true;            // ��������`�悵�Ă��邩
    private int segmentCount = 60;          // ���������\����������̐�
    private float predictionTime = 6.0f;    // �����������b���v�Z���邩
    private float arkWidth = 0.02f;         // �������̕�
    private LineRenderer[] lineRenderers;   // ���������\������LineRenderer
    [SerializeField] Vector3 startPosition; // �������̊J�n���W

    private Vector3 initialVelocity;    // �����x
    private float throwPower = 1.0f;    // �����鋭��


    // Start is called before the first frame update
    void Start()
    {
        /*
       // LineRenderer��p��
       CreateLineRengererObjects();
       */
    }

    // Update is called once per frame
    void Update()
    {

        /*
        // �����x���X�V
        if (Input.GetMouseButton(0)) throwPower += throwPower * Time.deltaTime;

        if (drawArc)
        {
            // ��������\��
            float timeStep = predictionTime / segmentCount;
            bool draw = false;
            float hitTime = float.MaxValue;
            for (int i = 0; i < segmentCount; i++)
            {
                // ���̍��W���X�V
                float startTime = timeStep * i;
                float endTime = startTime + timeStep;
                SetLineRendererPosition(i, startTime, endTime, !draw);

                // �Փ˔���
                if (!draw)
                {
                    hitTime = GetArcHitTime(startTime, endTime);
                    if (hitTime != float.MaxValue)
                    {
                        draw = true; // �Փ˂����炻�̐�̕������͕\�����Ȃ�
                    }
                }
            }
        }
        else
        {
            // ��������\�����Ȃ�
            for (int i = 0; i < lineRenderers.Length; i++)
            {
                lineRenderers[i].enabled = false;
            }
        }
        */
    }

    /*
    /// <summary>
    /// �w�莞�Ԃɑ΂���A�[�`�̕�������̍��W��Ԃ�
    /// </summary>
    /// <param name="time">�o�ߎ���</param>
    /// <returns>���W</returns>
    private Vector3 GetArcPositionAtTime(float time)
    {
        return (startPosition + ((initialVelocity * time) + (0.5f * time * time) * Physics.gravity));
    }

    /// <summary>
    /// LineRenderer�̍��W���X�V
    /// </summary>
    /// <param name="index"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    private void SetLineRendererPosition(int index, float startTime, float endTime, bool draw = true)
    {
        lineRenderers[index].SetPosition(0, GetArcPositionAtTime(startTime));
        lineRenderers[index].SetPosition(1, GetArcPositionAtTime(endTime));
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

    /// <summary>
    /// 2�_�Ԃ̐����ŏՓ˔��肵�A�Փ˂��鎞�Ԃ�Ԃ�
    /// </summary>
    /// <returns>�Փ˂�������(���ĂȂ��ꍇ��float.MaxValue)</returns>
    private float GetArcHitTime(float startTime, float endTime)
    {
        // Linecast��������̎n�I�_�̍��W
        Vector3 startPosition = GetArcPositionAtTime(startTime);
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
