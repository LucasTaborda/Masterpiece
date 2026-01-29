using UnityEngine;

public class ScenographyObject : MonoBehaviour
{
    // [HideInInspector] public Vector2 initialPos;
    // public Vector2 finalPos;
    public float moveSpeed = 10f;
    public ScenographySkin[] skins;
    public ScenographyRailLimit railLimit;
    private SpriteRenderer spriteRenderer;
    private int currentSkinIndex = 0;
    private bool isMovingRight = true;
    private bool isMovingDown = true;
    public bool moveHorizontal = false;
    public bool moveVertical = false;
    
    void Awake()
    {
        // initialPos = gameObject.transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = skins[0].image;
    
    }

    // public void MoveToFinalPos()
    // {
    //     LeanTween.move(gameObject, finalPos, moveSpeed);
    // }

    // public void MoveToInitialPos()
    // {
    //     LeanTween.move(gameObject, initialPos, moveSpeed);
    // }

    public void MoveHorizontal()
    {
        if(!moveHorizontal) return;
        if(isMovingRight){
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;    
            if(transform.position.x >= railLimit.rightLimitTransform.position.x){
                isMovingRight = false;
            }
        }
        else{
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            if(transform.position.x <= railLimit.leftLimitTransform.position.x){
                isMovingRight = true;
            }
        }
    }

    public void MoveVertical()
    {
        if(!moveVertical) return;
        if(isMovingDown){
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;    
            if(transform.position.y <= railLimit.bottomLimitTransform.position.y){
                isMovingDown = false;
            }
        }else{
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            if(transform.position.y >= railLimit.topLimitTransform.position.y){
                isMovingDown = true;
            }
        }
    }


    void Update()
    {
        MoveHorizontal();
        MoveVertical();
    }

    public void ChangeSkin()
    {
        if(currentSkinIndex == skins.Length -1 ) currentSkinIndex = 0;
        else{
            currentSkinIndex++;
        }

        spriteRenderer.sprite = skins[currentSkinIndex].image;
    }

    public string GetKey()
    {
        return skins[currentSkinIndex].key;
    }

}
