using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GoalTileAction : MonoBehaviour, ITileAction
{
    [SerializeField] public GameObject[] enemyList = null;
    [SerializeField] private GameObject menu = null;
    private TilemapRenderer tilemapRenderer = null;

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Vector3Int tilePosition; // 타일 위치

    private CustomTile customTile;

    private void Start()
    {
        TileBase tile = tilemap.GetTile(tilePosition);
        if (tile == null)
        {
            Debug.LogError("타일맵에서 타일을 찾을 수 없습니다. tilePosition이 올바른지 확인하세요.");
            return;
        }
        if (tile is CustomTile)
        {
            customTile = tile as CustomTile;
            customTile.SwitchToSpriteB();
            tilemap.RefreshTile(tilePosition);
        }
    }


    void Update()
    {
        if (ClearCheck())
        {
            // 적이 다 죽으면 골이 열리게 설정.
            customTile.SwitchToSpriteA();
            tilemap.RefreshTile(tilePosition);
        }
    }
    public bool OnPlayerEnter(PlayerControl player)
    {
        if (!ClearCheck())
        {
            return false;
        }
        //클리어 시 작동할 함수 작성
        Time.timeScale = 0;
        Debug.Log("Game Clear!");
        menu.SetActive(true);
        return true;
    }

    public bool OnEnemyEnter(EnemyMovement enemy)
    {
        return true;
    }


    public bool ClearCheck() { 

        for(int i = 0; i < enemyList.Length; i++)
        {
            EnemyMovement em = enemyList[i].GetComponent<EnemyMovement>();
            if (em.isDead == false)
            {
                return false;
            }
        }
        return true;
    }


}

[CreateAssetMenu(fileName = "CustomTile", menuName = "Tiles/CustomTile")]
public class CustomTile : Tile
{
    [SerializeField] private Sprite open_goal;
    [SerializeField] private Sprite close_goal;

    public void SwitchToSpriteA()
    {
        this.sprite = open_goal;
    }

    public void SwitchToSpriteB()
    {
        this.sprite = close_goal;
    }
}


