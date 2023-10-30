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
            Debug.Log("Вопросы закончились!");
            return;
        }

        questionText.text = $"Вопрос: {questions[currentQuestionIndex]}";
        answerInput.text = "";
        remainingTime = timeLimit;
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        while (remainingTime > 0)
        {
            timerText.text = $"Осталось времени: {remainingTime:F1} сек.";
            yield return new WaitForSeconds(1.0f);
            remainingTime -= 1.0f;
        }

        timerText.text = "Время вышло!";
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
            Debug.Log("Правильный ответ!");
            NextQuestion();
        }
        else
        {
            Debug.Log("Неправильный ответ!");
        }
    }
}

