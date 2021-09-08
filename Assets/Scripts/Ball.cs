using UnityEngine;

public class Ball : MonoBehaviour
{
    //Config parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xVelStart = 2f;
    [SerializeField] float yVelStart = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 2f;
    
    //State
    bool hasStarted = false;
    Vector2 paddleToBallVector;

    //Cached componenet references
    AudioSource myAudioSource;
    Rigidbody2D myrigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        myrigidbody2D = GetComponent<Rigidbody2D>();
        paddleToBallVector = transform.position - paddle1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(xVelStart,yVelStart);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2
                                    (Random.Range(0f,randomFactor), 
                                     Random.Range(0f,randomFactor));
        if(hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0,ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myrigidbody2D.velocity += velocityTweak;
        }
    }
}
