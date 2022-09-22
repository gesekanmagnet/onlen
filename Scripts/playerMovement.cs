using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float Speed = 8f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private CompositeCollider2D playerCollider;
    [SerializeField] private BoxCollider2D blocker;
    private float horizontal;
    private float vertical;
    public PhotonView photonView;
    public Transform hitBox;
    public float hitRange = 0.5f;
    public LayerMask target;
    public int attackDamage = 20;
    
    // Start is called before the first frame update
    void Start()
    {
        // mencegah player saling mendorong
        Physics2D.IgnoreCollision(playerCollider, blocker, true);
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(horizontal * Speed, vertical * Speed);
            //hadap kiri, hadap kanan
            if (!Mathf.Approximately(0, horizontal))
            {
                transform.rotation = -horizontal > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            }

            if(Input.GetKeyDown(KeyCode.D))
            {
                Attack();
                StopMove();
                animator.SetTrigger("isAttack");
                animator.ResetTrigger("isRun");
                Invoke("StartMove", 0.6f);
            }
            //animasi berjalan
            if (horizontal != 0)
            {
                animator.SetTrigger("isRun");
                animator.ResetTrigger("isIdle");
                
            }
            else if (horizontal == 0)
            {
                animator.SetTrigger("isIdle");
                animator.ResetTrigger("isRun");
            }
            if (vertical != 0)
            {
                animator.SetTrigger("isRun");
                animator.ResetTrigger("isIdle");
                
            }
        }
            
        
    }

    public void Attack()
    {

        // jarak serangan
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(hitBox.position, hitRange, target);

        // damage ke musuh
        foreach(Collider2D enemy in hitEnemy)
        {
            enemy.GetComponent<enemy>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(hitBox == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(hitBox.position, hitRange);
    }

    public void StartMove()
    {
        Speed = 8f;
    }
    public void StopMove()
    {
        Speed = 0f;
    }
}
