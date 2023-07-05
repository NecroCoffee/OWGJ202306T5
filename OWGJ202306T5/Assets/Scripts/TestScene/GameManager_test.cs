using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager_test : MonoBehaviour
{
    [Header("オブジェクト用変数")]
    [SerializeField] private GameObject life_TextObject;//lifetextオブジェクト保存用
    private TextMeshProUGUI life_Text;
    [SerializeField] private GameObject player_Prefab;
    private GameObject save_Prefab;

    [SerializeField] PlayerMove playerMove;
    [SerializeField] SavePointStop savePointStop;

    [Header("管理用変数")]
    public int player_Life = 15;//プレイヤー残機

    public Vector3 save_tmp;//セーブオブジェクト座標保存用

    public bool Is_saveObjectActive = false;//セーブジェクトは有効かどうか

    private Vector2 startPointPos;//スタート時プレイヤー生成位置



    [SerializeField] private float m_resetPlayerPos_y = 0.25f;//リセット時埋まり込み防止用

    public bool Is_checkPointIsActive;//チェックポイントは有効かどうか

    public Vector2 currentCheckPointPos;//チェックポイント座標保存用

    public bool Is_canSaveObjectGenerate;//セーブオブジェクト生成可否判定

    public bool Is_goal = false;//ゴール判定

    public GameObject player { get; private set; };


    private void Player_Generate_Start()//開始時プレイヤー生成
    {
        player = Instantiate(player_Prefab, startPointPos, Quaternion.identity);
        
    }

    private void Player_Action_Reset()//リセット処理
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

    private void Find_Pos_CurrentSaveObject()//セーブオブジェクト探索
    {
        if (save_Prefab == null)
        {
            save_Prefab = GameObject.FindWithTag("SaveObject");
        }
        else if (save_Prefab != null)
        {
            save_tmp = GameObject.FindWithTag("SaveObject").transform.position;//セーブオブジェクトの位置を取得；
        }
    }

    private void Return_To_Point()//Pointタグオブジェクトに戻る
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
        startPointPos = GameObject.FindWithTag("StartPoint").transform.position;//スタート位置取得

        save_tmp = startPointPos;//初期地点ロード

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

        if (Is_goal == true)//ここにゴール時の処理を入れる
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
