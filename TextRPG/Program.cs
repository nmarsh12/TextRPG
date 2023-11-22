using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameLogic gameManager = new GameLogic();
            Task loop = new Task(gameManager.DoGameLoop);
            loop.Start();

            while (gameManager.running)
            {
                gameManager.AddInputsToQueue();
                Console.WriteLine(gameManager.inputQueue[gameManager.inputQueue.Count - 1].Key);
            }
        }
    }

    class GameLogic
    {
        int FRAMERATE = 2;
        
        public bool running = true;
        public bool inLoop = false;
        public List<ConsoleKeyInfo> inputQueue = new List<ConsoleKeyInfo>();

        void GameLoop()
        {
            Console.WriteLine("Executes Code During Loop");
        }

        public void DoGameLoop() 
        {
            double deltaTime = (1 / FRAMERATE) * 1000;
            while (running)
            {
                if (!inLoop)
                {
                    inLoop = true;
                    GameLoop();
                    Thread.Sleep((int)deltaTime); // currently doesn't work if framerate is over 1 for some reason
                    inLoop = false;
                }
            }
        }

        public static ConsoleKeyInfo GetInput()
        {
            return Console.ReadKey(true);
        }

        public void AddInputsToQueue()
        {
            inputQueue.Add(GetInput());
        }
    }
}
