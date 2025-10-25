using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite[] sprites;
    public float strength = 5f;
    public float gravity = -9.8f;
    public float tilt = 5f;

    public AudioClip jumpClip;
    public AudioClip hitClip;
    
    public AudioClip diamondClip;
    private AudioSource audioSource;

    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private int spriteIndex;

    private float wingFlapTimer = 0f;
    public float wingFlapSpeed = 6f; // Plus grand = bat plus vite

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        // Si pas d'AudioSource sur le GameObject, ajoutez-en un dynamiquement :
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        // Désactive l'ancienne animation cyclique
        //InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        // Animation ailes réaliste
        if (sprites != null && sprites.Length > 1)
        {
            wingFlapTimer += Time.deltaTime * wingFlapSpeed;
            // Oscille entre 0 et sprites.Length-1
            float t = (Mathf.Sin(wingFlapTimer) + 1f) * 0.5f; // t entre 0 et 1
            int index = Mathf.RoundToInt(Mathf.Lerp(0, sprites.Length - 1, t));
            if (index != spriteIndex)
            {
                spriteIndex = index;
                spriteRenderer.sprite = sprites[spriteIndex];
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
            if (jumpClip != null)
                audioSource.PlayOneShot(jumpClip);
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    // Supprimez ou commentez AnimateSprite si non utilisé
    //private void AnimateSprite()
    //{
    //    spriteIndex++;
    //    if (spriteIndex >= sprites.Length)
    //    {
    //        spriteIndex = 0;
    //    }
    //    if (spriteIndex < sprites.Length && spriteIndex >= 0)
    //    {
    //        spriteRenderer.sprite = sprites[spriteIndex];
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("obstacle"))
        {
            if (hitClip != null)
                audioSource.PlayOneShot(hitClip);
            FindObjectOfType<GameManager>().GameOver();
        }
        else if (other.gameObject.CompareTag("scoring"))
        {
            
            FindObjectOfType<GameManager>().IncreaseScore();
        }
        else if (other.CompareTag("diamond1"))
        {
            if (diamondClip != null)
                audioSource.PlayOneShot(diamondClip);
            Debug.Log("Diamond collected by Player!");
            FindObjectOfType<GameManager>().IncreaseDiamondScore();
            Destroy(other.gameObject);
        }
    }
}