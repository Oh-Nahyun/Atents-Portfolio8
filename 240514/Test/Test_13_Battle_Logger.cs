using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_13_Battle_Logger : TestBase
{
    UserPlayer user;
    EnemyPlayer enemy;

    private void Start()
    {
        GameManager gameManager = GameManager.Instance;

        user = gameManager.UserPlayer;
        enemy = gameManager.EnemyPlayer;

        user.AutoShipDeployment(true);
        enemy.AutoShipDeployment(true);

        gameManager.GameState = GameState.Battle;
        user.Test_BindInputFuncs();
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        user.AutoAttack();
        enemy.AutoAttack();
    }
}
