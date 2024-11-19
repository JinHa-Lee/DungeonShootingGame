using Unity.Mathematics;
using UnityEngine; 

public class Player : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    private float moveSpeed = 5f;
    private float dashSpeed = 30f;
    private float maxDashTime = 0.1f; 
    private float dashTime = 0f;
    private float cooldown = 2f;

    private float lastDash;


    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform bulletShootPos;
    private float shootInterval = 0.5f;
    private float lastShotTime = 0f;

    [SerializeField]
    private Animator anim = null;
    Rigidbody2D rigid;
    private float v;
    private float h;
    private Vector3 dir;
    [SerializeField]
    private GameObject scanObject;
    public int coin;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        v = gameManager.isTalkAction ? 0 : Input.GetAxisRaw("Vertical");
        h = gameManager.isTalkAction ? 0 : Input.GetAxisRaw("Horizontal");
        
        // Vector3 moveTo = new Vector3(h,v,0f);
        // transform.position += moveTo * moveSpeed * Time.deltaTime;

        // check button down up
        bool hDown = gameManager.isTalkAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = gameManager.isTalkAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = gameManager.isTalkAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = gameManager.isTalkAction ? false : Input.GetButtonUp("Vertical");


        // animation
        // anim.SetBool("isChange", false);
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {   
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("isChange", false);



        // Direction
        if (vDown && v == 1)
            dir = Vector3.up;
        else if (vDown && v == -1)
            dir = Vector3.down; 
        else if (hDown && h == -1)
            dir = Vector3.left; 
        else if (hDown && h == 1)
            dir = Vector3.right; 

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        
        
        // Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            gameManager.Action(scanObject);
        }

    }
    void FixedUpdate()
    {
        Vector2 moveVec =  new Vector2(h,v);
        rigid.linearVelocity = moveVec * moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            dash(moveVec);
        }


        Debug.DrawRay(rigid.position, dir * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dir, 0.7f, LayerMask.GetMask("Object"));


        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
            scanObject = null;



    }
    void dash(Vector2 moveVec)
    {
        if (Time.time - lastDash < cooldown)
        {
            Debug.Log("Dash cooldown");
            return;
        }
        else
        {
            dashTime += Time.deltaTime;
            if (dashTime >= maxDashTime)
            {
                dashTime = 0f;
                lastDash = Time.time;
            }
            else
            {
                rigid.linearVelocity = moveVec * dashSpeed;
            }
            
            
        }
    }

    void Shoot()
    {
        if (Time.time - lastShotTime > shootInterval)
        {
            Instantiate(bullet, bulletShootPos.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag =="Dungeon_Door")
        {
            gameManager.NextStage();
        }
        else if (col.gameObject.tag =="Dungeon_Stair")
        {
            gameManager.BeforeStage();
        }
        else if (col.gameObject.tag == "Coin")
        {
            GameManager.instance.IncreaceCoin();
            Destroy(col.gameObject);
        }
    }
}
