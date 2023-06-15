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
    [SerializeField] private GameObject save_Prefab;

    [Header("�Ǘ��p�ϐ�")]
    public int player_Life = 15;//�v���C���[�c�@
    private Vector2 save_tmp;//�Z�[�u�I�u�W�F�N�g�ʒu�ۑ��p
    public bool Is_saveObjectActive=true;//�Z�[�u�I�u�W�F�N�g�͗L�����ǂ���
    private Vector2 startPointPos;//�X�^�[�g���v���C���[�����ʒu
    //[SerializeField] bool Is_canPlayerGenerate;//�v���C���[�����۔���
    private Vector2 currentCheckPointPos;

    private void Player_Generate_Start()//�J�n���v���C���[����
    {
        Instantiate(player_Prefab, startPointPos, Quaternion.identity);
        //Is_canPlayerGenerate = false;
    }

    private void Player_Action_Reset()//���Z�b�g����
    {
          
            Instantiate(player_Prefab, save_tmp, Quaternion.identity);
            Destroy(save_Prefab);
        
    }

    private void Find_Pos_CurrentSaveObject()
    {
        if (save_Prefab == null)
        {
            save_Prefab = GameObject.FindWithTag("SaveObject");
        }
        else if(save_Prefab!=null)
        {
            save_tmp = GameObject.FindWithTag("SaveObject").transform.position;//�Z�[�u�I�u�W�F�N�g�̈ʒu���擾�G
        }
        else if (save_tmp==null&&currentCheckPointPos!=null)
        {
            save_tmp = GameObject.FindWithTag("CheckPoint").transform.position;//���O�̃Z�[�u�|�C���g���擾
        }
        else
        {
            save_tmp = GameObject.FindWithTag("StartPoint").transform.position;//�Z�[�u�|�C���g��null and�`�F�b�N�|�C���g��null���̓X�^�[�g�n�_�̒l������B
        }
    }

    private void Awake()
    {
        startPointPos = GameObject.FindWithTag("StartPoint").transform.position;//�X�^�[�g�ʒu�擾
        life_Text = life_TextObject.GetComponent<TextMeshProUGUI>();
        life_Text.text = player_Life.ToString();
    }

    private void Start()
    {
        Player_Generate_Start();
    }
    

    private void Update()
    {

        Find_Pos_CurrentSaveObject();
        
        
        if (Input.GetMouseButtonDown(1))
        {
            Player_Action_Reset();
        }

    }
}
