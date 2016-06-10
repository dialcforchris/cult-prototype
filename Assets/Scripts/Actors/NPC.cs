using UnityEngine;
using System.Collections;
using DG.Tweening;

public class NPC : Actor
{
    #region Finding player variables
    public Transform target;
    Ray2D rayCast;
    RaycastHit2D hit = new RaycastHit2D();
    Vector2 targetEnemy = Vector2.zero;
    bool foundEnemy = false;
    float lookCounter = 5;
    float currentLookCounter = 0;
    #endregion

    AIBehaviour behaviour;
    int maxReloads = 3;
    int currentReloads = 0;
    float reloadCounter = 3;
    float currentReloadCounter = 0;
    public Transform targetPos;
    Vector2 lookHere;
    Vector2 lastSighted = Vector2.zero;
   
    #region pathfinding vars
    public Transform[] targets;
    int targetsIndex = 0;
    Vector2[] path;
    int targetIndex=0;
    bool finishedPath = true;
    bool gotPath;
    #endregion

    // Use this for initialization
	public override void Awake () 
    {
        base.Awake();
        behaviour = AIBehaviour.MOVING;
      //  player = Player.instance;
        currentWeapon = shooter;
        SetHealth(50);
        //targetEnemy = player.gameObject.transform.position;
        speed = 5;

	}
	
	// Update is called once per frame
    public override void Update()
    {
        Debug.Log("last sighted " + lastSighted + " player pos " + Player.instance.transform.position);
      //  Debug.Log(behaviour);
        if (ReturnState() == ActorState.ALIVE)
        {
            base.Update();
            Movement();
            PlayerFoundCoolDown();
            HitEnemyCheck(RayCastSearch());
            ///  Attack(currentWeapon.name);
            BehaviourSwitch(behaviour);
        }
       if (ReturnState() == ActorState.DEAD)
       {
           GetComponent<CircleCollider2D>().enabled = false;
       }
       
    }
   
    protected override void Movement()
    {
        //rotation
        float lookAngle = Mathf.Atan2((transform.position.y - lookHere.y), (transform.position.x - lookHere.x)) * Mathf.Rad2Deg;
        Quaternion newRot = new Quaternion();
        newRot.eulerAngles = new Vector3(0, 0, lookAngle+90 );
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 360 * ((speed * 100) * Time.deltaTime));

