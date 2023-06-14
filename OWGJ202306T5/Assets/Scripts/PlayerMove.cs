using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// �v���C���[�̑���
/// ���R�R��
/// </summary>
public class PlayerMove : MonoBehaviour
{
    // �ړ�
    [SerializeField] GameObject player;        // �v���C���[�̃I�u�W�F�N�g
    private Vector3 playerPos;                 // �v���C���[�̈ʒu
    [SerializeField] float amoMove = 1.0f;     // �ړ���
    [SerializeField] float gravity = 9.8f;     // �d��
    private float xPos;

    // �W�����v
    private Rigidbody2D playerRb;          // �v���C���[�̓����蔻��
    private float jumpForce = 350f;        // �W�����v��
    private bool jump = false;             // �W�����v�����ǂ���

    // ������
    [SerializeField] GameObject savePoint;  // �Z�[�u�|�C���g�̃v���n�u
    [SerializeField] Vector3 startPosition;    // �����n�߂�ʒu
    private float throwPower = 1.0f;        // �����鋭��
    Vector3 initialVelocity;                // �����x
    private float throwAngle = 0.0f;        // ������p�x
    private float speed = 1.0f;             // ��������

    void Start()
    {
        // �v���C���[�̈ʒu���擾
        // SerializeField�ɂ��ăC���X�y�N�^�[����w�肵�Ă��������A�F�X�������Ă�ԂɎQ�Ƃ��O���\��������
        playerPos = player.transform.position;  

        // �v���C���[�̃R���C�_�[���擾
        playerRb = player.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        // ���E�ړ�
        xPos = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector3 (xPos, 0) * amoMove * Time.deltaTime);

        // �W�����v
        if (!jump && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            playerRb.AddForce(transform.up * jumpForce);
        }
        if (jump) playerRb.AddForce(new Vector3(0, gravity, 0), (ForceMode2D)ForceMode.Force);

        // ������
        if (Input.GetMouseButton(0)) 
        {
            // �p�x���珉���x������
            Vector3 from = playerPos;
            Vector3 to = Input.mousePosition;
            Vector3 planeNormal = Vector3.up;   // �����
            Vector3 planeFrom = Vector3.ProjectOnPlane(from, planeNormal);
            Vector3 planeTo = Vector3.ProjectOnPlane(to, planeNormal);
            throwAngle = Vector3.SignedAngle(planeFrom, planeTo, planeNormal);
            float vx = throwPower * Mathf.Cos(throwAngle * Mathf.Deg2Rad);
            float vy = throwPower * Mathf.Sin(throwAngle * Mathf.Deg2Rad);
            initialVelocity = new Vector3(vx, vy, 0);
        }

        if (Input.GetMouseButtonUp(0))
        {
            GameObject obj = Instantiate(savePoint, startPosition, Quaternion.identity);
            Rigidbody2D rd = obj.GetComponent<Rigidbody2D>();
            rd.AddForce(initialVelocity, (ForceMode2D)ForceMode.Impulse);
        }
    }

    /// <summary>
    /// ���ɂ��Ă�Ƃ��̂݃W�����v�ɂ���
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        jump = false;
    }

}
