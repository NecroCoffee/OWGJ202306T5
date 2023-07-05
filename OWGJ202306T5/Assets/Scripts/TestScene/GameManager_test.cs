using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager_test : MonoBehaviour
{
    [Header("�I�u�W�F�N�g�p�ϐ�")]
    [SerializeField] private GameObject life_TextObject;//lifetext�I�u�W�F�N�g�ۑ��p
    private TextMeshProUGUI life_Text;
    [SerializeField] private GameObject player_Prefab;
    private GameObject save_Prefab;

    [SerializeField] PlayerMove playerMove;
    [SerializeField] SavePointStop savePointStop;

    [Header("�Ǘ��p�ϐ�")]
    public int player_Life = 15;//�v���C���[�c�@

    public Vector3 save_tmp;//�Z�[�u�I�u�W�F�N�g���W�ۑ��p

    public bool Is_saveObjectActive = false;//�Z�[�u�W�F�N�g�͗L�����ǂ���

    private Vector2 startPointPos;//�X�^�[�g���v���C���[�����ʒu



    [SerializeField] private float m_resetPlayerPos_y = 0.25f;//���Z�b�g�����܂荞�ݖh�~�p

    public bool Is_checkPointIsActive;//�`�F�b�N�|�C���g�͗L�����ǂ���

    public Vector2 currentCheckPointPos;//�`�F�b�N�|�C���g���W�ۑ��p

    public bool Is_canSaveObjectGenerate;//�Z�[�u�I�u�W�F�N�g�����۔���

    public bool Is_goal = false;//�S�[������

    public GameObject player { get; private set; };


    private void Player_Generate_Start()//�J�n���v���C���[����
    {
        player = Instantiate(player_Prefab, startPointPos, Quaternion.identity);
        
    }

    private void Player_Action_Reset()//���Z�b�g����
    {
        if (Is_saveObjectActive == true)
        {
            Debug.Log(save_tmp);
            //save_tmp = savePointStop.savePosition;
            GameObject.FindWithTag("Player").transform.position = new Vector3(save_tmp.x, save_tmp.y + m_resetPlayerPos_y);
            Is_canSaveObjectGenerate = true;
            Is_saveObjectActive = false;
            save_Prefab = playerMove.savePointObj;
            player_Life--;
            life_Text.text = player_Life.ToString();
            Destroy(save_Prefab);
        }
        else if (Is_saveObjectActive == false && Is_checkPointIsActive == true)
        {
            GameObject.FindWithTag("Player").transform.position = new Vector3(currentCheckPointPos.x, currentCheckPointPos.y + m_resetPlayerPos_y);
        }
        else if (Is_saveObjectActive == false && Is_checkPointIsActive == false)
        {
            GameObject.FindWithTag("Player").transform.position = new Vector3(startPointPos.x, startPointPos.y + m_resetPlayerPos_y);
        }
    }

    private void Find_Pos_CurrentSaveObject()//�Z�[�u�I�u�W�F�N�g�T��
    {
        if (save_Prefab == null)
        {
            save_Prefab = GameObject.FindWithTag("SaveObject");
        }
        else if (save_Prefab != null)
        {
            save_tmp = GameObject.FindWithTag("SaveObject").transform.position;//�Z�[�u�I�u�W�F�N�g�̈ʒu���擾�G
        }
    }

    private void Return_To_Point()//Point�^�O�I�u�W�F�N�g�ɖ߂�
    {
        if (Input.GetKeyDown("r"))
        {
            if (Is_saveObjectActive == true)
            {
                GameObject.FindWithTag("Player").transform.position = new Vector2(currentCheckPointPos.x, currentCheckPointPos.y);
                Debug.Log("Return to checkpoint!");
            }
            else if(Is_saveObjectActive == false)
            {
                GameObject.FindWithTag("Player").transform.position = new Vector2(startPointPos.x,startPointPos.y);
                Debug.Log("CheckPoint isnt active. Return to startpoint!");
            }
        }
    }

    private void Awake()
    {
        
        Is_canSaveObjectGenerate = true;
        startPointPos = GameObject.FindWithTag("StartPoint").transform.position;//�X�^�[�g�ʒu�擾

        save_tmp = startPointPos;//�����n�_���[�h

        life_Text = life_TextObject.GetComponent<TextMeshProUGUI>();
        life_Text.text = player_Life.ToString();
    }

    private void Start()
    {
        Player_Generate_Start();
    }
    

    private void Update()
    {

        //Find_Pos_CurrentSaveObject();
        //Debug.Log("curSavePos.x" + save_tmp.x);
        //Debug.Log("curSavePos.y" + save_tmp.y);

        if (Is_goal == true)//�����ɃS�[�����̏���������
        {
            Debug.Log("Goal!!");
        }

        Find_Pos_CurrentSaveObject();
        
        if (Input.GetMouseButtonDown(1))
        {
            Player_Action_Reset();
        }

    }
}
