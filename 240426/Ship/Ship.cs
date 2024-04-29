using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 배의 종류
/// </summary>
public enum ShipType : byte
{
    None = 0,
    Carrier,        // 항공모함 (5칸)
    BattleShip,     // 전함 (4칸)
    Destroyer,      // 구축함 (3칸)
    Submarine,      // 잠수함 (3칸)
    PatrolBoat      // 경비정 (2칸)
}

/// <summary>
/// 배의 방향 (뱃머리가 바라보는 방향)
/// </summary>
public enum ShipDirection : byte
{
    North = 0,
    East,
    South,
    West
}

public class Ship : MonoBehaviour
{
    /// <summary>
    /// 배의 종류
    /// </summary>
    ShipType shipType = ShipType.None;

    /// <summary>
    /// 배 종류 확인 및 설정용 프로퍼티
    /// </summary>
    public ShipType Type
    {
        get => shipType;
        private set
        {
            shipType = value;
            switch (shipType) // 배 종류별로 크기와 이름을 설정한다.
            {
                case ShipType.Carrier:
                    size = 5;
                    shipName = "항공모함";
                    break;
                case ShipType.BattleShip:
                    size = 4;
                    shipName = "전함";
                    break;
                case ShipType.Destroyer:
                    size = 3;
                    shipName = "구축함";
                    break;
                case ShipType.Submarine:
                    size = 3;
                    shipName = "잠수함";
                    break;
                case ShipType.PatrolBoat:
                    size = 2;
                    shipName = "경비정";
                    break;
            }
        }
    }

    /// <summary>
    /// 배의 이름
    /// </summary>
    string shipName = string.Empty;

    /// <summary>
    /// 배 이름 확인용 프로퍼티
    /// </summary>
    public string ShipName => shipName;

    /// <summary>
    /// 배의 크기 (= 최대 HP)
    /// </summary>
    int size = 0;

    /// <summary>
    /// 배 크기 확인용 프로퍼티
    /// </summary>
    public int Size => size;

    /// <summary>
    /// 배의 현재 내구도
    /// </summary>
    int hp = 0;

    /// <summary>
    /////// 배 현재 내구도 확인 및 설정용 프로퍼티
    /// </summary>
    public int HP
    {
        get => hp;
        private set
        {
            hp = value;
            if (hp < 1)         // hp가 0 이하가 되면
            {
                OnSinking();    // 침몰한다.
            }
        }
    }

    /// <summary>
    /// HP가 0보다 크면 살아있다.
    /// </summary>
    bool IsAlive => hp > 0;

    /// <summary>
    /// 배가 바라보는 방향 (북동남서로 회전하는 것이 정방향)
    /// </summary>
    ShipDirection direction = ShipDirection.North;

    public ShipDirection Direction
    {
        get => direction;
        set
        {
            direction = value;
            //modelRoot 회전
        }
    }

    /// <summary>
    /// 배의 모델 메시의 트랜스폼
    /// </summary>
    Transform modelRoot;

    /// <summary>
    /// 배가 배치된 위치 (그리드 좌표)
    /// </summary>
    Vector2Int[] positions;

    /// <summary>
    /// 배가 배치된 위치 확인용 프로퍼티
    /// </summary>
    public Vector2Int[] Positions => positions;

    /// <summary>
    /// 배의 배치 여부 (true면 배치되었고, false면 배치되지 않았다)
    /// </summary>
    bool isDeployed = false;

    /// <summary>
    /// 배의 배치 여부 확인용 프로퍼티
    /// </summary>
    public bool IsDeployed => isDeployed;

    /// <summary>
    /// 배의 머티리얼을 변경하기 위해 찾아 놓은 랜더러
    /// </summary>
    Renderer shipRenderer;

    /// <summary>
    /// 함선이 배치되거나 배치 해제되었을 때를 알리는 델리게이트 (bool : true면 배치되었다. false면 배치 해제되었다.)
    /// </summary>
    public Action<bool> onDeploy;

    /// <summary>
    /// 함선이 공격을 당했을 때를 알리는 델리게이트 (Ship : 자기자신. 이름이나 종류 등에 대한 접근이 필요하기 때문이다.)
    /// </summary>
    public Action<Ship> onHit;

    /// <summary>
    /// 함선이 침몰되었음을 알리는 델리게이트 (Ship : 자기자신.)
    /// </summary>
    public Action<Ship> onSink;

    /// <summary>
    /// 배 초기화용 함수
    /// </summary>
    /// <param name="shipType">배의 종류</param>
    public void Initialize(ShipType shipType)
    {
        Type = shipType;    // 종류 결정
        HP = Size;          // HP 종류에 맞게 설정

        modelRoot = transform.GetChild(0);
        shipRenderer = modelRoot.GetComponentInChildren<Renderer>();

        ResetData();

        gameObject.name = $"{Type}_{Size}";
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 공통적으로 데이터 초기화하는 함수
    /// </summary>
    void ResetData()
    {
        Direction = ShipDirection.North;
        isDeployed = false;
        positions = null;
    }

    /// <summary>
    /// 함선의 머티리얼을 선택하는 함수
    /// </summary>
    /// <param name="isNormal">true면 불투명한 머티리얼, false면 배치 모드용 반투면 머티리얼</param>
    public void SetMaterialType(bool isNormal = true)
    {

    }

    /// <summary>
    /// 함선이 배치되었을 때의 처리를 하는 함수
    /// </summary>
    /// <param name="deployPositions">배치되는 위치들</param>
    public void Deploy(Vector2Int[] deployPositions)
    {

    }

    /// <summary>
    /// 함선이 배치 해제되었을 때의 처리를 하는 함수
    /// </summary>
    public void UnDeploy()
    {

    }

    /// <summary>
    /// 함선을 90도씩 회전시키는 함수
    /// </summary>
    /// <param name="isCW">true면 시계 방향, false면 반시계 방향</param>
    public void Rotate(bool isCW = true)
    {
        if (isCW)
        {
            // isCW가 true이고, 마우스 휠을 아래로 끌어내리는 행동을 취한 경우
            // 배가 시계 방향으로 회전
        }
        else
        {
            // isCW가 false이고, 마우스 휠을 위로 끌어올리는 행동을 취한 경우
            // 배가 반시계 방향으로 회전
        }
    }

    /// <summary>
    /// 함선을 랜덤한 방향으로 회전시키는 함수
    /// </summary>
    public void RandomRotate()
    {

    }

    /// <summary>
    /// 함선이 공격받았을 때 실행되는 함수
    /// </summary>
    public void OnHit()
    {

    }

    /// <summary>
    /// 배가 침몰할 때 실행될 함수
    /// </summary>
    void OnSinking()
    {

    }
}
