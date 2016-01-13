using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour 
{
    public LayerMask blockingLayer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    protected bool movementInProgress = false;

	protected virtual void Start ()
    {
	   boxCollider = GetComponent<BoxCollider2D>();
       rb2D = GetComponent<Rigidbody2D>();
	}
	
        protected virtual void AttemptMove(float xDir, float yDir)
    {
        movementInProgress = true;
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);
        
        if(hit.transform == null)
        {
            return;
        }
    }
    
    protected bool Move (float xDir, float yDir, out RaycastHit2D hit)
    {
        
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);
        
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;
        
        if(hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        return false;
    }
   
    protected IEnumerator SmoothMovement (Vector3 end)
    {
        float sqrtRemainingDistance = (transform.position - end).sqrMagnitude;
        while(sqrtRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, (Time.fixedDeltaTime));
            rb2D.MovePosition(newPosition);
            sqrtRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
        movementInProgress = false;
        OnMovementFinished();
    }
    
    protected virtual void  OnMovementFinished(){}
}