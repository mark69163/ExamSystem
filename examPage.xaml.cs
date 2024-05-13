using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using ExamSystem.Logic;
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


        //adatbazisunk modellje
        Model.Model _context;
        private LoggedInUser currentUser { get; }


        private string examName;
        List<QUESTION> relevantQuestions;


        //konstruktor
        public examPage(string examName, LoggedInUser user, int time_limit)
        {
            InitializeComponent();

            _context = new Model.Model();
            currentUser = user;

            this.examName=examName;

            loadExam();


        }

        //relevans vizsgak betoltese
        void loadExam()
        {

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

            mixQuestionAndAnswerOrder();

            lbQuestions.SelectedIndex = 0;

        }



        #region mixQeustions

        private void mixQuestionOrder() {

            List<QUESTION> mixedQuestions = new List<QUESTION>(relevantQuestions);
            Random rnd = new Random();

            int itemsToMix = mixedQuestions.Count;

            relevantQuestions.Clear();

            for (int i = 0; i < itemsToMix; i++)
            {

                int index = rnd.Next(0, mixedQuestions.Count);
                relevantQuestions.Add(mixedQuestions[index]);
                mixedQuestions.RemoveAt(index);
            }
        }

        private void mixAnswerOrder()
        {
            Random rnd = new Random();

            for (int i = 0; i < relevantQuestions.Count; i++)
            {
                QUESTION question = relevantQuestions[i];
                string[] answers = question.answers.Split(';');
                string[] mixedAnswers = new string[answers.Length];

                Array.Copy(answers, mixedAnswers, answers.Length);


                for (int j = mixedAnswers.Length - 1; j > 0; j--)
                {
                    int k = rnd.Next(0, j + 1);

                    string temp = mixedAnswers[j];
                    mixedAnswers[j] = mixedAnswers[k];
                    mixedAnswers[k] = temp;
                }

                for (int j = 0; j < mixedAnswers.Length; j++)
                {
                    int solutionIndex = question.solution;

                    if (mixedAnswers[j] == answers[solutionIndex-1])
                    {
                        question.solution = j;
                        break;
                    }

                }

                // megkavart valaszok visszatoltese
                string answersString = "";
                for (int j = 0; j < mixedAnswers.Length; j++)
                {
                    answersString += mixedAnswers[j];
                    answersString += ";";
                    //relevantQuestions[i].answers[j] = mixedAnswers[j];
                }
                answersString = answersString.Substring(0,answersString.Length-1);

                relevantQuestions[i].answers = answersString;
                relevantQuestions[i] = question;
            }
        }


        private void mixQuestionAndAnswerOrder()
        {
            mixQuestionOrder();
            mixAnswerOrder();
        }

        #endregion

        #region moveToNextQuestion
        private void moveToNextQuestion(int buttonPressed)
        {
            questionCounter++;


            if (questionCounter < relevantQuestions.Count)
            {
                if (buttonPressed == relevantQuestions[questionCounter-1].solution)
                {
                    correctCounter += relevantQuestions[questionCounter - 1].point_value;

                }
                lbQuestions.SelectedIndex = questionCounter;
            }


            //done with exam
            else
            {
                //get completion precentage
                int maxScore = 0;
                foreach (QUESTION question in relevantQuestions) maxScore+=question.point_value;

                float completion = correctCounter*100 / maxScore;

                //isert to db
                // az entity-vel nem mukodik...

                //manual check az 5-ös korzusra (villanytan)
                //UpdateResult("B2TN3S", 5, correctCounter*100/maxScore);

                try {
                    UpdateResult(currentUser.userName.ToString(), courseId, (int)completion);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Unable to update results for student.");

                }

                //Uri pageFunctionUri = new Uri("ExamsPage.xaml", UriKind.Relative);
                //this.NavigationService.Navigate(pageFunctionUri);
                this.NavigationService.Navigate(new ExamsPage(currentUser));
            }



        }

        public void UpdateResult(string neptunId, int courseId, int? result)
        {
            // Itt definiáljuk a kapcsolati sztringet
            string connectionString = "Server=localhost;Database=examSystem;Trusted_Connection=True; TrustServerCertificate=True";

            // SQL parancs, ami frissíti a meglévő rekord eredményét
            string sql = @"
        UPDATE STUDENTs_EXAMs
        SET result = @Result
        WHERE neptun_id = @NeptunId AND course_id = @CourseId";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Paraméterek hozzáadása a SQL parancshoz
                        command.Parameters.AddWithValue("@NeptunId", neptunId);
                        command.Parameters.AddWithValue("@CourseId", courseId);
                        command.Parameters.AddWithValue("@Result", (object)result ?? DBNull.Value); // Kezeljük a NULL értéket ha szükséges

                        // SQL parancs végrehajtása
                        int affectedRows = command.ExecuteNonQuery();
                        Console.WriteLine("Affected rows: {0}", affectedRows);

                        // Ellenőrizzük, hogy valóban frissült-e a rekord
                        if (affectedRows == 0)
                        {
                            Console.WriteLine("No record was updated. Please check if the student and course ID are correct.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
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

        private void btAnswer3_Click(object sender, RoutedEventArgs e)
        {
            moveToNextQuestion(3);
        }

        #endregion



        //a kivalasztott kerdes fuggvenyeben jelenitjuk meg a kerdeseket

        private void lbQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
     
            tbQestion.Text = relevantQuestions[lbQuestions.SelectedIndex].question;

            string[] answers = relevantQuestions[lbQuestions.SelectedIndex].answers.Split(';');

            tbAnswer0.Text = answers[0];
            tbAnswer1.Text = answers[1];
            tbAnswer2.Text = answers[2];
            tbAnswer3.Text = answers[3];
        }



    }
}
