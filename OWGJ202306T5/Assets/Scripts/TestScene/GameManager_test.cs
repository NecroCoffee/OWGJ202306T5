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
    [SerializeField] private GameObject save_Prefab;

    [Header("管理用変数")]
    public int player_Life = 15;//プレイヤー残機
    private Vector2 save_tmp;//セーブオブジェクト位置保存用
    public bool Is_saveObjectActive=true;//セーブオブジェクトは有効かどうか
    private Vector2 startPointPos;//スタート時プレイヤー生成位置
    //[SerializeField] bool Is_canPlayerGenerate;//プレイヤー生成可否判定
    private Vector2 currentCheckPointPos;

    private void Player_Generate_Start()//開始時プレイヤー生成
    {
        Instantiate(player_Prefab, startPointPos, Quaternion.identity);
        //Is_canPlayerGenerate = false;
    }

    private void Player_Action_Reset()//リセット処理
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
            save_tmp = GameObject.FindWithTag("SaveObject").transform.position;//セーブオブジェクトの位置を取得；
        }
        else if (save_tmp==null&&currentCheckPointPos!=null)
        {
            save_tmp = GameObject.FindWithTag("CheckPoint").transform.position;//直前のセーブポイントを取得
        }
        else
        {
            save_tmp = GameObject.FindWithTag("StartPoint").transform.position;//セーブポイントがnull andチェックポイントがnull時はスタート地点の値を入れる。
        }
    }

    private void Awake()
    {
        startPointPos = GameObject.FindWithTag("StartPoint").transform.position;//スタート位置取得
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
