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
        if (Input.GetButtonDown("Jump") && !isJump) //�����̽��� ������ �� && isJump�� true�� ��
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

    void OnCollisionEnter(Collision collision) //�������� �����ϴ� �� ����
    {
        if (collision.gameObject.tag == "Floor")
            isJump = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            itemCount++; //���� ������ ���� �ö�
            audio.Play(); //������ ������ �Ҹ���
            other.gameObject.SetActive(false);
            manager.GetItem(itemCount);
        }
        else if(other.tag == "Finish") //Finish �±׸� ���� ������Ʈ�� �浹���� ��
        {
            if(itemCount == manager.totalItemCount)
            {
                //���� ��(���� ���� ������ ���� ��ü ������ ���� ������) or ���� ��������
                if (manager.stage == 2) 
                    SceneManager.LoadScene(0); //stage2�̸� �ٽ� ó�� stage��
                else
                    SceneManager.LoadScene(manager.stage + 1); //stage2�� �ƴϸ� ���� ����������
            }
            else //�������� �� ������ ���ϰ� ���� stage�� �Ѿ�� �ٽ� ���� stage�� ���ư�
            {
                //���� �ٽ� ����
                SceneManager.LoadScene(manager.stage);

                //SceneManager: ��� �����ϴ� �⺻ Ŭ����
                //LoadScene: �־��� ��� �ҷ����� �Լ�
                //Scene �ҷ� ������ �� File -> Build Setting���� �߰�
            }
        }
    }
}
