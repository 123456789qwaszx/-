using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    // 모든 기능을 싹다 끌어오는 GameObject
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.Map.LoadMap(1);

        GameObject player = Managers.Resource.Instantiate("Player/Knight");
        player.name = "Player";
        Managers.Object.Add(player);
    }

    public override void Clear()
    {
        
    }
}
