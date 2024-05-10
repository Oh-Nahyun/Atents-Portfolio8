using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public enum GameState : byte
{
    Title = 0,      // 타이틀 씬
    ShipDeployment, // 함선 배치 씬
    Battle,         // 전투 씬
    GameEnd         // 전투 씬에서 게임이 끝난 상황
}

[RequireComponent(typeof(TurnController))]
[RequireComponent(typeof(InputController))]
public class GameManager : Singleton<GameManager>
{
    // 게임 상태 -----------------------------------------------------------------

    /// <summary>
    /// 현재 게임 상태
    /// </summary>
    GameState gameState = GameState.Title;

    /// <summary>
    /// 현재 게임 상태를 확인하고 설정하기 위한 프로퍼티
    /// </summary>
    public GameState GameState
    {
        get => gameState;
        set
        {
            if (gameState != value) // 변경이 있을 때만 실행
            {
                gameState = value;
                InputController.ResetBind();            // 기존에 바인딩 되어있던 입력 제거
                onGameStateChange?.Invoke(gameState);   // 게임 상태가 변경되었음을 알림
            }
        }
    }

    /// <summary>
    /// 게임 상태의 변경을 알리는 델리게이트
    /// </summary>
    public Action<GameState> onGameStateChange;

    // 플레이어 ------------------------------------------------------------------

    /// <summary>
    /// 사용자 플레이어 (왼쪽)
    /// </summary>
    UserPlayer user;
    public UserPlayer UserPlayer => user;

    /// <summary>
    /// 적 플레이어 (오른쪽)
    /// </summary>
    EnemyPlayer enemy;
    public EnemyPlayer EnemyPlayer => enemy;

    // 컨트롤러 ------------------------------------------------------------------

    /// <summary>
    /// 턴 컨트롤러
    /// </summary>
    TurnController turnController;
    public TurnController TurnController => turnController;

    /// <summary>
    /// 입력 컨트롤러
    /// </summary>
    InputController inputController;
    public InputController InputController => inputController;

    // 기타 ---------------------------------------------------------------------

    /// <summary>
    /// 테스트 모드인지 아닌지 표시용 변수
    /// </summary>
    public bool IsTestMode = false;

    /// <summary>
    /// 카메라 진동 소스
    /// </summary>
    CinemachineImpulseSource cameraImpulseSource;

    // -------------------------------------------------------------------------

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();

        inputController = GetComponent<InputController>(); // 컨트롤러들 찾기
        turnController = GetComponent<TurnController>();

        cameraImpulseSource = GetComponentInChildren<CinemachineImpulseSource>(); // 컴포넌트 찾기
    }

    protected override void OnInitialize()
    {
        user = FindAnyObjectByType<UserPlayer>();
        enemy = FindAnyObjectByType<EnemyPlayer>();

        turnController.OnInitialize(user, enemy); // 턴 컨트롤러 초기화
    }

    /// <summary>
    /// 카메라를 랜덤한 방향으로 흔드는 함수
    /// </summary>
    /// <param name="force">흔드는 힘의 크기</param>
    public void CameraShake(float force = 1.0f)
    {
        cameraImpulseSource.GenerateImpulseWithVelocity(force * UnityEngine.Random.insideUnitCircle.normalized);
    }
}
