using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    // 플레이어가 트리거에 들어왔을 때 실행되는 함수
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 트리거에 들어온 오브젝트가 "Player" 태그를 가지고 있는지 확인
        if (other.CompareTag("Player"))
        {
            Debug.Log("다음층으로 이동했습니다.");
            Managers.Map.LoadMap(2);
        }
    }

}