using System;

namespace MyFirstApp
{
    class Program
    {
        const string backgroundSymbol = "S";
        const string secureSymbol = "C";
        const int consoleHeight = 100;
        const int consoleWidth = 200;
        const int maxBackgroundSymbolCount = consoleWidth * consoleHeight;
        const int attemptsCount = 3;

        static int secureSymbolCount = 0;
        static readonly Random randomGenerator = new Random();

        static void Main()
        {
            StartGame();
        }

        static void GenerateSecureSymbolCount()
        {
            secureSymbolCount = randomGenerator.Next(1, Math.Min(consoleWidth, consoleHeight));
        }

        static int GetSecureSymbolPosition(int minValue, int maxValue)
        {
            return randomGenerator.Next(minValue, maxValue);
        }

        static void FillField()
        {
            GenerateSecureSymbolCount();

            int nextSecureSymbolPosition = -1;
            int lengthBetweenSecureSymbol = 0;
            int maxLengthBetweenSecureSymbol = maxBackgroundSymbolCount / secureSymbolCount;
            int currentSecureSymbolCount = 0;
            

            for (int symbolIndex = 0; symbolIndex < maxBackgroundSymbolCount; symbolIndex++)
            {
                if (lengthBetweenSecureSymbol <= symbolIndex && currentSecureSymbolCount < secureSymbolCount)
                {
                    lengthBetweenSecureSymbol = maxLengthBetweenSecureSymbol + symbolIndex;
                    nextSecureSymbolPosition = GetSecureSymbolPosition(symbolIndex, lengthBetweenSecureSymbol);
                    currentSecureSymbolCount++;
                };

                string currentSymbol = nextSecureSymbolPosition == symbolIndex ? secureSymbol : backgroundSymbol;

                Console.Write(currentSymbol);
            }

            Console.WriteLine();
        }

        static void Play()
        {
            Console.WriteLine("Какое количество символов «{0}» вы нашли?", secureSymbol);

            int currentAttempt = 0;
            bool answerIsCorrect = false;

            while (attemptsCount > currentAttempt && !answerIsCorrect)
            {
                try
                {
                    answerIsCorrect = Int32.Parse(Console.ReadLine()) == secureSymbolCount;
                }
                catch (Exception)
                {
                    answerIsCorrect = false;
                }
                
                currentAttempt++;

                if (!answerIsCorrect)
                {
                    Console.WriteLine("Ответ не верный, количество попыток {0}", attemptsCount - currentAttempt);
                }
            }

            string result = answerIsCorrect ? "Поздравляю, вы правильно посчитали." : "К сожалению, вы посчитали неправильно.";

            Console.WriteLine(result);
            Console.Write("Хотите сыграть еще? Да/Нет(по умолчанию):");

            string userAnswer = Console.ReadLine().Trim().ToUpper();

            if (userAnswer == "ДА")
            {
                StartGame();

                return;
            }

            EndGame();
        }

        static void StartGame()
        {
            ResetGame();

            Console.WriteLine("Приветствую тебя в игре «Найди символ {0}»", secureSymbol);

            FillField();
            Play();
        }

        static void EndGame()
        {
            ResetGame();

            Console.WriteLine("С вами было интересно возвращайтесь еще!");
        }

        static void ResetGame()
        {
            Console.Clear();
            secureSymbolCount = 0;
        }
    }
}
