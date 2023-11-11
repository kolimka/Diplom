using System.Collections;
using TMPro;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TMP_InputField answerInput;
    public TextMeshProUGUI timerText;
    public float timeLimit = 10.0f;
    private float startTime;
    private bool correctAnswerGiven = false;
    public Canvas quiz;

    public Hero hero;

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
            quiz.gameObject.SetActive(false);
            hero.allowPause = true;
            hero.canMove = true;
            hero.canJump = true;
            return;
        }

        questionText.text = $"������: {questions[currentQuestionIndex]}";
        answerInput.text = "";
        answerInput.Select(); // ������������� ����� �� ���� �����
        answerInput.ActivateInputField();
        startTime = Time.realtimeSinceStartup;
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        while (!correctAnswerGiven)
        {
            float elapsedTime = Time.realtimeSinceStartup - startTime;
            float remainingTime = Mathf.Max(0, timeLimit - elapsedTime);
            timerText.text = $"�������� �������: {remainingTime:F1} ���.";

            if (elapsedTime >= timeLimit)
            {
                timerText.text = "����� �����!";
                NextQuestion();
            }

            yield return null;
        }
    }

    private void NextQuestion()
    {
        currentQuestionIndex++;
        correctAnswerGiven = false;
        DisplayCurrentQuestion();
    }

    public void CheckAnswer(string userAnswer)
    {
        string playerAnswer = answerInput.text.Trim().ToLower();
        string correctAnswer = answers[currentQuestionIndex].Trim().ToLower();

        if (playerAnswer == correctAnswer)
        {
            Debug.Log("���������� �����!");
            correctAnswerGiven = true;
            NextQuestion();
        }
        else
        {
            Debug.Log("������������ �����!");
        }
    }
}
