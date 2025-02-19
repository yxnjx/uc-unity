using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerLogic : MonoBehaviour
{
    public int totalItemCount;
    public int stage;
    public Text stageCountText;
    public Text playerCountText;

    void Awake()
    {
        stageCountText.text = "/ " + totalItemCount; //전체 아이템 개수를 stageCountText에 입력
    }

    public void GetItem(int count)
    {
        playerCountText.text = count.ToString(); //내가 먹은 아이템 개수를 playerCountText에 입력
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(stage);
            //LoadScene의 매개변수는 장면 순서(int)도 가능
        }
        //플레이어 오브젝트가 gameManager boxCollider랑 충돌했을 때(밑으로 떨어졌을 때) 다시 그 stage로 돌아감
    }
}
