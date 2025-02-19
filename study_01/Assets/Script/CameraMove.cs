using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform;
    Vector3 Offset;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Offset = transform.position - playerTransform.position;
    }

    //UI ������Ʈ, ī�޶� ������Ʈ, ī�޶� �̵� ���� ���⿡�� �̷����
    //Update���� ������ �� �ϰ� �ڿ� ���� �ٴ� �ű� ������,,,
    void LateUpdate()
    {
        transform.position = playerTransform.position + Offset;
    }
}
