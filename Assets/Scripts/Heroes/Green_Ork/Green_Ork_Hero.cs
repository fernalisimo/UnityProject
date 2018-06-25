using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Green_Ork_Hero : MonoBehaviour
{
    public static Green_Ork_Hero current;
    public AudioClip attackSound = null;

    AudioSource attackSource = null;

    public enum Mode
    {
        GoToA,
        GoToB,
        Dead,
        Attack
    }

    Mode mode = Mode.GoToB;

    Rigidbody2D myBody = null;
    Animator myController = null;
    public float speed = 2;
    public float PatrolDistance = 4;
    Vector3 scale_speed;
    Vector3 targetScale = Vector3.one;

    int health = 1;

    Vector3 pointA;
    Vector3 pointB;

    public bool isDead()
    {
        return this.health == 0;
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
        if (this.health == 0)
        {
            int childCount = this.transform.childCount;
            for (int i = 0; i < childCount; i++)
                Destroy(this.transform.GetChild(i).gameObject);
            StartCoroutine(orcDie());
        }
    }

    IEnumerator orcDie()
    {
        this.mode = Mode.Dead;
        this.myController.SetTrigger("die");
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }

    public void showAttack()
    {
        this.myController.SetTrigger("attack");
        if (SoundManager.Instance.isSoundOn())
        {
            attackSource.Play();
        }
    }

    float getDirection()
    {
        if (this.mode == Mode.Dead)
            return 0;

        Vector3 my_pos = this.transform.position;
        Vector3 rabit_pos = HeroRabbit.lastRabit.transform.position;

        if (rabit_pos.x > Mathf.Min(pointA.x, pointB.x)
            && rabit_pos.x < Mathf.Max(pointA.x, pointB.x))
        {
            mode = Mode.Attack;
            current = this;
        }
        if (mode == Mode.Attack && !(rabit_pos.x > Mathf.Min(pointA.x, pointB.x)
            && rabit_pos.x < Mathf.Max(pointA.x, pointB.x)))
            mode = Mode.GoToA;

        if (mode == Mode.Attack && !HeroRabbit.lastRabit.isDead())
        {
            if (my_pos.x < rabit_pos.x)
                return 1;
            else
                return -1;
        }

        if (this.mode == Mode.GoToB)
        {
            if (my_pos.x >= pointB.x)
                this.mode = Mode.GoToA;
        }
        else if (this.mode == Mode.GoToA)
        {
            if (my_pos.x <= pointA.x)
                this.mode = Mode.GoToB;
        }

        if (this.mode == Mode.GoToB)
        {
            if (my_pos.x <= pointB.x)
                return 1;
            else
                return -1;
        }
        else if (this.mode == Mode.GoToA)
        {
            if (my_pos.x <= pointA.x)
                return 1;
            else
                return -1;
        }

        return 0;
    }

    void Start()
    {
        pointA = this.transform.position;
        pointB = pointA;

        if (PatrolDistance < 0)
        {
            pointA.x += PatrolDistance;
        }
        else
        {
            pointB.x += PatrolDistance;
        }

        myBody = this.GetComponent<Rigidbody2D>();
        myController = this.GetComponent<Animator>();

        attackSource = gameObject.AddComponent<AudioSource>();
        attackSource.clip = attackSound;
    }

    void FixedUpdate()
    {
        float value = this.getDirection();
        Animator animator = GetComponent<Animator>();
        if (Mathf.Abs(value) > 0)
        {
            animator.SetBool("run", true);
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;
        }
        else
            animator.SetBool("run", false);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0)
            sr.flipX = false;
        else if (value > 0)
            sr.flipX = true;

        this.transform.localScale = Vector3.SmoothDamp(this.transform.localScale, this.targetScale, ref scale_speed, 1.0f);
    }
}