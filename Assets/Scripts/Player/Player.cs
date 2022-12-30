using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Cloth;

public class Player : Singleton<Player>//, IDamageable
{
    public List<Collider> colliders;
    public Animator animator;
    public CharacterController characterController;
    public float speed = 1f;
    public float turnspeed = 1f;
    public float gravity = -9.8f;
    public float jumpSpeed = 15f;

    [Header("Run")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    private float vSpeed = 0f;
    [Header("Flash")]
    public List<FlashColor> flashColors;

    [Header("Life")]
    public HealthBase healthBase;
    public UIFillUpdater uiGunUpdater;

    [Space]
    [SerializeField] private ClothChange _clothChange;

    private bool _alive = true;
    private bool _jumping = false;

    private void OnValidate()
    {
        if(healthBase != null)
        {
            healthBase = GetComponent<HealthBase>();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        OnValidate();

        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }

    #region LIFE

    private void OnKill(HealthBase h)
    {
        if (_alive)
        {
            _alive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);

            Invoke(nameof(Revive), 3f);
        }

    }

    private void Revive()
    {
        _alive = true;
        healthBase.ResetLife();
        animator.SetTrigger("Revive");
        Respawn();
        Invoke(nameof(TurnOnColliders), 1f);
    }

    private void TurnOnColliders()
    {
        colliders.ForEach(i => i.enabled = true);
    }

    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
    }

    public void Damage(float damage, Vector3 dir)
    {
        //Damage(damage);
    }
    #endregion


    private void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnspeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        if (characterController.isGrounded)
        {
            if (_jumping)
            {
                _jumping = false;
                animator.SetTrigger("Land");
            }

            vSpeed = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                vSpeed = jumpSpeed;
                if (!_jumping)
                {
                    _jumping = true;
                    animator.SetTrigger("Jump");
                }
                
            }

        }
       var isWalking = inputAxisVertical != 0;
       if (isWalking)
       {
           if (Input.GetKey(keyRun))
           {
                speedVector *= speedRun;
                animator.speed = speedRun;
           }
            else
            {
                animator.speed = 1;
            }
       }
       

        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        characterController.Move(speedVector * Time.deltaTime);

        animator.SetBool("Run", isWalking);

    }

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if (CheckpointManager.Instance.HasCheckpoint())
        {
            characterController.enabled = false;
            transform.position = CheckpointManager.Instance.GetPositionFromLastCheckpoint();
            characterController.enabled = true;
        }
    }

    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }

    IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration)
    {
        var defaultspeed = speed;
        speed = localSpeed;
        yield return new WaitForSeconds(duration);
        speed = defaultspeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        _clothChange.ChangeTexture(setup);
        yield return new WaitForSeconds(duration);
        _clothChange.ResetTexture();
    }
}
