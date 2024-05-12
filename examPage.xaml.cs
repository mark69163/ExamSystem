using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Model;

namespace ExamSystem
{
    /// <summary>
    /// Interaction logic for examPage.xaml
    /// </summary>
    public partial class examPage : Page
    {
        private int questionCounter = 0;
        public int correctCounter = 0;
        private int courseId = 0;


        Model.Model _context;


        /* /////////////////////////////// Test data setup ////////////////////////////////////////// */

        #region Test_data_setup

        private string[] questions = {  "Which electronic component disconnects a pin from the bus?",
                                        "Which logic gate does not exist?",
                                        "What is not included in the Arduino UNO R3 Developer Panel?",
                                        "Which is not a communication protocol?",
                                        "What PWM stands for in electronics?"
        };

        private int[] correctAnwers = { 3, 1, 0, 2, 1 };

        private string[] answers0 = { "H-Bridge", "Tri-state Buffer", "Operational Amplifier", "Schmitt Trigger" };
        private string[] answers1 = { "NOR", "XNOR", "AND", "XAND" };
        private string[] answers2 = { "Power Button", "Restart Button", "Built-in LED", "Power Supply Connector" };
        private string[] answers3 = { "I2C", "SPI", "NOP", "CAN" };
        private string[] answers4 = { "Parallel Wave Measurement",
                                      "Pulse Width Modulation",
                                      "Parallel Width Modulation",
                                      "Pulse Wave Modulation" };



        private List<string[]> Answers = new List<string[]>();


        private struct Question {
            public int correctAnswerIndex;
            public string questionToAnswer;
            public string[] answers;

        };

        private List<Question> Questions = new List<Question>();

        private TextBlock[] answerButtonTextBlocks;

        private void fillQuestions() {

            Answers.Add(answers0);
            Answers.Add(answers1); 
            Answers.Add(answers2); 
            Answers.Add(answers3);
            Answers.Add(answers4);

            for (int i=0;i<5;i++) { 
                Question question = new Question();
                question.correctAnswerIndex = correctAnwers[i];
                question.questionToAnswer = questions[i];
                question.answers = Answers[i];

                Questions.Add(question);
            }
            
        }

        private void displayQuestion() {
            for (int i=0;i<4;i++) {
                answerButtonTextBlocks[i].Text = Questions[questionCounter].answers[i];
            }

            tbQestion.Text = Questions[questionCounter].questionToAnswer;

        }
        #endregion
        private string examName;
        List<QUESTION> relevantQuestions;
        public examPage(string examName)
        {
            InitializeComponent();

            _context = new Model.Model();

            this.examName=examName;


            answerButtonTextBlocks = new TextBlock[] { tbAnswer0, tbAnswer1, tbAnswer2, tbAnswer3 };

            //fillQuestions();

            //mixQuestionOrder();
            //mixAnswerOrder();
            //mixQuestionAndAnswerOrder();


            //displayQuestion();

            //getting the course id
            List<EXAM> exams = _context.EXAMs.ToList();

            foreach (EXAM exam in exams)
            {
                if (exam.title == examName)
                {
                    courseId = exam.course_id; break;
                }
            }


            //getting a list of relevant questions
            List<QUESTION> questions = _context.QUESTIONs.ToList();
            this.relevantQuestions = new List<QUESTION>();

            foreach (QUESTION question in questions)
            {
                if (question.course_id == courseId)
                {
                    relevantQuestions.Add(question);
                }
            }
            questions.Clear();


            //hozzaadjuk a listboxhoz a kerdeseket
            for (int i = 1; i <= relevantQuestions.Count; i++)
            {
                lbQuestions.Items.Add($"Question {i}");
            }




        }




        private void mixQuestionOrder() {
            List<Question> mixedQuestions = new List<Question>(Questions);
            Random rnd = new Random();

            int itemsToMix = mixedQuestions.Count;

            Questions.Clear();

            for (int i = 0; i < itemsToMix; i++) {

               int index = rnd.Next(0, mixedQuestions.Count);
               Questions.Add(mixedQuestions[index]);
               mixedQuestions.RemoveAt(index);
            }

        }

        private void mixAnswerOrder()
        {
            Random rnd = new Random();

            for (int i = 0; i < Questions.Count; i++)
            {
                Question question = Questions[i];
                string[] mixedAnswers = new string[question.answers.Length];
                Array.Copy(question.answers, mixedAnswers, question.answers.Length);

                
                for (int j = mixedAnswers.Length - 1; j > 0; j--)
                {
                    int k = rnd.Next(0, j + 1);

                    string temp = mixedAnswers[j];
                    mixedAnswers[j] = mixedAnswers[k];
                    mixedAnswers[k] = temp;
                }

                for (int j = 0; j < mixedAnswers.Length; j++)
                {
                    if (mixedAnswers[j] == question.answers[question.correctAnswerIndex])
                    {
                        question.correctAnswerIndex = j;
                        break;
                    }
                    
                }

                for (int j = 0; j < mixedAnswers.Length; j++) {
                    Questions[i].answers[j]= mixedAnswers[j];
                }

                Questions[i]=question;
            }
        }


        private void mixQuestionAndAnswerOrder()
        {
            mixQuestionOrder();
            mixAnswerOrder();
        }


        
        private void btAnswer0_Click(object sender, RoutedEventArgs e)
        {
           // moveToNextQuestion(0);

        }
        private void btAnswer1_Click(object sender, RoutedEventArgs e)
        {
            //moveToNextQuestion(1);

        }

        private void btAnswer2_Click(object sender, RoutedEventArgs e)
        {
           // moveToNextQuestion(2);

        }

        //correct answer
        private void btAnswer3_Click(object sender, RoutedEventArgs e)
        {
            //moveToNextQuestion(3);
        }

        private void lbQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
       

            //a kivalasztott kerdes fuggvenyeben jelenitjuk meg a kerdeseket
     
            tbQestion.Text = relevantQuestions[lbQuestions.SelectedIndex].question;

            string[] answers = relevantQuestions[lbQuestions.SelectedIndex].answers.Split(';');

            tbAnswer0.Text = answers[0];
            tbAnswer1.Text = answers[1];
            tbAnswer2.Text = answers[2];
            tbAnswer3.Text = answers[3];

            

        }
    }
}
