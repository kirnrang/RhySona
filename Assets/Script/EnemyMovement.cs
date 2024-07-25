using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movedistance = 0.5f;
    public float chaseDistance = 0.75f;
    [SerializeField] private GameObject targetObj = null;
    private Transform targetTransform = null;
    private bool detected = false;
    private GameObject chasingEffect = null;

    private void Start()
    {
        targetTransform = targetObj.transform;
        chasingEffect = transform.Find("chasing").gameObject;
    }



    public void EnemyMove() {
        if (detected)
        {
            chasingEffect.SetActive(true);
            EnemyChase();
        }
        else
        {
            chasingEffect.SetActive(false);
            RandomMove();
        }
        GameOverCheck();
        EnemyDetect();
    }


    public void EnemyDetect()
    {
        if(Vector2.Distance(transform.position, targetTransform.position) < chaseDistance)
        {
            detected = true;
        }
    }


    public void EnemyChase()
    {
        float distX = targetTransform.position.x - this.transform.position.x;
        float distY = targetTransform.position.y - this.transform.position.y;

        if(distX != 0)
        {
            if(distX > 0 )
            {
                EnemyMoveRight();
            }
            else if(distX < 0)
            {
                EnemyMoveLeft();
            }
        }
        else if(distY != 0)
        {
            if (distY > 0)
            {
                EnemyMoveUp(); 
            }
            else if (distY < 0)
            {
                EnemyMoveDown();
            }
        }

    }


    public void RandomMove()
    {
        int a = Random.Range(1, 5);
        switch(a)
        {
            case 1:
                EnemyMoveUp();
                break;
            case 2:
                EnemyMoveDown();
                break;
            case 3:
                EnemyMoveLeft();
                break;
            case 4:
                EnemyMoveRight();
                break;
            default:
                EnemyMoveUp();
                break;

        }
    }


    public void GameOverCheck()
    {
        if(Vector2.Distance(transform.position, targetTransform.position) == 0){
            Debug.Log("Game Over!");
            Time.timeScale = 0f;
        }
    }




    public void EnemyMoveUp()
    {
        Vector2 temp = this.transform.position;
        temp.y += movedistance;
        this.transform.position = temp;
    }

    public void EnemyMoveDown()
    {
        Vector2 temp = this.transform.position;
        temp.y -= movedistance;
        this.transform.position = temp;
    }

    public void EnemyMoveRight()
    {
        Vector2 temp = this.transform.position;
        temp.x += movedistance;
        this.transform.position = temp;
    }

    public void EnemyMoveLeft()
    {
        Vector2 temp = this.transform.position;
        temp.x -= movedistance;
        this.transform.position = temp;
    }

}
