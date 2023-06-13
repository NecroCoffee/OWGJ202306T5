using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̑���
/// ���R�R��
/// </summary>
public class PlayerMove : MonoBehaviour
{
    // �ړ�
    [SerializeField] GameObject player;          // �v���C���[�̃I�u�W�F�N�g
    [SerializeField] Vector3 playerPos;          // �v���C���[�̈ʒu
    [SerializeField] float amoMove = 1.0f;      // �ړ���
    private float xPos;

    // �W�����v
    private Rigidbody2D playerCollider;          // �v���C���[�̓����蔻��
    private float jumpForce = 300f;              // �W�����v��
    private bool jump = false;                   // �W�����v�����ǂ���

    // ������֘A
    private float throwPower = 0.0f;    // �����鋭��
    [SerializeField] LineRenderer lineRenderer;
    private int playerPosRenderer = 0;
    private int targetPosRenderer = 1;
    private Vector3 targetPos;

    // test
    [SerializeField] GameObject test;
    private Vector3 rote;

    // Start is called before the first frame update
    void Start()
    {
        // �v���C���[�̈ʒu���擾
        // SerializeField�ɂ��ăC���X�y�N�^�[����w�肵�Ă��������A�F�X�������Ă�ԂɎQ�Ƃ��O���\��������
        playerPos = player.GetComponent<Transform>().position;  

        // �v���C���[�̃R���C�_�[���擾
        playerCollider = player.GetComponent<Rigidbody2D>();

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
            playerCollider.AddForce(transform.up * jumpForce);
        }

        // �p�x�����߂�
        if (Input.GetMouseButton(0)) {
            throwPower += Time.deltaTime;
            Debug.Log(throwPower);
        }

        // �Z�[�u�|�C���g�𓊂���
        if (Input.GetMouseButtonUp(0)) SaveThrow();

    }

    /// <summary>
    /// ���ɂ��Ă�Ƃ��̂݃W�����v��
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        jump = false;
    }

    /// <summary>
    /// �Z�[�u�|�C���g�𓊂��鏈��
    /// </summary>
    void SaveThrow()
    {
        // test
        playerPos = new Vector3(0, 0, 0);
        targetPos = new Vector3(2, 0, 0);
        rote = new Vector3(0, 0, throwPower);

        // �Ə��\��
        lineRenderer.SetPosition(playerPosRenderer, playerPos);
        lineRenderer.SetPosition(targetPosRenderer, targetPos);

        // ��������������߂�
        //targetPos = new Vector3(playerPos.x + 2, playerPos.y + throwPower, 0);

        throwPower = 0;  // ������
    }

}
