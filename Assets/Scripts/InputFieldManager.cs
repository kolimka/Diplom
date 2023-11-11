using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class InputFieldManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_InputField lineNumberText;
    public ProgressManager progressManager;
    public TextMeshProUGUI task;
    public TextMeshProUGUI error;

    private string[] inputLines;
    private List<bool> lineResults = new List<bool>();
    public Button checkButton;
    public Button nextButton;
    public Button prevButton;
    public Button completeButton;

    private List<bool> taskProgress = new List<bool>();
    private List<string> taskDescriptions = new List<string>();
    private List<string[]> correctAnswers = new List<string[]>();
    private int currentTaskIndex = 0;
    private Dictionary<int, string> userTextByTask = new Dictionary<int, string>();

    public Hero hero;

    private void Start()
    {
        // Здесь заполняете списки taskDescriptions и correctAnswers соответствующими данными.
        InitializeTasks();

        // Устанавливаем начальное задание.
        SetTask(currentTaskIndex);

        // Назначаем обработчики кнопок.
        checkButton.onClick.AddListener(CheckAnswer);
        nextButton.onClick.AddListener(NextTask);
        prevButton.onClick.AddListener(PrevTask);
        completeButton.onClick.AddListener(CompleteTask);

        // Инициализируем прогресс и текст заданий.
        InitializeProgressAndTasksText();

        // Обновляем номера строк при изменении текста в поле ввода.
        inputField.onValueChanged.AddListener(UpdateLineNumbers);
    }

    private void InitializeTasks()
    {
        taskDescriptions.Add("Задание 1: Напишите простой HTML заголовок.");
        taskDescriptions.Add("Задание 2: Создайте абзац с текстом.");
        taskDescriptions.Add("Задание 3: Вставьте изображение.");

        correctAnswers.Add(new string[] { "<!DOCTYPE html>", "<html>", "<head>", "<link rel='stylesheet' href='style.css'>" });
        correctAnswers.Add(new string[] { "<p>Это абзац с текстом.</p>", "fdg" });
        correctAnswers.Add(new string[] { "<img src='image.jpg' alt='Моя картинка'>" });
    }

    private void InitializeProgressAndTasksText()
    {
        progressManager = FindFirstObjectByType<ProgressManager>();
        if (progressManager == null)
        {
            return;
        }

        for (int i = 0; i < taskDescriptions.Count; i++)
        {
            taskProgress.Add(false);
            if (PlayerPrefs.HasKey($"TaskText_{i}"))
            {
                userTextByTask[i] = PlayerPrefs.GetString($"TaskText_{i}");
            }
        }
    }

    private void UpdateLineNumbers(string newText)
    {
        // Разделяем текст на строки и считаем их количество
        string[] lines = newText.Split('\n');
        int lineCount = lines.Length;

        // Инициализируем массив inputLines с размером, равным количеству строк
        inputLines = new string[lineCount];

        // Заполняем массив inputLines
        for (int i = 0; i < lineCount; i++)
        {
            inputLines[i] = lines[i];
        }

        // Формируем строку для нумерации
        string lineNumberingText = "";
        for (int i = 0; i < lineCount; i++)
        {
            bool isLineCorrect = true;
            bool hasMatchingAnswer = false;

            if (i < lineResults.Count)
            {
                isLineCorrect = lineResults[i];
            }

            if (i < correctAnswers[currentTaskIndex].Length)
            {
                string userAnswer = inputLines[i].Trim();
                string correctAnswer = correctAnswers[currentTaskIndex][i].Trim();

                if (userAnswer.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase))
                {
                    hasMatchingAnswer = true;
                }
            }

            if (!isLineCorrect && !hasMatchingAnswer)
            {
                // Если строка не прошла проверку и не совпадает с элементом массива правильных ответов, применяем красный цвет.
                lineNumberingText += $"<color=red>{i + 1}</color>\n";
            }
            else
            {
                lineNumberingText += (i + 1) + "\n"; // В противном случае номер строки без форматирования
            }
        }
        lineNumberText.text = lineNumberingText;
    }

    public void CheckAnswer()
    {
        lineResults.Clear();
        bool isCorrect = true;

        string[] correctAnswer = correctAnswers[currentTaskIndex];
        int inputLineCount = inputLines.Length;
        int correctLineCount = correctAnswer.Length;

        if (inputLineCount == correctLineCount)
        {
            for (int i = 0; i < inputLineCount; i++)
            {
                bool isLineCorrect = inputLines[i] == correctAnswer[i];
                lineResults.Add(isLineCorrect);

                if (!isLineCorrect)
                {
                    isCorrect = false;
                }
            }
        }
        else
        {
            isCorrect = false;
            error.text = "Ошибка";

            for (int i = 0; i < inputLineCount; i++)
            {
                lineResults.Add(false);
            }
        }

        if (isCorrect && !taskProgress[currentTaskIndex])
        {
            error.text = "Правильно";
            taskProgress[currentTaskIndex] = true;
            progressManager.IncrementProgress();
        }

        userTextByTask[currentTaskIndex] = inputField.text;
        PlayerPrefs.SetString($"TaskText_{currentTaskIndex}", inputField.text);
        PlayerPrefs.Save();
        UpdateLineNumbers(inputField.text);
    }

    private void NextTask()
    {
        if (currentTaskIndex < taskDescriptions.Count - 1)
        {
            error.text = "";
            SetTask(currentTaskIndex + 1);
        }
    }

    private void PrevTask()
    {
        if (currentTaskIndex > 0)
        {
            error.text = "";
            SetTask(currentTaskIndex - 1);
        }
    }

    private void SetTask(int taskIndex)
    {
        if (taskIndex >= 0 && taskIndex < taskDescriptions.Count)
        {
            task.text = taskDescriptions[taskIndex];

            if (userTextByTask.ContainsKey(taskIndex))
            {
                inputField.text = userTextByTask[taskIndex];
            }
            else
            {
                inputField.text = "";
            }

            currentTaskIndex = taskIndex;
        }
    }

    private void CompleteTask()
    {
        if (progressManager.GetProgress() >= 0.99f)
        {
            Destroy(transform.parent.gameObject);
            hero.canMove = true;
            hero.canJump = true;
        }
        else
        {
            Debug.Log("Вы не выполнили все задания.");
        }
    }
}
