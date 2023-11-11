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
        "Вопрос 1?",
        "Вопрос 2?",
        "Вопрос 3?",
        "Вопрос 4?",
        "Вопрос 5?",
        "Вопрос 6?",
        "Вопрос 7?",
        "Вопрос 8?",
        "Вопрос 9?",
        "Вопрос 10?"
    };

    private string[] answers = new string[10]
    {
        "Ответ 1",
        "Ответ 2",
        "Ответ 3",
        "Ответ 4",
        "Ответ 5",
        "Ответ 6",
        "Ответ 7",
        "Ответ 8",
        "Ответ 9",
        "Ответ 10"
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

        questionText.text = $"Вопрос: {questions[currentQuestionIndex]}";
        answerInput.text = "";
        answerInput.Select(); // Устанавливаем фокус на поле ввода
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
            timerText.text = $"Осталось времени: {remainingTime:F1} сек.";

            if (elapsedTime >= timeLimit)
            {
                timerText.text = "Время вышло!";
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
            Debug.Log("Правильный ответ!");
            correctAnswerGiven = true;
            NextQuestion();
        }
        else
        {
            Debug.Log("Неправильный ответ!");
        }
    }
}
