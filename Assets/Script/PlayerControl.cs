using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // 노트 판정용으로 둔 movable.
    public bool movable = false;
    //사망 판정용 변수
    public bool isDead = false;
    //적에게 신호 전달용 변수
    public bool playerMoved = false;
    public float moveDistance = 1.0f;
    public Vector3 targetPosition;
    [SerializeField] public GameObject[] enemyList = null;
    public GameObject sonaPrefab = null;
    private GameObject sona = null;


    private void Start()
    {
        isDead = false ;
        movable = false ;
        playerMoved = false ;
    }

    private void Update()
    {
        //적에게 잡히는 즉시 게임오버가 뜨기 위해 update에 게임오버 함수 위치시킴
        if (isDead)
        {
            //게임 오버 효과를 위한 임시 코드
            Debug.Log("Game Over!");
            Time.timeScale = 0.0f;
            this.gameObject.SetActive(false);
        }
    }

    public void MoveUp() => Move(Vector3.up);
    public void MoveDown() => Move(Vector3.down);
    public void MoveRight() => Move(Vector3.right);
    public void MoveLeft() => Move(Vector3.left);


    public void Move(Vector3 direction)
    {
        if(sona !=null)
        {
            Destroy(sona);
        }
        Vector3 pos = this.transform.position + direction * moveDistance;
        if (movable && HandleMove(pos))
        {
            this.transform.position = pos;
            movable = false;
            sona = Instantiate(sonaPrefab, this.transform.position, Quaternion.identity);
        }
        playerMoved = true ;
        
    }

    private bool HandleMove(Vector3 targetPosition)
    {
        Vector2 currentPosition2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 targetPosition2D = new Vector2(targetPosition.x, targetPosition.y);
        Vector2 direction = targetPosition2D - currentPosition2D;
        float distance = direction.magnitude;

        int layerMask = LayerMask.GetMask("Obstacle", "Trap", "Goal", "Enemy");

        // 레이캐스트를 사용하여 이동하려는 위치에 충돌이 있는지 검사
        RaycastHit2D hit = Physics2D.Raycast(currentPosition2D, direction.normalized, distance, layerMask);

        // 디버그 로그 추가
        //Debug.Log($"Raycast from {currentPosition2D} to {targetPosition2D}, Direction: {direction.normalized}, Distance: {distance}, LayerMask: {layerMask}");

        if (hit.collider != null)
        {
            //Debug.Log( $"Hit Collider: {hit.collider.gameObject.name}, Position: {hit.collider.transform.position}");
            ITileAction tileAction = hit.collider.GetComponent<ITileAction>();
            if (tileAction != null)
            {
                //이동할 방향에 있는 오브젝트가 트랩이라면
                if (hit.collider.gameObject.layer == 8)
                {
                    isDead = true;
                }
                //... 골이라면
                else if (hit.collider.gameObject.layer == 7) 
                {
                    
                }
                //... 적이라면
                else if(hit.collider.gameObject.layer== 9)
                {
                    StunEnemy(targetPosition);
                    return false;
                }

                return tileAction.OnPlayerEnter(this);
                
            }
            else
            {
                // 플레이어가 이동할 수 없는 경우 == obstacle
                Debug.Log(" Movement blocked by: " + hit.collider.gameObject.name);
                return false;
            }
        }
        else
        {
            // 충돌이 감지되지 않았을 때만 이동
            transform.position = targetPosition;
            return true;
        }
    }


    public void StunEnemy(Vector3 targrtPos)
    {
        for(int i = 0; enemyList.Length > i; i++)
        {
            if (enemyList[i].transform.position == targrtPos)
            {
                EnemyMovement em = enemyList[i].GetComponent<EnemyMovement>();
                em.isStunned = true;
            }
        }
    }

    public bool gamecheck() { return isDead; }
}
