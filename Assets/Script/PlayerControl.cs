using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // 노트 판정용으로 둔 movable.
    public bool movable = false;
    public float moveDistance = 1.0f;
    public Vector3 targetPosition;
    [SerializeField] private GameObject Enemy = null;
    EnemyMovement enemyCtr = null;

    private void Start()
    {
        enemyCtr = Enemy.GetComponent<EnemyMovement>();
    }

    public void MoveUp() => Move(Vector3.up);
    public void MoveDown() => Move(Vector3.down);
    public void MoveRight() => Move(Vector3.right);
    public void MoveLeft() => Move(Vector3.left);


    public void Move(Vector3 direction)
    {
        Vector3 pos = this.transform.position + direction * moveDistance;
        if (movable && HandleMove(pos))
        {
            this.transform.position = pos;
            movable = false;
        }
        enemyCtr.EnemyMove();
    }

    private bool HandleMove(Vector3 targetPosition)
    {
        Vector2 currentPosition2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 targetPosition2D = new Vector2(targetPosition.x, targetPosition.y);
        Vector2 direction = targetPosition2D - currentPosition2D;
        float distance = direction.magnitude;

        int layerMask = LayerMask.GetMask("Obstacle", "Trap", "Goal");

        // 레이캐스트를 사용하여 이동하려는 위치에 충돌이 있는지 검사
        RaycastHit2D hit = Physics2D.Raycast(currentPosition2D, direction.normalized, distance, layerMask);

        // 디버그 로그 추가
        Debug.Log($"Raycast from {currentPosition2D} to {targetPosition2D}, Direction: {direction.normalized}, Distance: {distance}, LayerMask: {layerMask}");

        if (hit.collider != null)
        {
            Debug.Log( $"Hit Collider: {hit.collider.gameObject.name}, Position: {hit.collider.transform.position}");
            ITileAction tileAction = hit.collider.GetComponent<ITileAction>();
            if (tileAction != null)
            {
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


}
