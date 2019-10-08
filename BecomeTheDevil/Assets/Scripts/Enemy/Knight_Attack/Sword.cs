using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Moving target;
    private KnightAttack parent;
    private Status unitTarget;

    // 사거리 관련.
    private Vector3 unitPosition;   // 검을 휘두를 때 Knight 위치.
    public float distance;  // 검 발사체와 Knight 사이의 거리, 사거리에 사용.
    public float attackRange;

    public Status status;

    // Start is called before the first frame update
    void Start()
    {
        // 지울예정.
        //GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 1);
        //Destroy(this.gameObject, swordRange / swordSpeed);
        status = parent.transform.parent.GetComponent<Status>();
        //Debug.Log(status.attackDMG);
        unitPosition = parent.transform.position;
        attackRange = status.attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(unitTarget);
        if (unitTarget != null)
        {
            MoveToTarget();
        }

        distance = Vector3.Distance(unitPosition, transform.position);
        if (attackRange < distance)    // 사거리보다 길어지면.
        {
            //transform.localScale = new Vector3(1f, 1f, 1f); //크기 초기화.
            //BattleManager.Instance.Pool.ReleaseObject(gameObject);
            //ObjectManager.Call().MemoryDelete(); 재효씨한테 objectmanager 물어보기.
            this.gameObject.SetActive(false);
        }
    }

    public void Initialize(KnightAttack parent)
    {
        // 총알을 쏜 적으로 초기화.
        this.parent = parent;
        this.unitTarget = parent.unitTarget;
        //bulletTime = parent.Range / parent.BulletSpeed;
    }

    private void MoveToTarget()
    {
        if (unitTarget != null /*&& target.IsActive*/)
        {
            transform.position = Vector3.MoveTowards(transform.position, unitTarget.transform.position, Time.deltaTime * status.attackSpeed);
            // 방향이 있는 발사체일 경우 적 방향으로 회전시킴. 
            Vector2 dir = unitTarget.transform.position - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            // Projectile 이 오른쪽으로 향해 있어야 앞이 적쪽으로 향함.
        }

        else if (/*!target.IsActive ||*/ unitTarget == null)  // 타겟이 죽으면 발사체 없앰.
        {
            //BattleManager.Instance.Pool.ReleaseObject(gameObject);
            //Destroy(this.gameObject);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<Status>().TakeDamage(status.attackDMG);
            //Destroy(this.gameObject); //자기 자신을 지웁니다.
            this.gameObject.SetActive(false);
            Debug.Log("플레이어 공격");
        }
    }
}
