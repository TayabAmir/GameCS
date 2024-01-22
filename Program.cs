using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using EZInput;

namespace Game
{
    internal class Program
    {
        static int[] pfX = new int[1000];
        static int[] pfY = new int[1000];
        static int pfCount = 0;
        static bool[] isPlayerBulletActive = new bool[100];

        static int[] efX = new int[1000];
        static int[] efY = new int[1000];
        static int efCount = 0;
        static bool[] isEnemyBulletActive = new bool[1000];

        static int[] efX2 = new int[1000];
        static int[] efY2 = new int[1000];
        static int efCount2 = 0;
        static bool[] isEnemyBulletActive2 = new bool[1000];

        static int[] efX3 = new int[1000];
        static int[] efY3 = new int[1000];
        static int efCount3 = 0;
        static bool[] isEnemyBulletActive3 = new bool[1000];

        static int[] efX_random = new int[1000];
        static int[] efY_random = new int[1000];
        static int efCount_random = 0;
        static bool[] isEnemyBulletActive_random = new bool[1000];

        static char[,] Maze = new char[30, 110]
        {
            {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
            {'#','|','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|','_','_','_','_','_','_','_',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','_','_','_','_','_','_','|','#'},
            {'#','|','_','_','_','_','_','_','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','_','_','_','_','_','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','|',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','|','#'},
            {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'}
        };
        static int ex1 = 4, ey1 = 9,ex2 = 70, ey2 = 9,ex3 = 45, ey3 = 9,px = 47, py = 32,fx = 0, fy = 0,e1Health = 5, e2Health = 5, e3Health = 5, playerHealth = 10;

        static void Main(string[] args)
        {
            int option, gameOption;
            do
            {
                Console.Clear();
                printHeader();
                displayMenu();
                option = int.Parse(Console.ReadLine());
                if (option == 1)
                {
                    do
                    {
                        Console.Clear();
                        printHeader();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Choos the difficulty level...");
                        Console.WriteLine(" 1. Easy ");
                        Console.WriteLine(" 2. Medium ");
                        Console.WriteLine(" 0. Return to Main Menu");
                        Console.WriteLine("Choose the option... ");

                        gameOption = int.Parse(Console.ReadLine());
                        switch (gameOption)
                        {
                            case 1:
                                Game(50);
                                Console.SetCursorPosition(113, 15);
                                Console.WriteLine("Enter any key to go back...");
                                Console.Read();
                                break;
                            case 2:
                                Game(1);
                                Console.SetCursorPosition(113, 15);
                                Console.WriteLine("Enter any key to go back...");
                                Console.Read();
                                break;
                            case 0:
                                break;

                            default:

                                Console.WriteLine("Write correct option.");
                                break;

                        }
                    } while (gameOption != 0);
                }
                else if(option==2)
                {
                    displayInstructions();
                    Console.Read();
                }
                else if(option == 3)
                {
                    Console.Clear();
                    printHeader();
                    Console.WriteLine();
                    Console.Write("Thanks for playing");
                    Console.Read();
                }
            } while (option != 3);
        }
        static void printHeader()
        {
            Console.WriteLine("\t \t  ____  ____   _    ____ _____   ____  _____ _____ _   _ ____  _____ ____  ");
            Console.WriteLine("\t \t / ___||  _ \\ / \\  / ___| ____| |  _ \\| ____|  ___| \\ | |  _ \\| ____|  _ \\ ");
            Console.WriteLine("\t \t \\___ \\| |_) / _ \\| |   |  _|   | | | |  _| | |_  |  \\| | | | |  _| | |_) |");
            Console.WriteLine("\t \t  ___) |  __/ ___ \\|___ | |___  | |_| | |___|  _| | |\\  | |_| | |___|  _ < ");
            Console.WriteLine("\t \t |____/|_| /_/   \\_\\____|_____| |____/|_____|_|   |_| \\_|____/|_____|_| \\_\\");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------  ");
            Console.WriteLine();
        }
        static void displayMenu()
        {
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Instructions");
            Console.WriteLine("3. Exit");
            Console.WriteLine("====================================");
            Console.WriteLine("Choose the option you want...");
        }
        static void displayInstructions()
        {
                Console.Clear();
                printHeader();
                Console.SetCursorPosition(25, 9);
                Console.Write("====================================");
                Console.SetCursorPosition(25, 10);
                Console.Write("           GAME INSTRUCTIONS         ");
                Console.SetCursorPosition(25, 11);
                Console.Write("====================================");
                Console.SetCursorPosition(25, 13);
                Console.Write("Use ^ to move up.");
                Console.SetCursorPosition(25, 14);
                Console.Write("Use > to move right.");
                Console.SetCursorPosition(25, 15);
                Console.Write("Use < to move left.");
                Console.SetCursorPosition(25, 16);
                Console.Write("Use v to move down.");
                Console.SetCursorPosition(25, 17);
                Console.Write("Press the spacebar to shoot.");
                Console.SetCursorPosition(25, 18);
                Console.Write("Avoid enemy bullets and collision with them");
                Console.SetCursorPosition(25, 19);
                Console.Write("Survive as long as you can!");
                Console.SetCursorPosition(25, 21);
                Console.WriteLine("====================================");
         }
        static void printMaze()
        {
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 110; j++)
                {
                    Console.Write(Maze[i,j]);
                }
                Console.WriteLine();
            }
        }

        static void Game(int value)
        {
            Console.Clear();
            printHeader();
            printMaze();
            printEnemy1();
            printEnemy2();
            printEnemy3();
            printPlayer();
            string enemyDirection = "right", enemyDirection2 = "right", vdirection = "down";

            while (true)
            {
                if (Keyboard.IsKeyPressed(Key.LeftArrow))
                {
                    movePlayerLeft();
                }
                if (Keyboard.IsKeyPressed(Key.RightArrow))
                {
                    movePlayerRight();
                }
                if (Keyboard.IsKeyPressed(Key.UpArrow))
                {
                    movePlayerUp();
                }
                if (Keyboard.IsKeyPressed(Key.DownArrow))
                {
                    movePlayerDown();
                }
                if (Keyboard.IsKeyPressed(Key.Space))
                {
                    generatePlayerFire();
                }

                if (value == 1)
                {
                    generateRandomFire();
                    moveRandomFire();
                }

                movePlayerFire();

                if (e1Health > 0)
                {
                    moveEnemy1(enemyDirection);
                    enemyDirection = changeDirection(enemyDirection);
                    Thread.Sleep(value);
                    generateEnemy1Fire();
                }
                else
                {
                    eraseEnemy1();
                }

                moveEnemy1Fire();

                if (e2Health > 0)
                {
                    moveEnemy2(enemyDirection2);
                    enemyDirection2 = changeDirection(enemyDirection2);
                    generateEnemy2Fire();
                    Thread.Sleep(value);
                }
                else
                {
                    eraseEnemy2();
                }

                moveEnemy2Fire();

                if (e3Health > 0)
                {
                    moveEnemy3(vdirection);
                    vdirection = changevDirection(vdirection);
                    generateEnemy3Fire();
                    Thread.Sleep(value);
                }
                else
                {
                    eraseEnemy3();
                }

                moveEnemy3Fire();

                printScore();

                if (playerHealth < 0)
                {
                    playerHealth = 0;
                }

                if (e1Health < 0)
                {
                    e1Health = 0;
                }

                if (e2Health < 0)
                {
                    e2Health = 0;
                }

                if (e3Health < 0)
                {
                    e3Health = 0;
                }

                if (e1Health == 0 && e2Health == 0 && e3Health == 0)
                {
                    ex1 = 4; ey1 = 9; ex2 = 70; ey2 = 9; ex3 = 45; ey3 = 9; px = 47; py = 32; fx = 0; fy = 0; e1Health = 5; e2Health = 5; e3Health = 5; playerHealth = 10; pfCount = 0; efCount = 0; efCount2 = 0; efCount3 = 0;
                    Console.SetCursorPosition(115, 14);
                    Console.WriteLine("Game Over! You Win.");
                    break;
                }
                else if (playerHealth == 0)
                {
                    ex1 = 4; ey1 = 9; ex2 = 70; ey2 = 9; ex3 = 45; ey3 = 9; px = 47; py = 32; fx = 0; fy = 0; e1Health = 5; e2Health = 5; e3Health = 5; playerHealth = 10; pfCount = 0; efCount = 0; efCount2 = 0; efCount3 = 0;
                    erasePlayer();
                    Console.SetCursorPosition(115, 14);
                    Console.WriteLine("Game Over! You Lose");
                    break;
                }
            }
        }

        static string changevDirection(string vdirection)
        {
            if (vdirection == "up" && ey3 <= 10)
            {
                vdirection = "down";
            }
            else if (vdirection == "down" && ey3 + 2 >= 23)
            {
                vdirection = "up";
            }
            return vdirection;
        }
        static string changeDirection(string direction)
        {
            if (direction == "right" && ex1 >= 38)
            {
                direction = "left";
            }
            if (direction == "left" && ex1 <= 4)
            {
                direction = "right";
            }
            if (direction == "right" && ex2 >= 104)
            {
                direction = "left";
            }
            if (direction == "left" && ex2 <= 52)
            {
                direction = "right";
            }
            return direction;
        }
        static void printEnemy1()
        {
            Console.SetCursorPosition(ex1, ey1);
            Console.Write("___");
            Console.SetCursorPosition(ex1, ey1 + 1);
            Console.Write("|~|");
            Console.SetCursorPosition(ex1, ey1 + 2);
            Console.Write(":*:");
        }

        static void eraseEnemy1()
        {
            Console.SetCursorPosition(ex1, ey1);
            Console.Write("   ");
            Console.SetCursorPosition(ex1, ey1 + 1);
            Console.Write("   ");
            Console.SetCursorPosition(ex1, ey1 + 2);
            Console.Write("   ");
        }

        static void printEnemy2()
        {
            Console.SetCursorPosition(ex2, ey2);
            Console.Write("___");
            Console.SetCursorPosition(ex2, ey2 + 1);
            Console.Write("|~|");
            Console.SetCursorPosition(ex2, ey2 + 2);
            Console.Write("(o)");
        }
        static void eraseEnemy2()
        {
            Console.SetCursorPosition(ex2, ey2);
            Console.Write("   ");
            Console.SetCursorPosition(ex2, ey2 + 1);
            Console.Write("   ");
            Console.SetCursorPosition(ex2, ey2 + 2);
            Console.Write("   ");
        }
        static void printEnemy3()
        {
            Console.SetCursorPosition(ex3, ey3);
            Console.Write("___");
            Console.SetCursorPosition(ex3, ey3 + 1);
            Console.Write("|~|");
            Console.SetCursorPosition(ex3, ey3 + 2);
            Console.Write("\\^/");
        }
       static void eraseEnemy3()
        {
            Console.SetCursorPosition(ex3, ey3);
            Console.Write("   ");
            Console.SetCursorPosition(ex3, ey3 + 1);
            Console.Write("   ");
            Console.SetCursorPosition(ex3, ey3 + 2);
            Console.Write("   ");
        }
        static void printScore()
        {
            Console.SetCursorPosition(115, 9);
            Console.Write("                            ");
            Console.SetCursorPosition(115, 9);
            Console.Write("Player's Health: " + playerHealth);

            Console.SetCursorPosition(115, 10);
            Console.Write("                            ");
            Console.SetCursorPosition(115, 10);
            Console.Write("Enemy 1 Health: " + e1Health);
            
            Console.SetCursorPosition(115, 11);
            Console.Write("                            ");
            Console.SetCursorPosition(115, 11);
            Console.Write("Enemy 2 Health: " + e2Health);

            Console.SetCursorPosition(115, 12);
            Console.Write("                            ");
            Console.SetCursorPosition(115, 12);
            Console.Write("Enemy 3 Health: " + e3Health);
        }
        static void printPlayer()
        {
            Console.SetCursorPosition(px, py);
            Console.WriteLine(" /~\\");
            Console.SetCursorPosition(px, py + 1);
            Console.WriteLine("_|~|_");
            Console.SetCursorPosition(px, py + 2);
            Console.WriteLine(" ~~~ ");
        }

        static void erasePlayer()
        {
            Console.SetCursorPosition(px, py);
            Console.WriteLine("     ");
            Console.SetCursorPosition(px, py + 1);
            Console.WriteLine("     ");
            Console.SetCursorPosition(px, py + 2);
            Console.WriteLine("     ");
        }

        static void moveEnemy1(string direction)
        {
            if (Maze[ey1,ex1+4] == '/' || Maze[ey1 + 1, ex1 + 3] == '_' || Maze[ey1 + 2, ex1 + 4] == '~' ||
            Maze[ey1,ex1-1] == '\\' || Maze[ey1 + 1, ex1 - 1] == '_' || Maze[ey1 + 1, ex1 - 1] == '~')
            {
                playerHealth--;
            }
            else
            {
                eraseEnemy1();
                if (direction == "right")
                {
                    ex1 = ex1 + 1;
                }
                if (direction == "left")
                {
                    ex1 = ex1 - 1;
                }
                printEnemy1();
            }
        }

        static void moveEnemy2(string direction)
        {
            if (Maze[ey1,ex2+3] == '/' || Maze[ey1 + 1, ex2 + 3] == '_' || Maze[ey1 + 2, ex2 + 3] == '~' ||
            Maze[ey1, ex2-1] == '\\' || Maze[ey1 + 1, ex2 - 2] == '_' || Maze[ey1 + 1, ex2 - 2] == '~')
            {
                playerHealth--;
            }
            else
            {
                eraseEnemy2();
                if (direction == "right")
                {
                    ex1 = ex1 + 1;
                }
                if (direction == "left")
                {
                    ex1 = ex1 - 1;
                }
                printEnemy2();
            }
        }

        static void moveEnemy3(string vdirection)
        {
            if (Maze[ey3 + 3, ex3] == '/' || Maze[ey3 + 3, ex3 + 1] == '~' || Maze[ey3 + 3, ex3 + 2] == '\\' ||
            Maze[ey3 - 1, ex3] == '~' || Maze[ey3 - 1, ex3 + 1] == '~' || Maze[ey3 - 1, ex3 + 2] == '~')
            {
                playerHealth--;
            }
            else
            {
                eraseEnemy3();
                if (vdirection == "down")
                {
                    ey3 = ey3 + 1;
                }
                if (vdirection == "up")
                {
                    ey3 = ey3 - 1;
                }
                printEnemy3();
            }
        }
        static void movePlayerLeft()
        {
            if (Maze[py, px - 1] == ' ' && Maze[py + 1, px - 1] == ' ' && Maze[py + 2, px - 1] == ' ')
            {
                erasePlayer();
                px = px - 1;
                printPlayer();
            }
        }
        static void movePlayerRight()
        {
            if (Maze[py, px + 6] == ' ' && Maze[py + 2, px + 6] == ' ' && Maze[py + 2, px + 6] == ' ')
            {
                erasePlayer();
                px = px + 1;
                printPlayer();
            }
        }
        static void movePlayerUp()
        { 
            if (Maze[py - 1, px] == ' ' && Maze[py - 1, px + 1] == ' ' && Maze[py - 1, px + 2] == ' ' && Maze[py - 1, px + 3] == ' ' && Maze[py - 1, px + 4] == ' ')
            {
                erasePlayer();
                py = py - 1;
                printPlayer();
            }
        }
        static void movePlayerDown()
        {
            if (Maze[py + 3, px] == ' ' && Maze[py + 3, px + 1] == ' ' && Maze[py + 3, px + 2] == ' ' && Maze[py + 3, px + 3] == ' ' && Maze[py + 3, px + 4] == ' ')
            {
                erasePlayer();
                py = py + 1;
                printPlayer();
            }
        }

        static void generatePlayerFire()
        {
            pfX[pfCount] = px + 2;
            pfY[pfCount] = py - 1;

            if (Maze[pfY[pfCount], pfX[pfCount]] == ' ')
            {
                Console.SetCursorPosition(pfX[pfCount], pfY[pfCount]);
                Console.Write(".");

                isPlayerBulletActive[pfCount] = true;
                pfCount++;
            }
        }
        static void movePlayerFire()
        {
            for (int i = 0; i < pfCount; i++)
            {
                if (isPlayerBulletActive[i])
                {
                    removeFire(pfX[i], pfY[i]);

                    if (Maze[pfY[i] - 1, pfX[i]] == ' ')
                    {
                        pfY[i]--;
                        Console.SetCursorPosition(pfX[i], pfY[i]);
                        Console.Write(".");
                    }
                    else if (Maze[pfY[i] - 1, pfX[i]] == '\\' || Maze[pfY[i] - 1, pfX[i]] == '^' || Maze[pfY[i] - 1, pfX[i]] == '/' || Maze[pfY[i] - 1, pfX[i]] == '|')
                    {
                        e3Health--;
                        isPlayerBulletActive[i] = false;
                    }
                    else if (Maze[pfY[i] - 1, pfX[i]] == '(' || Maze[pfY[i] - 1, pfX[i]] == 'o' || Maze[pfY[i] - 1, pfX[i]] == ')' || Maze[pfY[i] - 1, pfX[i]] == '|')
                    {
                        e2Health--;
                        isPlayerBulletActive[i] = false;
                    }
                    else if (Maze[pfY[i] - 1, pfX[i]] == ':' || Maze[pfY[i] - 1, pfX[i]] == '*' || Maze[pfY[i] - 1, pfX[i]] == '|')
                    {
                        e1Health--;
                        isPlayerBulletActive[i] = false;
                    }
                    else
                    {
                        isPlayerBulletActive[i] = false;
                    }
                }
            }
        }
        static void generateEnemy1Fire()
        {
            int randomValue = new Random().Next(100);

            if (randomValue < 40)
            {
                efX[efCount] = ex1 + 2;
                efY[efCount] = ey1 + 3;

                if (Maze[efY[efCount], efX[efCount]] == ' ')
                {
                    Console.SetCursorPosition(efX[efCount], efY[efCount]);
                    Console.Write(".");
                    isEnemyBulletActive[efCount] = true;
                    efCount++;
                }
            }
        }
        static void moveEnemy1Fire()
        {
            for (int i = 0; i < efCount; i++)
            {
                if (isEnemyBulletActive[i])
                {
                    removeFire(efX[i], efY[i]);
                    if (Maze[efY[i] + 1, efX[i]] == ' ')
                    {
                        efY[i]++;
                        Console.SetCursorPosition(efX[i], efY[i]);
                        Console.Write(".");
                    }
                    else if (Maze[efY[i] + 1, efX[i]] == '/' || Maze[efY[i] + 1, efX[i]] == '^' || Maze[efY[i] + 1, efX[i]] == '\\')
                    {
                        playerHealth--;
                        isEnemyBulletActive[i] = false;
                    }
                    else if (efY[i] >= 25)
                    {
                        isEnemyBulletActive[i] = false;
                    }
                }
            }
        }
        static void generateEnemy2Fire()
        {
            int randomValue = new Random().Next(100);

            if (randomValue < 40)
            {
                efX2[efCount2] = ex2 + 1;
                efY2[efCount2] = ey2 + 3;

                if (Maze[efY2[efCount2], efX2[efCount2]] == ' ')
                {
                    Console.SetCursorPosition(efX2[efCount2], efY2[efCount2]);
                    Console.Write(".");
                    isEnemyBulletActive2[efCount2] = true;
                    efCount2++;
                }
            }
        }
        static void moveEnemy2Fire()
        {
            for (int i = 0; i < efCount2; i++)
            {
                if (isEnemyBulletActive2[i])
                {
                    removeFire(efX2[i], efY2[i]);

                    if (Maze[efY2[i] + 1, efX2[i]] == ' ')
                    {
                        efY2[i]++;
                        Console.SetCursorPosition(efX2[i], efY2[i]);
                        Console.Write(".");
                    }
                    else if (Maze[efY2[i] + 1, efX2[i]] == '/' || Maze[efY2[i] + 1, efX2[i]] == '^' || Maze[efY2[i] + 1, efX2[i]] == '\\')
                    {
                        playerHealth--;
                        isEnemyBulletActive2[i] = false;
                    }
                    else if (efY2[i] >= 25)
                    {
                        isEnemyBulletActive2[i] = false;
                    }
                }
            }
        }
        static void generateEnemy3Fire()
        {
            efX3[efCount3] = ex3 + 1;
            efY3[efCount3] = ey3 + 3;

            int randomValue = new Random().Next(100);

            if (randomValue < 10 && Maze[efY3[efCount3], efX3[efCount3]] == ' ')
            {
                Console.SetCursorPosition(efX3[efCount3], efY3[efCount3]);
                Console.Write(".");
                isEnemyBulletActive3[efCount3] = true;
                efCount3++;
            }
        }
        static void moveEnemy3Fire()
        {
            for (int i = 0; i < efCount3; i++)
            {
                if (isEnemyBulletActive3[i])
                {
                    removeFire(efX3[i], efY3[i]);
                    if (Maze[efY3[i] + 1, efX3[i]] == ' ')
                    {
                        efY3[i]++;
                        Console.SetCursorPosition(efX3[i], efY3[i]);
                        Console.Write(".");
                    }
                    else if (Maze[efY3[i] + 1, efX3[i]] == '/' || Maze[efY3[i] + 2, efX3[i]] == '/' || Maze[efY3[i] + 1, efX3[i]] == '^' || Maze[efY3[i] + 2, efX3[i]] == '^' || Maze[efY3[i] + 1, efX3[i]] == '\\' || Maze[efY3[i] + 2, efX3[i]] == '\\')
                    {
                        playerHealth--;
                        isEnemyBulletActive3[i] = false;
                    }
                    else if (efY3[i] >= 25)
                    {
                        isEnemyBulletActive3[i] = false;
                    }
                }
            }
        }
        static void generateRandomFire()
        {
            int randomNumber = new Random().Next(100);
            if (randomNumber < 15)
            {
                efX_random[efCount_random] = 2;
                efY_random[efCount_random] = new Random().Next(17, 36);

                if (Maze[efY_random[efCount_random], efX_random[efCount_random]] == ' ')
                {
                    Console.SetCursorPosition(efX_random[efCount_random], efY_random[efCount_random]);
                    Console.Write(">");
                    isEnemyBulletActive_random[efCount_random] = true;
                    efCount_random++;
                }
            }
        }
        static void moveRandomFire()
        {
            for (int i = 0; i < efCount_random; i++)
            {
                if (isEnemyBulletActive_random[i])
                {
                    removeFire(efX_random[i], efY_random[i]);
                    if (Maze[efY_random[i], efX_random[i] + 1] == ' ')
                    {
                        efX_random[i]++;
                        Console.SetCursorPosition(efX_random[i], efY_random[i]);
                        Console.Write(">");
                    }
                    else if (Maze[efY_random[i], efX_random[i] + 1] == '/' || Maze[efY_random[i], efX_random[i] + 1] == '_' || Maze[efY_random[i], efX_random[i] + 1] == '~')
                    {
                        playerHealth--;
                        isEnemyBulletActive_random[i] = false;
                    }
                    else if (efX_random[i] + 1 >= 110)
                    {
                        isEnemyBulletActive_random[i] = false;
                    }
                }
            }
        }
        static void removeFire(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }
    }
}
