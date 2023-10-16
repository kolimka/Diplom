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
        // ������������ ������� ��������, ��������, �� 0.1 (10%)
        currentProgress += 0.33f;

        // ���������� �������� �� 100%
        currentProgress = Mathf.Clamp01(currentProgress);

        // ���� Slider ��� ����� ��������� ��������, ������� ��������
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
        // �� ������ ������ �������� �������, ���� ��� ����������
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

        // ���������, ��� Slider ������ �������� �������� �����
        progressSlider.value = currentProgress;
    }
}
