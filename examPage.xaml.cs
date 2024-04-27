using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
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

namespace ExamSystem
{
    /// <summary>
    /// Interaction logic for examPage.xaml
    /// </summary>
    public partial class examPage : Page
    {
        private int questionCounter = 0;
        public int correctCounter = 0;



        /* /////////////////////////////// Test data setup ////////////////////////////////////////// */

        #region Test_data_setup

        private string[] questions = {  "Which electronic component disconnects a pin from the bus?",
                                        "Which logic gate does not exist?",
                                        "What is not included in the Arduino UNO R3 Developer Panel?",
                                        "Which is not a communication protocol?",
                                        "What PWM stands for in electronics?"
        };

        private int[] correctAnwers = { 3, 3, 0, 2, 1 };

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

        public examPage()
        {
            InitializeComponent();

            answerButtonTextBlocks = new TextBlock[] { tbAnswer0, tbAnswer1, tbAnswer2, tbAnswer3 };

            fillQuestions();
            mixQuestionOrder();
            displayQuestion();

        }


        private void mixQuestionOrder() {
            List<Question> mixedQuestions = new List<Question>(Questions);
            
            int itemsToMix = mixedQuestions.Count;

            Questions.Clear();

            for (int i = 0; i < itemsToMix; i++) {
                Random rnd = new Random();

                int index = rnd.Next(0, mixedQuestions.Count);
               Questions.Add(mixedQuestions[index]);
               mixedQuestions.RemoveAt(index);
            }

        }

        private void mixAnswerOrder()
        {


        }

        private void mixQuestionAndAnswerOrder()
        {
            mixQuestionOrder();
            mixAnswerOrder();
        }

        private void moveToNextQuestion(int buttonPressed)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();

            // correct answer
            //if (buttonPressed == correctAnwers[questionCounter])
            if (buttonPressed == Questions[questionCounter].correctAnswerIndex)

                {
                    bitmap.UriSource = new Uri("/img/examStateSuccess.png", uriKind: UriKind.Relative);
                    bitmap.EndInit();

                    correctCounter++;

            }

            //incorrect answer
            else
            {
                bitmap.UriSource = new Uri("/img/examStateFailed.png", uriKind: UriKind.Relative);
                bitmap.EndInit();
            }

            switch (questionCounter)
            {
                case 0:
                    imQuestionState0.Source = bitmap;
                    frQuestionSelected0.Background = new SolidColorBrush(Colors.Wheat);
                    frQuestionSelected1.Background = new SolidColorBrush(Colors.DarkBlue);
                    questionCounter++;

                    break;

                case 1:
                    imQuestionState1.Source = bitmap;
                    frQuestionSelected1.Background = new SolidColorBrush(Colors.Wheat);
                    frQuestionSelected2.Background = new SolidColorBrush(Colors.DarkBlue);
                    questionCounter++;

                    break;

                case 2:
                    imQuestionState2.Source = bitmap;
                    frQuestionSelected2.Background = new SolidColorBrush(Colors.Wheat);
                    frQuestionSelected3.Background = new SolidColorBrush(Colors.DarkBlue);
                    questionCounter++;

                    break;

                case 3:
                    imQuestionState3.Source = bitmap;
                    frQuestionSelected3.Background = new SolidColorBrush(Colors.Wheat);
                    frQuestionSelected4.Background = new SolidColorBrush(Colors.DarkBlue);
                    questionCounter++;

                    break;

                case 4:
                    imQuestionState4.Source = bitmap;

                    Uri pageFunctionUri = new Uri("ExamsPage.xaml", UriKind.Relative);
                    this.NavigationService.Navigate(pageFunctionUri);

                    break;

                default:
                    imQuestionState0.Source = bitmap;
                    frQuestionSelected4.Background = new SolidColorBrush(Colors.Wheat);
                    frQuestionSelected0.Background = new SolidColorBrush(Colors.DarkBlue);

                    break;
            };

            displayQuestion();
        }

        private void btAnswer0_Click(object sender, RoutedEventArgs e)
        {
            moveToNextQuestion(0);

        }
        private void btAnswer1_Click(object sender, RoutedEventArgs e)
        {
            moveToNextQuestion(1);

        }

        private void btAnswer2_Click(object sender, RoutedEventArgs e)
        {
            moveToNextQuestion(2);

        }

        //correct answer
        private void btAnswer3_Click(object sender, RoutedEventArgs e)
        {
            moveToNextQuestion(3);
        }


    }
}
