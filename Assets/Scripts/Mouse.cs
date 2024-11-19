using UnityEngine;

public class Mouse : MonoBehaviour
{


    [SerializeField]
    Texture2D cursorImg;

    [SerializeField]
    public Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.SetCursor(cursorImg, new Vector2(32,32), CursorMode.ForceSoftware);
    }
    void Update()
    {
        transform.position = player.transform.position + new Vector3( 0, 0, -10);
    }

}