        //// move to target
        Vector2 move = Vector2.zero;
        Vector2 pos = Vector2.zero;
        //     if (Vector2.Distance(transform.position, targetEnemy) > 5||foundEnemy&&currentLookCounter>0)
        //          {
        //               move = Vector2.MoveTowards(transform.position, targetEnemy, Time.deltaTime);
        //               pos = transform.position;                
        //              transform.position = move;
        //          }
             animator.SetBool("Walking", pos - move != Vector2.zero ? true : false);
            
            
    }
    //void MoveDownPath()
    //{
    //    Vector2 currenWaypoint = path[0];
    //    while (true)
    //    {
    //        if (Vector2.Distance(transform.position, currenWaypoint) < 0.02f)
    //        {
    //            targetIndex++;
    //            if (targetIndex >= path.Length)
    //            {
    //                targetIndex = 0;
    //                finishedPath = true;
    //            }
    //            currenWaypoint = path[targetIndex];
    //        }
    //        transform.position = Vector2.MoveTowards(transform.position, currenWaypoint, speed * Time.deltaTime);
    //    }
    //}

  protected virtual void FindEnemy()
    {
        hit = Physics2D.Raycast(transform.position, transform.up,10);
        if (hit)
        {
            if (hit.collider.gameObject.tag != gameObject.tag && hit.collider.gameObject.GetComponent<Actor>())
            {
                foundEnemy = true;
                currentLookCounter = 0;
                lastSighted = hit.point;
                behaviour = AIBehaviour.ATTACK;
                StopCoroutine("FollowPath");
                lookHere = lastSighted;
            }

            else
            {
                if (foundEnemy)
                {
                    targetEnemy = lastSighted;
                    PlayerFoundCoolDown();

                }

            }
        }
        Debug.DrawRay(transform.position,transform.up*10,Color.red,Time.deltaTime,true);
    }
  public virtual RaycastHit2D RayCastSearch()
  {
      RaycastHit2D ht = Physics2D.Raycast(transform.position, transform.up, 10);
      Debug.DrawRay(transform.position, transform.up * 10, Color.red);
      return ht;
  }
    void HitEnemyCheck(RaycastHit2D _hit)
  {
        if (_hit)
        {
            if (_hit.collider.gameObject.tag != gameObject.tag && _hit.collider.gameObject.GetComponent<Actor>())
            {
                foundEnemy = true;
                currentLookCounter = 0;
                lastSighted = _hit.point;
                targetEnemy = _hit.point;
                behaviour = AIBehaviour.ATTACK;
                StopCoroutine("FollowPath");
            }
            else
            {
                if (foundEnemy)
                {
                    targetEnemy = lastSighted;
                    lookHere = lastSighted;
                    PlayerFoundCoolDown();
                }
            }
        }
  }

  public override void Attack(string type)
  {
      if (behaviour == AIBehaviour.ATTACK)
      {
          if (foundEnemy)
          {
              if (Vector2.Distance(transform.position, targetEnemy) < 2)
              {
                  base.Attack(type);
                  if (currentWeapon.GetAmmo() <= 1)
                  {
                      currentWeapon.Reload();
                  }
              }
              else
              {
                  transform.position = Vector2.MoveTowards(transform.position, targetEnemy, speed * Time.deltaTime);
              }
          }
      }
  }
  void PlayerFoundCoolDown()
  {
      if (currentLookCounter < lookCounter)
      {
          currentLookCounter += Time.deltaTime;
      }
      else
      {
          foundEnemy = false;
          if (behaviour == AIBehaviour.ATTACK)
          {
              RecalculatePath();
              behaviour = AIBehaviour.MOVING;
          }
      }
  }
    #region pathfinding
 
    void SelectTarget()
 {
     if (finishedPath) 
     {
         StopCoroutine("FollowPath");
         PathRequestManager.RequestPath(transform.position, targets[targetsIndex].position, OnPathFound);
         finishedPath = false;
         targetsIndex++;
         if (targetsIndex >= targets.Length-1)
         {
             targetsIndex = 0;
         }
     }
 }

    private void OnPathFound(Vector2[] newPath, bool pathSuccess)
    {
        if (pathSuccess)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    void RecalculatePath()
    {
        targetIndex = 0;
        finishedPath = true;
        PathRequestManager.RequestPath(transform.position, targets[targetsIndex].position, OnPathFound);
    }

    IEnumerator FollowPath()
    {
        Vector2 currentWaypoint = path[targetIndex];
        while (behaviour==AIBehaviour.MOVING)
        {
            if (path != null)
            {
                lookHere = path[targetIndex];
                if (Vector2.Distance(transform.position, currentWaypoint) < 0.02f)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length-1)
                    {
                        targetIndex = 0;
                        finishedPath = true;
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length-1; i++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawCube(path[i], Vector3.one);
                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
    #endregion

    void BehaviourSwitch(AIBehaviour b)
    {
        switch(b)
        {
            case AIBehaviour.MOVING:
                {
                    SelectTarget();
                  //  FindEnemy();
                    break;
                }
            case AIBehaviour.ATTACK:
                {
                    if (foundEnemy) 
                    {
                        if (RayCastSearch() && RayCastSearch().collider.gameObject.tag == "Cult")
                        Attack(currentWeapon.name);
                    }
                    break;
                }
            case AIBehaviour.FRIENDLY:
                {
                    break;
                }
            case AIBehaviour.SEARCHING:
                {
                    break;
                }
            case AIBehaviour.STANDING:
                {
                    break;
                }
        }
    }
    
}
public enum AIBehaviour
{
    ATTACK,
    FRIENDLY,
    SEARCHING,
    MOVING,
    STANDING,
    DEAD,
    
}