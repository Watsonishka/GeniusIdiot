using System;
using System.Text;
namespace GeniusIdiotConsoleApp
{
    public class Program
    {
        public static void Main()
        {
            int questionsCount = 5;
            int diagnosesCount = 6;

            string userName = GetUserName();
            string[] questions = GetQuestions(questionsCount);
            int[] answers = GetAnswers(questionsCount);
            string[] diagnoses = GetDiagnoses(diagnosesCount);

            PlayMainQuiz(questions, answers, diagnoses, userName);
        }

        private static string[] GetQuestions(int questionsCount)
        {
            string[] questions = new string[questionsCount];
            questions[0] = "Сколько будет два плюс два умноженное на два?";
            questions[1] = "Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?";
            questions[2] = "На двух руках 10 пальцев. Сколько пальцев на 5 руках?";
            questions[3] = "Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?";
            questions[4] = "Пять свечей горело, две потухли. Сколько свечей осталось?";

            return questions;
        }

        private static int[] GetAnswers(int questionsCount) 
        {
            int[] answers = new int[questionsCount];
            answers[0] = 6;
            answers[1] = 9;
            answers[2] = 25;
            answers[3] = 60;
            answers[4] = 2;

            return answers;
        }

        private static string[] GetDiagnoses(int diagnosesCount)
        {
            string[] diagnoses = new string[diagnosesCount];
            diagnoses[0] = "Идиот";
            diagnoses[1] = "Кретин";
            diagnoses[2] = "Дурак";
            diagnoses[3] = "Нормальный";
            diagnoses[4] = "Талант";
            diagnoses[5] = "Гений";

            return diagnoses;
        }

        private static int GetUserAnswer() 
        {
            int userAnswer = 0;

            while (true)
            {
                try
                {
                    userAnswer = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine($"Некорректный ввод. Нужно ввести целое число от {int.MinValue} до {int.MaxValue}. Попробуйте снова!"); 
                }
            }

            return userAnswer;
        }

        private static int[] GetRandomIndexes(int questionsCount) // функция, позволяющая реализовать рандомизацию вопросов без повторений с помощью алгоритма Фишера-Йетса
        {
            int[] randomIndexes = new int[questionsCount];

            Random randomizer = new Random();

            for (int i = 0; i < questionsCount; i++) 
            {
                randomIndexes[i] = i;
            }

            for (int i = randomIndexes.Length - 1; i > 0; i--) 
            {
                int index = randomizer.Next(i + 1);
                int temp = randomIndexes[index];
                randomIndexes[index] = randomIndexes[i];
                randomIndexes[i] = temp;
            }

            return randomIndexes;
        }

        private static void PlayMainQuiz(string[] questions, int[] answers, string[] diagnoses, string userName)
        {
            bool isGameContinue = true;
            
            while (isGameContinue)
            {
                int rightAnswersCount = 0;
                int[] randomIndexes = GetRandomIndexes(questions.Length);

                for (int i = 0; i < questions.Length; i++)
                {
                    Console.WriteLine($"Вопрос №{i + 1}:");

                    int randomQuestionIndex = randomIndexes[i];

                    Console.WriteLine(questions[randomQuestionIndex]);

                    int userAnswer = GetUserAnswer();

                    int rightAnswer = answers[randomQuestionIndex];

                    if (userAnswer == rightAnswer)
                    {
                        rightAnswersCount++;
                    }
                }                

                Console.WriteLine($"Количество правильных ответов: {rightAnswersCount}");
                Console.WriteLine($"{userName}, твой диагноз: {diagnoses[rightAnswersCount]}");
                Console.WriteLine("Поздравляю! Игра закончена! Желаешь попробовать еще раз? Введи \"Да\" или \"Нет\"");

                if (!GetUserConfirmation())
                {
                    isGameContinue = false;

                    Console.WriteLine($"{userName}, игра окончена! До новых встреч!");
                }                
            }
        }

        private static string GetUserName()
        {
            bool isNameConfirmed = false;
            string userName = "";

            Console.Write("Ты попал в консольную игру \"Гений-Идиот\"! ");

            while (!isNameConfirmed)
            {
                Console.WriteLine("Пожалуйста, введи свое имя:");
                bool isNameValid = false;

                while (!isNameValid)
                {
                    userName = Console.ReadLine();
                    isNameValid = CheckNameIsValid(userName);
                }

                userName = NormalizeName(userName);

                Console.WriteLine($"Подтвердите имя: {userName}. Введите \"Да\" или \"Нет\".");
                isNameConfirmed = GetUserConfirmation();
            }
                Console.WriteLine($"Приветствую тебя, {userName}! Твое имя зафиксировано. Давай приступим к вопросам!");

                return userName;
        }

        private static bool CheckNameIsValid(string inputName) 
        {
            if (string.IsNullOrEmpty(inputName))
            {
                Console.WriteLine("Некорректный ввод, ты ввел пустое значение! Пожалуйста, попробуй снова!");
                return false;
            }

            if (!char.IsLetter(inputName[0])) 
            {
                Console.WriteLine("Некорректный ввод, первый символ должен быть буквой! Пожалуйста, попробуй снова!");
                return false;
            }

            for (int i = 0; i < inputName.Length; i++)
            {
                if (!char.IsLetter(inputName[i]) && inputName[i] != '-' && inputName[i] != ' ') 
                {
                    Console.WriteLine("Некорректный ввод! Имя может содержать только буквы, пробелы и тире. Попробуйте снова!");
                    return false;
                }
            }

            return true;
        }

        private static string NormalizeName(string inputName) 
        {
            inputName = inputName.ToLower().Trim(' ','-'); 

            StringBuilder userName = new StringBuilder();

            bool previousCharWasSymbol = false;

            for (int i = 0; i < inputName.Length; i++)
            {
                char currentChar = inputName[i];

                if ((currentChar == ' ' || currentChar == '-') && previousCharWasSymbol) 
                {
                    continue;
                }
                if (i == 0 || previousCharWasSymbol) 
                {
                    if (char.IsLetter(currentChar))
                    {
                        userName.Append(char.ToUpper(currentChar));
                    }
                    else
                    {
                        userName.Append(currentChar);
                    }
                }
                else
                {
                    userName.Append(currentChar);
                }

                previousCharWasSymbol = currentChar == ' ' || currentChar == '-'; 
            }

            return userName.ToString();
        }

        private static bool GetUserConfirmation()
        {
            string userAnswer = "";

            while (true)
            {
                userAnswer = Console.ReadLine().ToLower().Trim();

                if (string.IsNullOrEmpty(userAnswer))
                {
                    Console.WriteLine("Некорректный ввод, ты ввел пустое значение! Пожалуйста, попробуй снова!");
                }
                else
                {
                    if (userAnswer == "да")
                    {
                        return true;
                    }
                    else if (userAnswer == "нет")
                    {
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод! Пожалуйста, введи в консоль \"Да\" или \"Нет\"!");
                    }
                }              
            }
        }        
    }
}

