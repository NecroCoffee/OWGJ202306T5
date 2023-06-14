using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �O���̌v�Z(�v)
/// ���R�R��
/// </summary>
public class ThrowPoint : MonoBehaviour
{
    /*[SerializeField] GameObject player;         // �v���C���[�̃I�u�W�F�N�g
    Vector3 playerPos;                          // �v���C���[�̈ʒu
    //GameObject savePoint;
    Rigidbody2D savePointRd;
    //Vector3 initialVelocity;
    // ������֘A
    //private float throwPower = 0.0f;    // �����鋭��
    private float throwAngle = 0.0f;         // ������p�x
    [SerializeField] GameObject savePointPre;  // �Z�[�u�|�C���g�̃v���n�u

    private bool throwSamePoint = false;        // �Z�[�u�|�C���g�𓊂��Ă��邩

    [SerializeField] LineRenderer simuLine = null; // �\�������O��
    Vector3 gravity = new Vector3(0f, -9.8f, 0f);   // �d��

    // test
    [SerializeField] GameObject test;
    private Vector3 rote;
    private int playerPosRenderer = 0;
    private int targetPosRenderer = 1;
    private Vector3 targetPos;


    // Update is called once per frame
    void Update()
    {
        // �O�������߂�
        if (throwPower <= 500f && Input.GetMouseButton(0)) throwPower += 5.0f;
        if (Input.GetMouseButtonDown(0)) StartCoroutine(Simulation());

        // �Z�[�u�|�C���g�𓊂���
        if (Input.GetMouseButtonUp(0)) SaveThrow();

        if (throwSamePoint)
        {
            savePointRd.AddForce(gravity, (ForceMode2D)ForceMode.Acceleration);
            savePointRd.AddForce(initialVelocity, (ForceMode2D)ForceMode.VelocityChange);
        }
    }

    /// <summary>
    /// �O���̗\��
    /// </summary>
    /// <returns></returns>
    private IEnumerator Simulation()
    {
        // ����L�[���I�t�ɂ���

        // �v���C���[�̌��݈ʒu���擾
        playerPos = player.transform.position;
        // �^�����~�����������ׂ��Ȃ���
        Physics.autoSimulation = false;
        // �Z�[�u�|�C���g�𐶐�
        Vector3 savePointPlace = new Vector3(playerPos.x + 1, playerPos.y, playerPos.z);
        savePoint = Instantiate(savePointPre, savePointPlace, Quaternion.identity);
        // �\���|�C���g���i�[���郊�X�g
        //List<Vector3> points = new List<Vector3> { savePointRd.transform.position };

        // �p�x������
        Vector3 from = playerPos;
        Vector3 to = Input.mousePosition;
        Vector3 planeNormal = Vector3.up;   // �����
        Vector3 planeFrom = Vector3.ProjectOnPlane(from,planeNormal);
        Vector3 planeTo = Vector3.ProjectOnPlane(to,planeNormal);
        throwAngle = Vector3.SignedAngle(planeFrom, planeTo, planeNormal);

        float vx = throwPower * Mathf.Cos(throwAngle * Mathf.Deg2Rad);
        float vy = throwPower * Mathf.Sin(throwAngle * Mathf.Deg2Rad);
        initialVelocity = new Vector3(vx, vy, 0);

        
        // �O�����V�~�����[�V����
        for (int i = 0; i < 16; i++)
        {
            Physics.Simulate(Time.deltaTime);
            points.Add(savePointRd.position);
        }
        // ���̈ʒu��
        savePointRd.velocity = Vector3.zero;
        savePointRd.transform.position = playerPos;
        // �\���n�_���Ȃ�
        simuLine.positionCount = points.Count;
        simuLine.SetPositions(points.ToArray());
        

        Physics.autoSimulation = true;

        yield return new WaitForFixedUpdate();
    }

    /// <summary>
    /// �Z�[�u�|�C���g�𓊂��鏈��
    /// </summary>
    private void SaveThrow()
    {
        throwSamePoint = true;

        // test
        playerPos = new Vector3(0, 0, 0);
        targetPos = new Vector3(2, 0, 0);
        rote = new Vector3(0, 0, throwPower);

        // �Ə��\��
        simuLine.SetPosition(playerPosRenderer, playerPos);
        simuLine.SetPosition(targetPosRenderer, targetPos);

        throwPower = 0;  // ������
    }*/
}
