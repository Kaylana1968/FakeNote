using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;


public class StarButtonFill : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject film;
    [SerializeField] GameObject background;
    [SerializeField] GameObject BGM;
    [SerializeField] AudioClip newMusic;
    [SerializeField] Button startButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button settingsButton;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] GameObject starsBackground;
    [SerializeField] GameObject secretBackground;
    AudioSource audioSource;
    AudioClip baseMusic;
    Image colorButton;
    Image quitColorButton;
    Image settingsColorButton;
    [SerializeField] ChangeSceneToGame changeSceneToGame;
    Image filmImage;
    SpriteRenderer backgroundSpriteRenderer;
    Image targetImage;
    Color baseColor = Color.white; 
    Color activeColor = new(0.5f, 0f, 0f);
    readonly float fillTime = 14.5f;

    bool isPressed = false;
    float currentFill = 0f;
    Coroutine fillCoroutine;
    readonly float shakeIntensityMax = 0.5f;
    Transform cameraTransform;
    Vector3 initialPosition;

    void Awake()
    {
        film.SetActive(false);
        secretBackground.SetActive(false);
        filmImage = film.GetComponent<Image>();

        backgroundSpriteRenderer = background.GetComponent<SpriteRenderer>();

        audioSource = BGM.GetComponent<AudioSource>();
        baseMusic = audioSource.clip;

        colorButton = startButton.GetComponent<Image>();
        quitColorButton = quitButton.GetComponent<Image>();
        settingsColorButton = settingsButton.GetComponent<Image>();
    }

    void Start()
    {
        targetImage = GetComponent<Image>();
        cameraTransform = Camera.main.transform;
        initialPosition = cameraTransform.localPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        film.SetActive(true);
        audioSource.clip = newMusic;
        audioSource.volume = 0f;
        audioSource.Play();

        if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);
        }
        fillCoroutine = StartCoroutine(FillStar(activeColor, 1f));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        film.SetActive(false);

        if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);
        }

        ResetCameraPosition();

        if (currentFill <= 0.99f)
        {
            fillCoroutine = StartCoroutine(FillStar(baseColor, 0f));
            audioSource.clip = baseMusic;
            audioSource.volume = 1f;
            audioSource.Play();
        }
    }

    IEnumerator FillStar(Color targetColor, float targetFillAmount)
    {
        float startFill = targetImage.fillAmount;
        Color startColor = targetImage.color;
        float elapsed = 0f;

        float duration = Mathf.Abs(targetFillAmount - startFill) * fillTime;
        
        bool isFillingUp = targetFillAmount > startFill;
        
        // --- LOGIQUE TRANSPARENCE IMAGE ---
        Color filmStartColor = filmImage.color;
        Color filmTargetColor = filmImage.color;
        filmTargetColor.a = targetFillAmount; 
        // ----------------------------------

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            
            // Mise à jour de l'image du bouton
            targetImage.fillAmount = Mathf.Lerp(startFill, targetFillAmount, t);
            targetImage.color = Color.Lerp(startColor, targetColor, t);
            currentFill = targetImage.fillAmount;

            audioSource.volume = Mathf.Lerp(0f, 1f, t);
            
            // MISE À JOUR DE LA COULEUR/ALPHA DU FILM
            if (filmImage != null)
            {
                filmImage.color = Color.Lerp(filmStartColor, filmTargetColor, t/4);
            }
            
            // LOGIQUE DE TREMBLEMENT CONTINU
            if (isFillingUp && cameraTransform != null)
            {
                float currentIntensity = shakeIntensityMax * currentFill; 
                Vector3 randomOffset = Random.insideUnitSphere * currentIntensity;
                cameraTransform.localPosition = initialPosition + randomOffset;
            }

            // Exécution de l'action à la fin du remplissage
            if (isFillingUp && isPressed && targetImage.fillAmount >= 0.99f)
            {
                ResetCameraPosition(); 
                
                ExecuteAction();
                isPressed = false;
                targetImage.fillAmount = 1f;
                targetImage.color = activeColor;
                
                // Assure que le film est totalement opaque ou transparent à la fin de l'action
                if (filmImage != null)
                {
                    filmImage.color = filmTargetColor;
                }
                
                film.SetActive(false);
                yield break;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // S'assure que tout est à l'état final
        ResetCameraPosition();
        audioSource.volume = 1f;
        
        targetImage.fillAmount = targetFillAmount;
        targetImage.color = targetColor;
        currentFill = targetFillAmount;
        
        // Désactive l'objet film
        if (targetFillAmount == 0f) 
        {
            film.SetActive(false);
        }
    }

    void ExecuteAction()
    {
        // backgroundSpriteRenderer.color = new Color(1f, 0f, 0f);
        // starsBackground.SetActive(false);
        // secretBackground.SetActive(true);
        // colorButton.color = new Color(0.5f, 0f, 0f);
        // quitColorButton.color = new Color(0.5f, 0f, 0f);
        // settingsColorButton.color = new Color(0.5f, 0f, 0f);


        // startButton.onClick.RemoveAllListeners();
        // startButton.onClick.AddListener(() => changeSceneToGame.MoveToGameScene("SecretMainMenu"));

        // settingsButton.onClick.RemoveAllListeners();
        // settingsButton.onClick.AddListener(() => changeSceneToGame.MoveToGameScene("SecretSettings"));

        // title.color = new Color(0.5f, 0f, 0f);

        SceneManager.LoadScene("SecretHome");
    }

    void ResetCameraPosition()
    {
        if (cameraTransform != null)
        {
            cameraTransform.localPosition = initialPosition;
        }
    }
}