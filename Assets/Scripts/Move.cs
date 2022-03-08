using UnityEngine;
using System.Collections;
 
public class Move : MonoBehaviour {
 
    public float speed;
    public float jump;
    private SpriteRenderer _renderer;
    Animator _animator;
    Rigidbody2D rb;
 
    void Start () {
        _animator = gameObject.GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent <Rigidbody2D> ();
        
    }
 
    void Update () {
        // movement
        float x = Input.GetAxis ("Horizontal");

        // flips sprite
        if (Input.GetAxisRaw("Horizontal") > 0) {
            _renderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0) {
            _renderer.flipX = true;
        }   

        // jump
        if (Input.GetButtonDown("Jump") &&  Mathf.Abs(rb.velocity.y) < 0.001f) {
            rb.AddForce(new Vector2(0,jump), ForceMode2D.Impulse); 
            _animator.SetBool("isJumping", true);
        }
        else {
            _animator.SetBool("isJumping", false);
        }

        // update new position
        _animator.SetFloat("Speed",Mathf.Abs(x));
        transform.position += new Vector3 (x,0f,0f) * Time.deltaTime * speed;
        
 
    }

}