using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCastlevania : MonoBehaviour
{
    public LayerMask GroundLayer;
    public LayerMask EnemyLayer;
    public float MoveSpeed;
    public float JumpForce;
    
    private bool _isGrounded { get { return IsGrounded(); } }
    private Rigidbody2D _rb;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Hurt();
    }

    // Update is called once per frame
    void Update()
    {
        ManageHorizontal();
        ManageJump();

        CheckEnemyBelow();
    }

    private void ManageJump()
    {
        if (Input.GetAxisRaw("Vertical") > 0 || Input.GetKey(KeyCode.Space))
        {
            if (_isGrounded)
            {
                Jump(JumpForce);
            }
        }
    }

    private void Jump(float jumpForce)
    {
        //_rb.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
        _rb.velocity += Vector2.up * jumpForce;
    }

    private void CheckEnemyBelow()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.2f;

        Debug.DrawRay(position, direction * distance, Color.green);
        RaycastHit2D hit = Physics2D.BoxCast(position, new Vector2(0.09f, 1), 0, new Vector2(0, -1), Mathf.Infinity, EnemyLayer);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag.Equals("Enemy"))
            {
                EnemyJumpKilled e = hit.collider.gameObject.GetComponent<EnemyJumpKilled>();
                if (e != null)
                {
                    _rb.velocity += Vector2.up * 10;
                    e.Kill();
                }
            }
        }
    }

    private void ManageHorizontal()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector3(direction, 0, 0) * MoveSpeed * Time.deltaTime);

        if (direction < 0) {
            this.transform.Find("Sprite").rotation = Quaternion.Euler(0, 180, 0);
        } else if(direction > 0){
            this.transform.Find("Sprite").rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.2f;

        Debug.DrawRay(position, direction * distance, Color.green); //------------------------------

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, GroundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

    public void Hurt()
    {
        StartCoroutine(HurtAnimation());
    }

    IEnumerator HurtAnimation()
    {
        SpriteRenderer s = transform.Find("Sprite").GetComponent<SpriteRenderer>();

        Color original = new Color(s.color.r, s.color.g, s.color.b, 1);
        Color original_alphaCero = new Color(original.r, original.g, original.b, 0);

        for (int i = 0; i < 4; i++)
        {
            s.color = original_alphaCero;
            yield return new WaitForSeconds(0.2f);
            s.color = original;
            yield return new WaitForSeconds(0.2f);
        }

    }
}
