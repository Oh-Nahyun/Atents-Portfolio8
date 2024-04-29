using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_05_ShipDeployment : Test_04_ShipMovement
{
    Ship[] testShips;

    Ship TargetShip
    {
        get => ship;
        set
        {
            ship = value;
            ship?.gameObject.SetActive(true);
        }
    }

    public Material ShipNormal;
    public Material ShipDeployMode;

    private void Start()
    {
        testShips = new Ship[ShipManager.Instance.ShipTypeCount];
        testShips[(int)ShipType.Carrier - 1] = ShipManager.Instance.MakeShip(ShipType.Carrier, transform);
        testShips[(int)ShipType.BattleShip - 1] = ShipManager.Instance.MakeShip(ShipType.BattleShip, transform);
        testShips[(int)ShipType.Destroyer - 1] = ShipManager.Instance.MakeShip(ShipType.Destroyer, transform);
        testShips[(int)ShipType.Submarine - 1] = ShipManager.Instance.MakeShip(ShipType.Submarine, transform);
        testShips[(int)ShipType.PatrolBoat - 1] = ShipManager.Instance.MakeShip(ShipType.PatrolBoat, transform);
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        TargetShip = testShips[(int)ShipType.Carrier - 1];
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        TargetShip = testShips[(int)ShipType.BattleShip - 1];
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        TargetShip = testShips[(int)ShipType.Destroyer - 1];
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        TargetShip = testShips[(int)ShipType.Submarine - 1];
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {
        TargetShip = testShips[(int)ShipType.PatrolBoat - 1];
    }

    protected override void OnTestLClick(InputAction.CallbackContext context)
    {
        Vector2Int grid = board.GetMouseGridPosition();
        if (TargetShip != null && board.ShipDeployment(TargetShip, grid))
        {
            Debug.Log($"배치 성공 : {TargetShip.gameObject.name}");
            TargetShip = null;
        }
        else
        {
            Debug.Log("배치 실패");
        }
    }

    protected override void OnTestRClick(InputAction.CallbackContext context)
    {
        Vector2 grid = board.GetMouseGridPosition();
        ShipType shipType = board.GetShipTypeOnBoard(grid);
        if (shipType != ShipType.None)
        {
            Ship ship = testShips[(int)shipType - 1];
            board.UndoShipDeployment(ship);
        }
    }
}

/// 실습_240429
/// Test_05_ShipDeployment 구현하기
/// Board_ShipDeployment & IsShipDeploymentAvailable 구현하기
/// ----------------------------------------------------------------------------
/// [목표]
/// 1. 1 ~ 5번 단축키로 함선 선택 (선택된 배는 활성화해서 보인다.)
/// 2. 보드에 좌클릭을 하면 선택된 함선이 배치
/// 3. 함선이 있는 부분을 우클릭하면 배가 배치 해제가 된다. (해제된 배는 비활성화해서 안보인다.)
/// ----------------------------------------------------------------------------
/// 4. 머티리얼 변경하기 (선택되었을 때 배치 가능한 지역이면 반투명한 녹색, 불가능한 지역이면 반투명한 빨간색, 배치 완료되었으면 Normal)
/// 5. 선택이 변경되었을 때 배치 안된 함선은 다시 안보이게 만들기
