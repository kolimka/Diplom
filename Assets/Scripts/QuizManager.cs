using System.Collections;
using TMPro;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TMP_InputField answerInput;
    public TextMeshProUGUI timerText;
    public float timeLimit = 10.0f;
    private float remainingTime;

    private string[] questions = new string[10]
    {
        "������ 1?",
        "������ 2?",
        "������ 3?",
        "������ 4?",
        "������ 5?",
        "������ 6?",
        "������ 7?",
        "������ 8?",
        "������ 9?",
        "������ 10?"
    };

    private string[] answers = new string[10]
    {
        "����� 1",
        "����� 2",
        "����� 3",
        "����� 4",
        "����� 5",
        "����� 6",
        "����� 7",
        "����� 8",
        "����� 9",
        "����� 10"
    };

    private int currentQuestionIndex = 0;

    private void Start()
    {
        answerInput.onSubmit.AddListener(CheckAnswer);
        DisplayCurrentQuestion();
    }

    private void DisplayCurrentQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            Debug.Log("������� �����������!");
            return;
        }

        questionText.text = $"������: {questions[currentQuestionIndex]}";
        answerInput.text = "";
        remainingTime = timeLimit;
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        while (remainingTime > 0)
        {
            timerText.text = $"�������� �������: {remainingTime:F1} ���.";
            yield return new WaitForSeconds(1.0f);
            remainingTime -= 1.0f;
        }

        timerText.text = "����� �����!";
        NextQuestion();
    }

    private void NextQuestion()
    {
        currentQuestionIndex++;
        DisplayCurrentQuestion();
    }

    public void CheckAnswer(string userAnswer)
    {
        string playerAnswer = answerInput.text.Trim().ToLower();
        string correctAnswer = answers[currentQuestionIndex].Trim().ToLower();

        if (playerAnswer == correctAnswer)
        {
            Debug.Log("���������� �����!");
            NextQuestion();
        }
        else
        {
            Debug.Log("������������ �����!");
        }
    }
}

