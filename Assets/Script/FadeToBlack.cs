using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    [Header("Fade Settings")]
    public float fadeDuration = 0.5f;

    public UnityEvent OnMiddleFade;

    private bool isFading;

    //public static FadeToBlack instance;
    private void Start()
    {
        /*if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }*/
        _canvasGroup.alpha = 1f;
        StartCoroutine(Fade(1f, 0f));
    }

    [EasyButtons.Button]
    public void LaunchFade()
    {
        if (!isFading)
            StartCoroutine(FadeRoutine());
    }

    private IEnumerator FadeRoutine()
    {
        isFading = true;

        // Fade to black
        yield return StartCoroutine(Fade(0f, 1f));

        // Call event in the middle (scene switch, logic, etc.)
        OnMiddleFade?.Invoke();

        // Small optional pause at full black
        yield return new WaitForSeconds(0.1f);

        // Fade back in
        yield return StartCoroutine(Fade(1f, 0f));

        isFading = false;
    }

    private IEnumerator Fade(float from, float to)
    {
        Debug.Log("Yo");
        float t = 0f;
        _canvasGroup.alpha = from;
        _canvasGroup.blocksRaycasts = true;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            Debug.Log(Mathf.Lerp(from, to, t / fadeDuration));

            _canvasGroup.alpha = Mathf.Lerp(from, to, t / fadeDuration);
            yield return null;
        }

        _canvasGroup.alpha = to;
        _canvasGroup.blocksRaycasts = to > 0f;
    }
}
