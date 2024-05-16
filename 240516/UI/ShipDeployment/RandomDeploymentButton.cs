using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomDeploymentButton : MonoBehaviour
{
    // 아직 배치되지 않은 모든 함선을 자동으로 배치

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        UserPlayer player = GameManager.Instance.UserPlayer;
        if (player.IsAllDeployed)
        {
            // 함선이 전부 배치되어있는 상태이면, 전부 배치 취소해서 새로 배치하게 만들기
            player.UndoAllShipDeployment();
        }
        player.AutoShipDeployment(true);
    }
}
