using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaProject.Model
{
    public class Question : INotifyPropertyChanged
    {
        private int questionId;
        private string questionText;
        private string category;
        private List<Answer> possibleAnswers;
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

        public List<Answer> PossibleAnswers
        {
            get { return possibleAnswers; }
            set
            {
                if (possibleAnswers != value)
                {
                    possibleAnswers = value;
                    OnPropertyChanged(nameof(PossibleAnswers));
                }
            }
        }
        public string QuestionText
        {
            get { return questionText; }
            set
            {
                if (questionText != value)
                {
                    questionText = value;
                    OnPropertyChanged(nameof(QuestionText));
                }
            }
        }

        public string Category
        {
            get { return category; }
            set
            {
                if (category != value)
                {
                    category = value;
                    OnPropertyChanged(nameof(Category));
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
