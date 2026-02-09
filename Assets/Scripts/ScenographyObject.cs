using UnityEngine;
using UnityEngine.Events;

public class ScenographyObject : MonoBehaviour
{
    // [HideInInspector] public Vector2 initialPos;
    // public Vector2 finalPos;
    public float moveSpeed = 10f;
    public ScenographySkin[] skins;
    public ScenographyRailLimit railLimit;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    private int currentSkinIndex = 0;
    private bool isMovingRight = true;
    private bool isMovingDown = true;
    public bool moveHorizontal = false;
    public bool moveVertical = false;
    public Sword sword;
    public Rail rail;
    private Transform currentWaypoint;
    public bool isHuman = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = skins[0].image;
    }

    void OnEnable()
    {
        if(rail != null) 
            rail.SetScenographyObject(this);
        else
            Debug.LogWarning("Rail is null at " + name);
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

    public void ChangeSkin(UnityAction callback = null)
    {
        if(currentSkinIndex == skins.Length -1 ) currentSkinIndex = 0;
        else currentSkinIndex++;
        Sprite skin;
        if(Game.IsKeyInBrokenScenography(skins[currentSkinIndex].key))
            skin = skins[currentSkinIndex].damagedImage;
        else
            skin = skins[currentSkinIndex].image;
        
        if(sword != null && Game.IsKnifeHooked && skins[currentSkinIndex].key == "MOON") sword.gameObject.SetActive(true);
        else if (sword != null) sword.gameObject.SetActive(false);
        Debug.Log("IS HUMAN: " + isHuman);
        if(isHuman) {
            Debug.Log("Voy a aplastarme porque soy humano");
            Vector3 initialScale = transform.localScale; 
            Vector3 impact = new Vector3(initialScale.x * 1.06f, 0.94f, 1f);
            LeanTween.scale(gameObject, impact, 0.2f)
                .setEase(LeanTweenType.easeOutQuad)
                .setOnComplete(() =>
                {
                    spriteRenderer.sprite = skin;
                    LeanTween.scale(gameObject, initialScale, 0.12f)
                        .setEase(LeanTweenType.easeOutBack);
                    callback?.Invoke();
                });
        }
        else {
            Debug.Log("Voy a rotar porque no soy humano");
            LeanTween.value(gameObject, 0, 90, 0.2f)
            .setOnUpdate((float val) => {
                transform.rotation = Quaternion.Euler(0,val,0);
            })
            .setOnComplete(() => {
                spriteRenderer.sprite = skin;
                LeanTween.value(gameObject, 90, 0, 0.2f)
                    .setOnUpdate((float val) => {
                        transform.rotation = Quaternion.Euler(0,val,0);
                    })
                    .setOnComplete(() => {
                        callback?.Invoke();
                    });
            });
        }
        
    }

    public string GetKey()
    {
        return skins[currentSkinIndex].key;
    }

    public void SwitchImageToDamaged()
    {
        spriteRenderer.sprite = skins[currentSkinIndex].damagedImage;
    }


    public void TweenVerticalToNextLevel(bool moveUp)
    {
        // var nextWaypoint = rail.GetNextLevelWaypoint(moveDown);
        // LeanTween.move(gameObject, currentWaypoint.position, moveSpeed)
        // .setOnComplete(() => {
        //     currentWaypoint = nextWaypoint;
        // });
    }
}
