using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Windows;
using TriviaProject.Model;

public class GameLogic
{
    private List<Question> questions;
    private int currentQuestionIndex;
    private int correctAnswersCount;
    private Random random;
    private string category;

    public GameLogic(string category)
    {
        random = new Random();
        questions = new List<Question>();
        currentQuestionIndex = 0;
        correctAnswersCount = 0;
        this.category = category;
        InitializeQuestions();
    }

    private void InitializeQuestions()
    {
        using (SQLiteConnection connection = new SQLiteConnection(@"Data Source=C:\Users\User\Downloads\DBAllUsers.db;"))
        {
            connection.Open();

            // Fetch unique question IDs based on the selected category
            string questionIdsQuery = "SELECT DISTINCT QuestionID FROM Questions WHERE Category = @Category;";
            List<int> questionIds = new List<int>();

            using (SQLiteCommand command = new SQLiteCommand(questionIdsQuery, connection))
            {
                command.Parameters.AddWithValue("@Category", category);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int questionId = Convert.ToInt32(reader["QuestionID"]);
                        questionIds.Add(questionId);
                    }
                }
            }

            // Shuffle the question IDs
            Shuffle(questionIds);

            // Select the first 10 question IDs (or less if there are fewer than 10 available)
            List<int> selectedQuestionIds = questionIds.Take(10).ToList();

            // Fetch questions and answers for the selected question IDs
            foreach (int questionId in selectedQuestionIds)
            {
                FetchQuestionAndAnswers(connection, questionId);
            }
        }
    }

    private void FetchQuestionAndAnswers(SQLiteConnection connection, int questionId)
    {
        string questionQuery = "SELECT Question, Category FROM Questions WHERE QuestionId = @QuestionId;"; // Change "QuestionText" to "Question"
        List<Answer> answers = new List<Answer>();

        using (SQLiteCommand command = new SQLiteCommand(questionQuery, connection))
        {
            command.Parameters.AddWithValue("@QuestionId", questionId);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    Question question = new Question
                    {
                        QuestionId = questionId,
                        QuestionText = reader["Question"].ToString(), // Change "QuestionText" to "Question"
                        Category = reader["Category"].ToString()
                    };

                    question.PossibleAnswers = FetchAnswers(connection, questionId);
                    questions.Add(question);
                }
            }
        }
    }


    private List<Answer> FetchAnswers(SQLiteConnection connection, int questionId)
    {
        List<Answer> answers = new List<Answer>();

        string answerQuery = "SELECT Answer, IsCorrect FROM Answers WHERE QuestionID = @QuestionID;";
        using (SQLiteCommand command = new SQLiteCommand(answerQuery, connection))
        {
            command.Parameters.AddWithValue("@QuestionID", questionId);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    

                    Answer answer = new Answer
                    {
                        AnswerText = reader["Answer"].ToString(),
                        IsCorrect = Convert.ToBoolean(reader["IsCorrect"])
                    };
                    answers.Add(answer);
                }
            }
        }

        return answers;
    }



    public Question GetNextQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            return questions[currentQuestionIndex++];
        }
        return null;
    }

    public bool IsCorrectAnswer(string selectedAnswer)
    {
        if (currentQuestionIndex > 0 && currentQuestionIndex <= questions.Count)
        {
            List<Answer> possibleAnswers = questions[currentQuestionIndex - 1].PossibleAnswers;
            foreach (var answer in possibleAnswers)
            {
                if (answer.AnswerText == selectedAnswer)
                {
                    if (answer.IsCorrect)
                    {
                        correctAnswersCount++;
                        return true;
                    }
                    else
                    {

                        return false;
                    }
                }
            }
            
        }
        MessageBox.Show("Invalid question index!");
        return false;
    }




    public int GetCorrectAnswersCount()
    {
        return correctAnswersCount;
    }

    public int GetTotalQuestions()
    {
        return questions.Count;
    }

    private void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
