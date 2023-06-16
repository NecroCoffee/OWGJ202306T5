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

    [SerializeField]private Vector2 save_tmp;//�Z�[�u�I�u�W�F�N�g���W�ۑ��p

    public bool Is_saveObjectActive=false;//�Z�[�u�I�u�W�F�N�g�͗L�����ǂ���

    private Vector2 startPointPos;//�X�^�[�g���v���C���[�����ʒu











    
    public Vector2 currentCheckPointPos;//�`�F�b�N�|�C���g���W�ۑ��p

    public bool Is_canSaveObjectGenerate;//�Z�[�u�I�u�W�F�N�g�����۔���

    private void Player_Generate_Start()//�J�n���v���C���[����
    {
        Instantiate(player_Prefab, startPointPos, Quaternion.identity);
        
    }

    private void Player_Action_Reset()//���Z�b�g����
    {

        GameObject.FindWithTag("Player").transform.position = new Vector3(save_tmp.x, save_tmp.y+0.25f);
        Is_canSaveObjectGenerate = true;
        Destroy(save_Prefab);
        
    }

    private void Find_Pos_CurrentSaveObject()
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
        //Debug.Log("curSavePos.x" + save_tmp.x);
        //Debug.Log("curSavePos.y" + save_tmp.y);
        
        
        if (Input.GetMouseButtonDown(1))
        {
            Player_Action_Reset();
        }

    }
}
