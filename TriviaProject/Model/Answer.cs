using System.ComponentModel;

namespace TriviaProject.Model
{
    public class Answer : INotifyPropertyChanged
    {
        private int answerId;
        private string answerText;
        private int questionId;
        private bool isCorrect;

        public int AnswerId
        {
            get { return answerId; }
            set
            {
                if (answerId != value)
                {
                    answerId = value;
                    OnPropertyChanged(nameof(AnswerId));
                }
            }
        }

        public string AnswerText
        {
            get { return answerText; }
            set
            {
                if (answerText != value)
                {
                    answerText = value;
                    OnPropertyChanged(nameof(AnswerText));
                }
            }
        }

        public int QuestionId
        {
            get { return questionId; }
            set
            {
                if (questionId != value)
                {
                    questionId = value;
                    OnPropertyChanged(nameof(QuestionId));
                }
            }
        }

        public bool IsCorrect
        {
            get { return isCorrect; }
            set
            {
                if (isCorrect != value)
                {
                    isCorrect = value;
                    OnPropertyChanged(nameof(IsCorrect));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
