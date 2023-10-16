using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressManager : MonoBehaviour
{
    public Slider progressSlider;
    private float currentProgress = 0f;
    public float animationDuration = 1.0f;

    public void IncrementProgress()
    {
        // Увеличивайте текущий прогресс, например, на 0.1 (10%)
        currentProgress += 0.33f;

        // Ограничьте прогресс до 100%
        currentProgress = Mathf.Clamp01(currentProgress);

        // Если Slider уже имеет ненулевое значение, начните анимацию
        if (currentProgress > 0)
        {
            StartCoroutine(AnimateSlider());
        }
    }
    public float GetProgress()
    {
        return currentProgress;
    }
    private void Start()
    {
        // Вы можете начать анимацию вручную, если это необходимо
        StartCoroutine(AnimateSlider());
    }

    private IEnumerator AnimateSlider()
    {
        float startValue = progressSlider.value;
        float timer = 0f;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / animationDuration;
            progressSlider.value = Mathf.Lerp(startValue, currentProgress, progress);

            yield return null;
        }

        // Убедитесь, что Slider достиг целевого значения точно
        progressSlider.value = currentProgress;
    }
}
