using UnityEngine;

public class Bullet : MonoBehaviour
{


    private float bulletSpeed = 20f;
    Rigidbody2D rigid;

    
    GameObject preFabBullet;
    Vector2 dir;
    Camera cam;
    Vector2 endPos;
    Vector2 nowPos;
    public int damage = 1;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        endPos = Input.mousePosition;
        nowPos = transform.position;
        cam = Camera.main;
        dir = (Vector2)cam.ScreenToWorldPoint(endPos) - nowPos;
        dir = dir.normalized;
        

        Destroy(gameObject, 1f);
    }
    // Update is called once per frame
    void Update()
    {
        rigid.linearVelocity = dir * bulletSpeed;
        // rigid.AddForce(dir * bulletSpeed, ForceMode2D.Impulse);
    }



}
