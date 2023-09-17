using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxVibrationForce;
    [SerializeField] private float deathVibrationForce;
    [SerializeField] private float timeToReachMaxVibrationForce = 2f;
    [SerializeField] private ParticleSystem chargeParticles;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private AudioSource wallpingChargeSfx;
    [SerializeField] private AudioSource trespassingWallSfx;
    [SerializeField] private AudioSource deathSfx;

    private float _currentVibrationForce;
    private float holdTimer;

    private SpriteRenderer _renderer;
    private Material _playerMaterial;
    
    private readonly int _noiseScaleProp = Shader.PropertyToID("_NoiseScale");
    private readonly int _velocityProp = Shader.PropertyToID("_Velocity");
    private readonly int _vibrationForceProp = Shader.PropertyToID("_VibrationForce");

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _playerMaterial = _renderer.material;
    }

    void Start()
    {
        holdTimer = 0;
        SetMaterialProperties();
    }

    // Update is called once per frame
    void Update()
    {
        if (MurosManager.instance.gameOver) return;
        if (Input.touchCount > 0)
        {
            Touch firstTouch = Input.GetTouch(0);
            switch (firstTouch.phase)
            {
                case TouchPhase.Began:
                    // finger was just put down
                    if (!MurosManager.instance.IsWallping)
                    {
                        GetComponent<Animator>().SetTrigger("Jump");
                        chargeParticles.Play();
                        wallpingChargeSfx.Play();
                        StartCoroutine(MoveToProjection(4.2f, 0.2f));
                    }

                    break;
                case TouchPhase.Ended:
                    // finger was just removed
                    StartCoroutine(MoveToProjection(5, 0.2f));
                    GetComponent<Animator>().SetTrigger("StartJump");
                    GetComponent<Animator>().ResetTrigger("StartJump");
                    StartCoroutine(MoveToPosition(Camera.main.gameObject, new Vector3(Camera.main.transform.position.x, -1f, Camera.main.transform.position.z), 0.2f));

                    MurosManager.instance.IsWallping = true;
                    MurosManager.instance.IsHolding = false;
                    //print(holdTimer);

                    _renderer.color = Color.cyan;
                    Invoke("StopWallping", holdTimer * 0.4f);
                    holdTimer = 0;
                    _currentVibrationForce = 0f;

                    chargeParticles.Stop();
                    explosionParticles.Play();
                    trespassingWallSfx.Play();
                    wallpingChargeSfx.Stop();

                    SetMaterialProperties();
                    break;
                case TouchPhase.Moved:
                    // finger was already down and has moved
                    break;
                case TouchPhase.Stationary:
                    float screenWidth = Screen.width;
                    if (firstTouch.position.x >= 0 && firstTouch.position.x < screenWidth * 0.1f)
                    {
                        MoveLeft();
                    }
                    else if (firstTouch.position.x <= screenWidth && firstTouch.position.x >= screenWidth * 0.9f)
                    {
                        MoveRight();
                    }
                    else
                    {
                        // finger was already down and hasnt moved
                        if (!MurosManager.instance.IsWallping)
                        {
                            holdTimer += Time.deltaTime;



                            var tVibrationForce = Mathf.InverseLerp(0f, timeToReachMaxVibrationForce, holdTimer);
                            _currentVibrationForce = Mathf.Lerp(0f, maxVibrationForce, tVibrationForce);

                            SetMaterialProperties();

                            MurosManager.instance.IsHolding = true;




                        }
                    }


                    
                    break;
                case TouchPhase.Canceled:
                    // touch was canceled by the system
                    break;
            }

        }
        if (Input.GetKey(KeyCode.RightArrow) && !MurosManager.instance.IsHolding)
        {
            transform.position += new Vector3(0.04f, 0, 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !MurosManager.instance.IsHolding)
        {
            transform.position += new Vector3(-0.04f, 0, 0);
        }

        
    }

    private void SetMaterialProperties()
    {
        _playerMaterial.SetFloat(_vibrationForceProp, _currentVibrationForce);
    }

    void StopWallping()
    {
        GetComponent<Animator>().SetTrigger("Fall");
        StartCoroutine(MoveToPosition(Camera.main.gameObject,new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z),0.2f));
        
        print("stop wallping");
        MurosManager.instance.IsWallping = false;
        _renderer.color = Color.white;

        trespassingWallSfx.Stop();
    }

    private void SetDeathMaterialProperties()
    {
        _playerMaterial.SetFloat(_velocityProp, 0f);
        _playerMaterial.SetFloat(_vibrationForceProp, deathVibrationForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!MurosManager.instance.IsWallping)
        {
            print("gameover");
            MurosManager.instance.gameOver = true;
            trespassingWallSfx.Stop();
            deathSfx.Play();
            GetComponent<Animator>().enabled = false;
            SetDeathMaterialProperties();
            StartCoroutine(LoadNextScene());
        }

    }

    private IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1f);

        var asyncOperation = SceneManager.LoadSceneAsync("GameOver");
        asyncOperation.allowSceneActivation = false;
        
        while (asyncOperation.progress < 0.9f)
        {
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;
    }

    private IEnumerator MoveToPosition(GameObject objectToMove, Vector3 newPosition, float waitTime) {
        float elapsedTime = 0;
        while (elapsedTime < waitTime)
        {
            objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, newPosition, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }

        // Make sure we got there
        objectToMove.transform.position = newPosition;
        yield return null;
    }

    private IEnumerator MoveToScale(GameObject objectToScale, Vector3 newScale, float waitTime)
    {
        float elapsedTime = 0;
        while (elapsedTime < waitTime)
        {
            objectToScale.transform.localScale = Vector3.Lerp(objectToScale.transform.position, newScale, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }

        // Make sure we got there
        objectToScale.transform.position = newScale;
        yield return null;
    }

    private IEnumerator MoveToProjection(float newSize, float waitTime)
    {
        float elapsedTime = 0;
        while (elapsedTime < waitTime)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, newSize, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }

        // Make sure we got there
        Camera.main.orthographicSize = newSize;
        yield return null;
    }

    public void MoveLeft()
    {
        transform.position += new Vector3(-0.04f*Time.deltaTime*60, 0, 0);
    }

    public void MoveRight()
    {
        transform.position += new Vector3(0.04f * Time.deltaTime * 60, 0, 0);
    }


}
