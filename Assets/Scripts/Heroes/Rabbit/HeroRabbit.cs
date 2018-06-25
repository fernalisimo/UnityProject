using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroRabbit : MonoBehaviour {
    public GameObject losePrefab;

    public AudioClip walkingSound = null;
    AudioSource walkingSource = null;

    public AudioClip deathSound = null;
    AudioSource deathSource = null;

    public AudioClip landingSound = null;
    AudioSource landingSource = null;

    public int MaxHealth = 3;

    int health = 2;

	public float speed = 1;

    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;

    Rigidbody2D myBody = null;

    Transform heroParent = null;

    Animator animator = null;

    public static HeroRabbit lastRabit;

    Vector3 targetScale = Vector3.one;
    Vector3 scale_speed = Vector3.one;

    bool isMovable = true;

    void Awake()
    {
        lastRabit = this;
    }

    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        LevelController.current.setStartPosition(transform.position);
        animator = GetComponent<Animator>();

        this.heroParent = this.transform.parent;

        walkingSource = gameObject.AddComponent<AudioSource>();
        walkingSource.clip = walkingSound;

        deathSource = gameObject.AddComponent<AudioSource>();
        deathSource.clip = deathSound;

        landingSource = gameObject.AddComponent<AudioSource>();
        landingSource.clip = landingSound;

        LivesPanel.current.setLivesQuantity(this.health);
    }

	void FixedUpdate () {
        if (isMovable)
        {
            float value = Input.GetAxis("Horizontal");
            if (Mathf.Abs(value) > 0)
            {
                Vector2 vel = myBody.velocity;
                vel.x = value * speed;
                myBody.velocity = vel;

                if (SoundManager.Instance.isSoundOn())
                {
                    walkingSource.Play();
                }
            }
  

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (value < 0)
            {
                sr.flipX = true;
            }
            else if (value > 0)
            {
                sr.flipX = false;
            }

            Vector3 from = transform.position + Vector3.up * 0.3f;
            Vector3 to = transform.position + Vector3.down * 0.1f;
            int layer_id = 1 << LayerMask.NameToLayer("Ground");

            RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
            if (hit)
            {
                isGrounded = true;

                if (hit.transform != null
                && hit.transform.GetComponent<MovingPlatform>() != null)
                {
                    SetNewParent(this.transform, hit.transform);
                }
            }
            else
            {
                isGrounded = false;

                SetNewParent(this.transform, this.heroParent);
            }
            Debug.DrawLine(from, to, Color.red);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                this.JumpActive = true;
            }
            if (this.JumpActive)
            {
                if (Input.GetButton("Jump"))
                {
                    this.JumpTime += Time.deltaTime;
                    if (this.JumpTime < this.MaxJumpTime)
                    {
                        Vector2 vel = myBody.velocity;
                        vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                        myBody.velocity = vel;
                    }
                }
                else
                {
                    this.JumpActive = false;
                    this.JumpTime = 0;
                }
            }


            if (Mathf.Abs(value) > 0)
            {
                this.animator.SetBool("run", true);
            }
            else
            {
                this.animator.SetBool("run", false);
            }

            if (this.isGrounded)
            {
                this.animator.SetBool("jump", false);
                if (SoundManager.Instance.isSoundOn())
                {
                    landingSource.Play();
                }
            }
            else
            {
                this.animator.SetBool("jump", true);
                this.animator.SetBool("run", false);
            }
        }
    }

    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            Vector3 pos = obj.transform.position;
            obj.transform.parent = new_parent;
            obj.transform.position = pos;
        }
    }

    public void addHealth(int number)
    {
        this.health += number;
        if (this.health > MaxHealth)
            this.health = MaxHealth;
        this.onHealthChange();
    }

    public void removeHealth(int number)
    {
        this.health -= number;
        if (this.health < 0)
            this.health = 0;
        this.onHealthChange();
    }

    void onHealthChange()
    {
        LivesPanel.current.setLivesQuantity(this.health);

        if (this.health == 1)
        {
            StartCoroutine(rabitDie());
        }
        else if (this.health == 2)
        {
            StartCoroutine(rabitDie());
        }
        else if (this.health == 0)
        {
            StartCoroutine(rabitDie());
            GameObject parent = UICamera.first.transform.parent.gameObject;
            GameObject obj = NGUITools.AddChild(parent, losePrefab);
        }
    }

    IEnumerator rabitDie()
    {
        if (SoundManager.Instance.isSoundOn())
        {
            deathSource.Play();
        }
        isMovable = false;
        this.animator.SetBool("die", true);
        yield return new WaitForSeconds(4);
        LevelController.current.onRabbitDeath(this);
        this.animator.SetBool("die", false);
        isMovable = true;
    }

    public bool isDead()
    {
        return this.animator.GetBool("die");
    }

    public void makeBigger()
    {
        this.transform.localScale = Vector3.one * 2;
    }
}
