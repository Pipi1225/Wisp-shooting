using System.Collections;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("Status:")]
    [SerializeField] private float horizontalInput;
    [SerializeField] private bool grounded;
    [SerializeField] private bool doubleJump;
    [SerializeField] private bool dash;
    [SerializeField] private bool flag;
    [SerializeField] private bool poped = false;
    [SerializeField] private bool dead = false;
    [SerializeField] private bool IsAttacking = false;

    [Header("Stats:")]
    [SerializeField] private int health = 200;
    [SerializeField] private int currentHealth;
    [SerializeField] private float speed;

    [Header ("For Knockback:")]
    [SerializeField] private float knockBackForce; // 1
    [SerializeField] private float knockBackForceUp; // 2.23
    [SerializeField] private bool hitCooldown = false;
    [SerializeField] private bool unmovable = false;
    [SerializeField] private Action inAction = Action.None;
    [SerializeField] private enum Action { None, Inventory, Others };

    [Header("Damage Popup:")]
    [SerializeField] private Transform damagePopup;
    [SerializeField] private Transform healPopup;
    [SerializeField] private float dashCooldown; // 2
    [SerializeField] private float nextDash = 0f;

    [Header("UI:")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ActionBar dashBar;

    [Header("Controllers:")]
    [SerializeField] private Popup_Button popupButton;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Shoot projectile;
    [SerializeField] private Player_Action_Controller actionController;
    [SerializeField] private Transform button;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        projectile = GetComponent<Shoot>();
        actionController = GetComponent<Player_Action_Controller>();
        popupButton = GetComponentInChildren<Popup_Button>();
        button = this.transform.Find("Button");

        currentHealth = health;
        healthBar.SetMaxHealth(health);
        dashBar.SetCoolDown(dashCooldown);
        flag = true;   
    }

    private void Update()
    {
        if (dead)
        {
            if (grounded && !unmovable)
            {
                stopMovement();
            }

            return;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        dash = (Time.time >= nextDash);

        Vector3 t = transform.localScale;

        if (!unmovable && flag)
        {
            if (dash && Input.GetKeyDown(KeyCode.LeftControl))
            {
                body.velocity = new Vector2(Mathf.Sign(t.x) * speed * 1.6f, body.velocity.y);

                nextDash = Time.time + dashCooldown;
                dashBar.SetTimer();

                flag = false;
                Invoke("Cooldown2", 0.25f);
            }

            if (!IsAttacking && !anim.GetBool("Air_Attacking") && flag)
            {
                body.velocity = new Vector2(horizontalInput * speed * (float)(0.7), Mathf.Max(body.velocity.y, -30));

                //Jump
                if ((Input.GetKeyDown(KeyCode.Space) && (doubleJump || grounded)) && !IsAttacking)
                {
                    if (grounded)
                    {
                        anim.SetTrigger("Jump");
                        body.velocity = new Vector2(body.velocity.x, speed);
                        grounded = false;
                    }
                    else
                    {
                        body.velocity = new Vector2(body.velocity.x, (float)(speed / 1.24));
                        doubleJump = false;
                    }
                }

                //Flip Character
                if ((horizontalInput > 0.01f && transform.localScale.x < 0) || (horizontalInput < -0.01f && transform.localScale.x > 0))
                {
                    t.x *= -1;
                }
                button.localScale = t;
                transform.localScale = t;

                //Jump Animation
                anim.SetBool("Ground", grounded);

                //Double Jump Animation
                anim.SetBool("DoubleJump", doubleJump);
            }

            //Run Animation
            anim.SetBool("IsRunning", Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || (!flag));

            if (IsAttacking && !anim.GetBool("Air_Attacking"))
            {
                body.velocity = new Vector2(horizontalInput * speed * 0.2f, body.velocity.y);
            }
        }
        
        var obj = popupButton.getCurrentInteractable();
        bool pressed = false;
        if (inAction == Action.None)
        {
            if (grounded && !IsAttacking)
            {
                if (Input.GetKeyDown(KeyCode.I))
                {
                    pressed = true;
                    stopMovement();
                    actionController.openInventory();
                    anim.SetTrigger("Searching_Inventory");
                    unmovable = true;
                    inAction = Action.Inventory;
                }

                if (obj != null &&Input.GetKeyDown(obj.getKey()))
                {
                    pressed = true;
                    stopMovement();
                    obj.startInteract();
                    inAction = Action.Others;
                }
            }

            if (pressed)
            {
                poped = popupButton.getPopedStatus();
                if (poped)
                {
                    popupButton.popDown();
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.I) && inAction == Action.Inventory)
            {
                pressed = true;
                actionController.closeInventory();
                anim.SetTrigger("Stop_Inventory");
                unmovable = false;
                inAction = Action.None;
            }

            if (obj != null && Input.GetKeyDown(obj.getKey()) && inAction == Action.Others)
            {
                pressed = true;
                obj.stopInteract();
                inAction = Action.None;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pressed = true;
                StopAction();
            }

            if (pressed && poped)
            {
                poped = false;
                popupButton.popUp();
            }
        }
    }

    public void StopAction()
    {
        if (inAction == Action.Inventory)
        {
            actionController.closeInventory();
            anim.SetTrigger("Stop_Inventory");
            unmovable = false;
        }
        else if (inAction == Action.Others)
        {
            var obj = popupButton.getCurrentInteractable();
            if (obj != null)
            {
                obj.stopInteract();
            }
        }

        inAction = Action.None;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check grounded
        if (collision.CompareTag("Ground") && body.velocity.y == 0)
        {
            grounded = true;
            doubleJump = true;

            anim.ResetTrigger("Jump");
            anim.SetBool("Air_Attacking", false);
            anim.SetBool("Ground", grounded);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //check grounded
        if (collision.CompareTag("Ground") && body.velocity.y == 0)
        {
            grounded = true;
            doubleJump = true;

            anim.ResetTrigger("Jump");
            anim.SetBool("Air_Attacking", false);
            anim.SetBool("Ground", grounded);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //check grounded
        if (collision.CompareTag("Ground") && body.velocity.y == 0)
        {
            grounded = false;
            doubleJump = true;

            anim.SetBool("Ground", grounded);
        }
    }

    public void Healing(int amount)
    {
        currentHealth += amount;

        if (currentHealth > health)
        {
            currentHealth = health;
        }

        healthBar.SetHealth(currentHealth);

        Transform damagePopupTransform = Instantiate(healPopup, body.position, Quaternion.identity);
        damagePopupTransform.GetComponent<damagePopup>().Setup(amount, body.position);
    }

    public void TakeDamage(int damage, float enemies)
    {
        if (dead)
        {
            return;
        }

        if (!hitCooldown && damage > 0)
        {
            currentHealth -= damage;

            healthBar.SetHealth(currentHealth);

            float knockBackDirector;

            hitCooldown = true;
            unmovable = true;
            inAction = Action.None;
            actionController.stopCameraMovement();

            if (currentHealth <= 0)
            {
                dead = true;
                anim.SetBool("IsDead", true);
                Debug.Log("You die D:!");

                sprite.color = new Color(1f, 1f, 1f, .8f);
            }
            else
            {
                //Invicible Time
                Invoke("Cooldown", 2f);

                //Blink
                Invoke("Normal", 2f);

                sprite.color = new Color(1f, 1f, 1f, .6f);

                anim.SetBool("Knockbaked", true);
                anim.SetBool("IsRunning", false);
                anim.SetBool("Air_Attacking", false);
            }

            //Immoveable Time
            Invoke("Cooldown1", 0.75f);

            Transform damagePopupTransform = Instantiate(damagePopup, body.position, Quaternion.identity);
            damagePopupTransform.GetComponent<damagePopup>().Setup(damage, body.position);

            if (enemies > transform.position.x)
            {
                knockBackDirector = -1;
            }
            else
            {
                knockBackDirector = 1;
            }
            
            Vector3 t = transform.localScale;

            if (knockBackDirector < 0.01f)
            {
                if (t.x < 0) 
                {
                    t.x *= -1;
                }

                transform.localScale = t;
            }
            else if (knockBackDirector > -0.01f)
            {
                if (t.x > 0) 
                {
                    t.x *= -1;
                }

                transform.localScale = t;
            }

            body.velocity = new Vector2(knockBackDirector * 0.6f, knockBackForceUp) * knockBackForce;
        }  
    }

    public bool getHitCooldown()
    {
        return hitCooldown;
    }

    public bool getInmoveableStatus()
    {
        return unmovable;
    }

    public bool getDeadStatus()
    {
        return dead;
    }

    public void setInmoveableStatus(bool status)
    {
        unmovable = status;
    }

    void stopMovement()
    {
        body.velocity = new Vector2(0, 0);
    }

    void Attacking()
    {
        IsAttacking = true;
    }

    void stopAttacking()
    {
        IsAttacking = false;
    }

    void StopAniKnockbacked()
    {
        anim.SetBool("Knockbaked", false);
    }

    void Cooldown()
    {
        hitCooldown = false;        
    }

    void Normal()
    {
        sprite.color = new Color(1f, 1f, 1f, 1f);
    }

    void Cooldown1()
    {
        unmovable = false;
    }

    void Cooldown2()
    {
        flag = true;
    }
}
