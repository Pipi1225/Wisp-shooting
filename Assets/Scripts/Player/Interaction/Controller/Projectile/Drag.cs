using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    Camera cam;

    public Projectile projectile;
    public Projectile defaultProjectile;
    public float PushForce;
    private Animator anim;
    private Rigidbody2D body;

    public Trajectory trajectory;

    bool collision = false;
    bool isDragging;

    public Vector2 startPoint, endPoint, direction, force;
    [SerializeField] private float distance;
    public bool hasDragged = false;


    private void Start()
    {
        cam = Camera.main;
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        trajectory = GameObject.Find("Projectile Trajectory").GetComponent<Trajectory>();
    }

    private void OnEnable()
    {
        hasDragged = false;

        isDragging = false;
        
        defaultProjectile = null;
    }

    private void Update()
    {
        collision = anim.GetBool("Collision");

        if (collision) 
        {
            trajectory.CollisionHide(transform.position);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            isDragging = false;

            defaultProjectile = null;

            StopDrag();
        }
        
        if (!hasDragged && !collision)
        {
            Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                defaultProjectile = null;
                //Debug.Log(Physics2D.OverlapPoint(pos));
                
                if(projectile.getCollider() == Physics2D.OverlapPoint(pos))
                {
                    isDragging = true;

                    defaultProjectile = projectile;

                    OnDragStart();
                }          
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (defaultProjectile != null)
                {
                    isDragging = false;

                    OnDragEnd();
                }
            }

            if (isDragging)
            {
                OnDrag();
            }
        }
    }

    private void OnDragStart()
    {
        startPoint = transform.position;
        trajectory.Show(defaultProjectile.pos);
    }

    private void StopDrag()
    {
        trajectory.Hide();
    }

    private void OnDrag()
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Mathf.Min(Vector2.Distance(startPoint, endPoint), 0.5f);
        direction = (startPoint - endPoint).normalized;

        force = direction * distance * PushForce * 2;

        trajectory.UpdateDots(force);
    }

    private void OnDragEnd()
    {
        body.constraints = RigidbodyConstraints2D.None;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;

        defaultProjectile.Push(force);

        anim.SetBool("Attacking", true);

        hasDragged = true;

        trajectory.Hide();
    }
}
