using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_04_ShipMovement : TestBase
{
    public Board board;
    public Ship ship;

    protected override void OnEnable()
    {
        base.OnEnable();
        inputActions.Test.MouseMove.performed += OnMouseMove;
        inputActions.Test.MouseWheel.performed += OnMouseWheel;

    }

    protected override void OnDisable()
    {
        inputActions.Test.MouseWheel.performed -= OnMouseWheel;
        inputActions.Test.MouseMove.performed -= OnMouseMove;
        base.OnDisable();
    }

    private void Start()
    {
        ship.Initialize(ShipType.Carrier);
        ship.gameObject.SetActive(true);
    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
        if (ship != null)
        {
            // Debug.Log(context.ReadValue<Vector2>());
            Vector2Int grid = board.GetMouseGridPosition();
            Vector3 world = board.GridToWorld(grid);
            ship.transform.position = world;
        }
    }

    private void OnMouseWheel(InputAction.CallbackContext context)
    {
        if (ship != null)
        {
            // Debug.Log(context.ReadValue<float>());
            float wheel = context.ReadValue<float>();
            if (wheel > 0)
                ship.Rotate(false);
            else
                ship.Rotate(true);
        }
    }
}

/// 실습_240426
/// 1. ship을 그리드 단위로 움직이기 (칸 단위로 움직이기)
/// 2. 마우스 휠을 이용해서 ship을 돌리기 (Ship.Rotate 구현하기)
/// ----------------------------------------------------
//if (isCW)
//{
//    // isCW가 true이고, 마우스 휠을 아래로 끌어내리는 행동을 취한 경우
//    // 배가 시계 방향으로 회전
//}
//else
//{
//    // isCW가 false이고, 마우스 휠을 위로 끌어올리는 행동을 취한 경우
//    // 배가 반시계 방향으로 회전
//}
