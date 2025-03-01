using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    public float jumpPower;
    public int itemCount;
    public GameManagerLogic manager;

    Rigidbody rigid;
    bool isJump;
    AudioSource audio;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        isJump = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJump) //스페이스바 눌렀을 때 && isJump가 true일 때
        {
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
            isJump = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 vec = new Vector3(h, 0, v);
        rigid.AddForce(vec, ForceMode.Impulse);

        //rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision) //무한으로 점프하는 걸 막음
    {
        if (collision.gameObject.tag == "Floor")
            isJump = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            itemCount++; //모은 아이템 개수 올라감
            audio.Play(); //아이템 먹으면 소리남
            other.gameObject.SetActive(false);
            manager.GetItem(itemCount);
        }
        else if(other.tag == "Finish") //Finish 태그를 가진 오브젝트와 충돌했을 때
        {
            if(itemCount == manager.totalItemCount)
            {
                //게임 끝(내가 모은 아이템 수와 전체 아이템 수가 같으면) or 다음 스테이지
                if (manager.stage == 2) 
                    SceneManager.LoadScene(0); //stage2이면 다시 처음 stage로
                else
                    SceneManager.LoadScene(manager.stage + 1); //stage2가 아니면 다음 스테이지로
            }
            else //아이템을 다 모으지 못하고 다음 stage로 넘어가면 다시 원래 stage로 돌아감
            {
                //게임 다시 시작
                SceneManager.LoadScene(manager.stage);

                //SceneManager: 장면 관리하는 기본 클래스
                //LoadScene: 주어진 장면 불러오는 함수
                //Scene 불러 오려면 꼭 File -> Build Setting에서 추가
            }
        }
    }
}
