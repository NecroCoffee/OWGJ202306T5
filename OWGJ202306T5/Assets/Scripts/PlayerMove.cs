using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// �v���C���[�̑���(�v���C���[�ɃA�^�b�`����)
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
    // �Z�[�u�|�C���g
    [SerializeField] GameObject savePoint;          // �Z�[�u�|�C���g�̃v���n�u
    private const string saveTag = "SaveObject";    // �Z�[�u�|�C���g�̃^�O
    private GameObject savePointObj;                // ���������Z�[�u�|�C���g
    private Rigidbody2D savePointRd;                // ���������Z�[�u�|�C���g��RigidBody
    // �p�x
    private bool nowThrow = false;      // �����悤�Ƃ��Ă��邩
    Vector3 startPosition;              // �����n�߂�ʒu
    private Vector3 mousePosition;      // �}�E�X�̍��W
    private Vector3 worldTarget;        // �}�E�X�̃��[���h���W
    private float throwAngle = 0.0f;    // ������p�x
    // ����
    private bool throwing = false; // �����Ă��邩
    private float Travel;               // �����ړ���
    private float speed = 1.0f;         // �������Z�[�u�|�C���g����������
    private float throwPower = 1.0f;    // �����鋭��
    [SerializeField] Vector3 initialVelocity;            // �Z�[�u�|�C���g�ɉ�����


    void Start()
    {
        // �v���C���[�̈ʒu���擾
        playerPos = player.transform.position;  

        // �v���C���[��RigidBody���擾
        playerRb = player.GetComponent<Rigidbody2D>();

        startPosition = savePoint.transform.position;

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

        // ��������������߂�
        if (!nowThrow) mousePosition = Input.mousePosition;

        // ������
        if (Input.GetMouseButtonDown(0)) ThrowPoint();
        if (Input.GetMouseButton(0) ) throwPower += Time.deltaTime + 1.5f;
        if (Input.GetMouseButtonUp(0)) SaveThrow();
        //if (nowThrow) Throwing();

    }

    /*private void FixedUpdate()
    {
        if (throwing)
        {
            Vector3 gravity = new Vector3(0f, -this.gravity, 0f);
            savePointRd.AddForce(initialVelocity, (ForceMode2D)ForceMode.VelocityChange);

            if (savePointObj.transform.position.x <= initialVelocity.x / 2)
            {
                savePointRd.AddForce(gravity, (ForceMode2D)ForceMode.Acceleration);
            }
        }
        
    }*/

    /// <summary>
    /// ���ɂ��Ă�Ƃ��̂݃W�����v�ɂ���
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        jump = false;
    }

    /// <summary>
    /// ������p�x�̌v�Z
    /// </summary>
    private void ThrowPoint()
    {
        // �v���C���[�̈ʒu���Œ�
        playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.constraints = RigidbodyConstraints2D.FreezeAll;

        // �N���b�N�������W���擾
        worldTarget = Camera.main.ScreenToWorldPoint(mousePosition);

        // �w��q�I�u�W�F�N�g�Ɠ����ʒu�ɃZ�[�u�|�C���g�𐶐�
        startPosition = GameObject.FindWithTag(saveTag).transform.position;
        savePointObj = Instantiate(savePoint, startPosition, Quaternion.identity);
        savePointRd = savePointObj.GetComponent<Rigidbody2D>();

        // �p�x���v�Z
        float dx = worldTarget.x - playerPos.x;
        float dy = worldTarget.y - playerPos.y;
        float rad = Mathf.Atan2(dy, dx);
        throwAngle = rad * Mathf.Rad2Deg;  // ���W�A����x�ɒ���

        Debug.Log(throwAngle);

        Travel = 0.0f;
        nowThrow = true;
    }

    /// <summary>
    /// �����鋭���̌v�Z
    /// </summary>
    private void SaveThrow()
    {
        float vx = throwPower * Mathf.Cos(throwAngle);
        float vy = throwPower * Mathf.Sin(throwAngle);
        initialVelocity = new Vector3(vx, vy, 0f);

        savePointRd.bodyType = RigidbodyType2D.Dynamic;
        savePointRd.AddForce(initialVelocity);
        nowThrow = false;
        throwing = true;
        playerRb.constraints = RigidbodyConstraints2D.None;
    }

    /*
    private void Throwing()
    {
        savePointRd.bodyType = RigidbodyType2D.Dynamic;

        // �����ړ��ʂ����߂�
        Travel += speed * Time.deltaTime;

        // �ړI�n�_�܂ł̋��������߂�
        worldTarget.y = playerPos.y;  // ���������킹��
        float distance = Vector3.Distance(startPosition, worldTarget);
        
        // �i�s���������߂�
        var t = Travel * distance;

        if (t < 1.0f) 
        {
            //t��0.5�i�܂蒆�Ԓn�_�j����ǂꂾ������Ă��邩�����߂�
            //���Ԓn�_��0.0�A�o���n��ړI�n��1.0�ƂȂ�悤�Ȓl�ɂ���
            var d = Mathf.Abs(t - 0.5f) * 2.0f;

            //���݂̐����ʒu������...
            var p = Vector3.Lerp(playerPos, worldTarget, t);

            //������񎟊֐��̋Ȑ��ɉ����Ē�����...
            p.y += Mathf.Tan(Mathf.Deg2Rad * throwAngle) * 0.25f * distance * (1.0f - (d * d));

            //�ʒu��ݒ肷��
            savePointObj.transform.position = p;

            return;
        }

        if (t >= 1.0f)
        {
            //t��1.0�ɓ��B������ړ��I���Ƃ���
            savePointObj.transform.position = worldTarget;
            nowThrow = false;
            playerRb.constraints = RigidbodyConstraints2D.None;
        }
        
    }
    */
}
