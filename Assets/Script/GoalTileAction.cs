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
    [SerializeField] private Vector3Int tilePosition; // Ÿ�� ��ġ

    private CustomTile customTile;

    private void Start()
    {
        TileBase tile = tilemap.GetTile(tilePosition);
        if (tile == null)
        {
            Debug.LogError("Ÿ�ϸʿ��� Ÿ���� ã�� �� �����ϴ�. tilePosition�� �ùٸ��� Ȯ���ϼ���.");
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
            // ���� �� ������ ���� ������ ����.
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
        //Ŭ���� �� �۵��� �Լ� �ۼ�
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


