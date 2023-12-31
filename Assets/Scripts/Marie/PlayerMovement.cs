using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpStrength = 8f;
    public float upGravity = 1f, downGravity = 5f;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform feet;
    [SerializeField] private Checkpoint checkpoint;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private SoundPlayer _audioPlayer;

    private LayerMask voidLayer;
    private Collider2D _holeCollider;

    private float horizontalMovement = 0;
    public bool jump = false, isGrounded = false, canMove = true;
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioPlayer = GetComponent<SoundPlayer>();
        voidLayer = LayerMask.NameToLayer("Void");
    }

    void Update()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.A))
            {
                horizontalMovement = -_speed;
                _spriteRenderer.flipX = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                horizontalMovement = _speed;
                _spriteRenderer.flipX = false;
            }
            else
            {
                horizontalMovement = 0;
            }
        }
        
        _animator.SetBool("Walk", (horizontalMovement != 0 && isGrounded));
        transform.position += new Vector3(horizontalMovement * Time.deltaTime, 0, 0);

        CheckGround();
        
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            isGrounded = false;
            _animator.SetBool("Jump", true);
            _audioPlayer.PlayAudio(SoundFX.Jump);
        };
    }

    private void FixedUpdate()
    {
        if (jump)
        {
            _rigidbody.AddForce(new Vector2(0, _jumpStrength), ForceMode2D.Impulse);
            _rigidbody.gravityScale = upGravity;
            jump = false;
        }

        if (!isGrounded && _rigidbody.velocity.y < 0)
        {
            _rigidbody.gravityScale = downGravity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == groundLayers)
        {
            isGrounded = true;
            _animator.SetBool("Jump", false);
            _rigidbody.gravityScale = upGravity;
        } 
        else if(collision.gameObject.layer == voidLayer)
        {
            collision.collider.isTrigger = true;
            GetComponent<PlayerLife>().Hurt(1);
            Invoke("Respawn", 1f);
            _holeCollider = collision.collider;
        }
    }

    public void Respawn()
    {
        transform.position = checkpoint.transform.position;
        if(_holeCollider != null)
        {
            _holeCollider.isTrigger = false;
            _holeCollider = null;
        }
    }

    public void PlayWalkingSound()
    {
        _audioPlayer.PlayAudio(SoundFX.Walk);
    }
    
    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(feet.transform.position, Vector2.down, 0.1f, groundLayers);
        if (hit)
        {
            isGrounded = true;
            _animator.SetBool("Jump", false);
            _rigidbody.gravityScale = upGravity;
        }
        else {
            isGrounded = false;
            _animator.SetBool("Jump", true);
            _rigidbody.gravityScale = downGravity;
        }
    }

    public void ChangeCheckpoint(Checkpoint newCheckpoint)
    {
        if (checkpoint != newCheckpoint)
        {
            Destroy(checkpoint.gameObject);
        }
        checkpoint = newCheckpoint;
    }
}
