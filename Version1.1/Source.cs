/*
Made By therealogplayer
Name: TicTacToe
Made On: 11/14/2023
Worked On Again: 12/01/2023
Finished: 12/01/2023
*/
using System;
using System.Linq;

namespace TicTacToe
{
    class Program
    {
        public static string diffecultyChoice;

        public static char[] board = new char[9];

        public static bool canRun = true;
        public static bool debuggingMode = false;
        public static bool playersTurn;

        public static int computerPoints = 0;
        public static int playerPoints = 0;
        public static int tieCounter = 0;

        static void writeRules()
        {
            Console.WriteLine("Welcome To The Real Og Player's Tic Tac Toe");
            Console.WriteLine("Match Three In A Row In Order To Win\n\nYou have {0} Point(s) The computer has {1} Point(s) You both have {2} Tie(s)\n", playerPoints, computerPoints,tieCounter);
            Console.WriteLine("You Are X's");

            Console.WriteLine("");
        }


        static void printBoard(bool initiateBoard)
        {
            //Clears The Console
            if (!debuggingMode) Console.Clear();
            writeRules();

            //Draws The Board
            string ABC = "BC";
            int counter = 0;

            Console.WriteLine("\\|1|2|3");
            for (int i = 0; i < board.Length; i++)
            {
                if (initiateBoard) board[i] = '#';

                if (i == 0)
                {
                    Console.Write("A|{0}", board[i]);
                }
                else
                {
                    Console.Write(board[i]);
                }

                //Formats The Board
                if (i % 3 == 0 || i % 3 == 1) Console.Write("|");
                if (i % 3 == 2)
                {
                    if (counter != 2)
                    {
                        Console.Write("\n{0}|", ABC[counter++]);
                    }
                    else
                    {
                        Console.Write("\n");
                    }
                }
            }
            checkWinState();
            handleMoves();

        }

        static void botPlace(int location) 
        {
            //Checks to see if it can play if not then it plays randomly
            if (board[location] == '#')
            {
                board[location] = 'O';
                playersTurn = true;
                printBoard(false);
            }
            else
            {
                botRandom();
                printBoard(false);
            }

        }

        static void botRandom() 
        {
            if (debuggingMode) Console.WriteLine("Random");
            int[] spotsItCanGo = new int[0];
            Random rnd = new Random();

            //Finds All Spots It Can go
            for (int i = 0; i < board.Length; i++) 
            {
                if (board[i] == '#') 
                {
                    spotsItCanGo = spotsItCanGo.Append(i).ToArray();
                }
            }
            if (spotsItCanGo.Length < 1)
            {
                endStateOfgame('E');
            }
            else 
            {     
                int location = spotsItCanGo[rnd.Next(0, spotsItCanGo.Length)];
                botPlace(location);
            }
        }
        static void botCorners() 
        {
            Random rnd = new Random();
            if (debuggingMode) Console.WriteLine("Corners");

            //Gets All The Open Corners and puts them in a list
            int[] corners = { 0, 2, 6, 8 };
            int[] openCorners = new int[0];
            for (int i = 0; i < corners.Length; i++)
            {
                if (board[corners[i]] == '#')
                {
                    openCorners = openCorners.Append(corners[i]).ToArray();
                }
            }

            botPlace(openCorners[rnd.Next(0,openCorners.Length)]);
        }

        static void botCheck() 
        {
            //Check For Winable State
            string possibleWinStates = "H021H063H285H687H264H084H354H174";
            bool didPlaceWork = false;
            //Check if bot can win
            for (int i = 0; i < possibleWinStates.Length; i++)
            {
                if (possibleWinStates[i] == 'H')
                {
                    int XCord = Convert.ToInt32(Convert.ToString(possibleWinStates[i + 1]));
                    int YCord = Convert.ToInt32(Convert.ToString(possibleWinStates[i + 2]));
                    int ZCord = Convert.ToInt32(Convert.ToString(possibleWinStates[i + 3]));

                    if (debuggingMode) Console.WriteLine("First Look | X: {0} {1} | Y: {2} {3} | Z: {4} {5}", XCord, board[XCord], YCord, board[YCord], ZCord, board[ZCord]);

                    if ((board[XCord] == 'O' && board[YCord] == 'O') && (board[ZCord] != 'X' && board[ZCord] != 'O'))
                    {
                            if (debuggingMode) Console.WriteLine("Found Win State");
                            didPlaceWork = true;
                            botPlace(ZCord);
                            break;
                    }
                    else 
                    {
                        if (debuggingMode) Console.WriteLine("Could Not Find Win State");
                    }

                }
            }


            //Only Runs if gamemode is not on medium
            if (diffecultyChoice == "Hard") 
            {
                //Checks to see if player is about to win
                possibleWinStates = "H036H147H258H048H246H642H840H360H471H582H012H345H678H786H453H120H840";
                for (int i = 0; i < possibleWinStates.Length; i++)
                {
                    if (possibleWinStates[i] == 'H')
                    {
                        int XCord = Convert.ToInt32(Convert.ToString(possibleWinStates[i + 1]));
                        int YCord = Convert.ToInt32(Convert.ToString(possibleWinStates[i + 2]));
                        int ZCord = Convert.ToInt32(Convert.ToString(possibleWinStates[i + 3]));

                        //Runs Twice To See If bot Has A Win
                        for (int y = 0; y < 2; y++) 
                        {
                            string XandO = "XO";
                            if (debuggingMode) Console.WriteLine("Second Look | X: {0} {1} | Y: {2} {3} | Z: {4} {5}", XCord, board[XCord], YCord, board[YCord], ZCord, board[ZCord]);

                            if ((board[XCord] == XandO[y] && board[YCord] == XandO[y]) && (board[ZCord] != 'X' && board[ZCord] != 'O'))
                            {
                                botPlace(ZCord);
                                didPlaceWork = true;
                                break;
                            }
                        }
                    }
                }
            }

            //Gets All The Open Corners and puts them in a list
            int[] corners = { 0, 2, 6, 8 };
            bool openCorner = false;
            for (int i = 0; i < corners.Length; i++)
            {
                if (board[corners[i]] == '#')
                {
                    openCorner = true;
                    break;
                }
            }

            //If there win possiblity do this.
            if (!didPlaceWork) 
            {
                //Plays Corners if it can
                if (openCorner)
                {
                    botCorners();
                }
                else 
                {
                    botRandom();
                }
            }

        }


        static void handleMoves()
        {
            if (playersTurn && canRun)
            {
                //Players turn
                try
                {
                    Console.Write("\nType 999 to exit\nType 111 to restart the game\nYour move cowboy! What cords do you want to move to? IE A1\n>> ");
                    int locationOfChoice = 0;
                    string cords = Console.ReadLine();
                    if (cords == "999") Environment.Exit(0);
                    if (cords == "111") StartGame();
                    int Colloum = Convert.ToInt32(Convert.ToString(cords[1]));
                    int Row = 4;
                    switch (cords.ToUpper()[0])
                    {
                        case 'A':
                            Row = 1;
                            break;
                        case 'B':
                            Row = 4;
                            break;
                        case 'C':
                            Row = 7;
                            break;
                        default:
                            Row = 100;
                            break;
                    }
                    locationOfChoice = (Row + Colloum) - 2;

                    if (Colloum > 3 || Colloum <= 0 || Row == 100 || board[locationOfChoice] != '#')
                    {
                        if (!debuggingMode) Console.Clear();
                        Console.Write("The Cords '{0}' are not possible try another set of cords.\nWhen ready press any key.", cords);
                        Console.ReadKey();
                        printBoard(false);
                    }
                    else
                    {
                        board[locationOfChoice] = 'X';
                        playersTurn = false;
                        printBoard(false);
                    }
                }
                catch (Exception e)
                {
                    if (!debuggingMode) Console.Clear();
                    Console.Write("The cords you have entered are not possible try another set of cords.\nWhen ready press any key.");
                    Console.ReadKey();
                    printBoard(false);
                }
            }
            else if (canRun)
            {
                Random rnd = new Random();


                if (diffecultyChoice == "Easy")
                {
                    botRandom();
                }
                else if (diffecultyChoice == "Hard" || diffecultyChoice == "Medium")
                {
                    botCheck();
                }
            }

        }

        static void endStateOfgame(char WhoWon)
        {
            canRun = false;
            //Handles What To Do After Win
            string outPutWhoWon = "";
            switch (WhoWon)
            {
                case 'X':
                    Console.Write("\nYou Won the game!\n\nWant to play again?(Y)(N)\n>> ");
                    playerPoints++;
                    break;
                case 'O':
                    Console.Write("\nThe Computer Won.\n\nWant to play again?(Y)(N)\n>> ");
                    computerPoints++;
                    break;
                case 'T':
                    Console.Write("\nThe game ended in a Tie\n\nWant to play again?(Y)(N)\n>> ");
                    tieCounter++;
                    break;
                default:
                    Console.Write("\nError NO ONE WON SOMETHING WENT WRONG\n\nWant to try playing again?(Y)(N)\n>> ");
                    break;
            }

            string userInput = Console.ReadLine();

            //Sees if the user wants to play again.
            if (userInput.ToUpper() != "N")
            {
                canRun = true;
                StartGame();
            }
            else
            {
                Environment.Exit(0);
            }
        }


        static void checkWinState()
        {
            string XOrO = "XO";

            //Check
            for (int i = 0; i < XOrO.Length; i++)
            {
                if ((board[0] == XOrO[i] && board[1] == XOrO[i] && board[2] == XOrO[i]) || (board[0] == XOrO[i] && board[3] == XOrO[i] && board[6] == XOrO[i]) || (board[0] == XOrO[i] && board[4] == XOrO[i] && board[8] == XOrO[i]))
                {
                    endStateOfgame(XOrO[i]);
                }
                else if ((board[1] == XOrO[i] && board[4] == XOrO[i] && board[7] == XOrO[i]) || (board[3] == XOrO[i] && board[4] == XOrO[i] && board[5] == XOrO[i]) || (board[6] == XOrO[i] && board[7] == XOrO[i] && board[8] == XOrO[i]))
                {
                    endStateOfgame(XOrO[i]);
                }
                else if ((board[2] == XOrO[i] && board[5] == XOrO[i] && board[8] == XOrO[i]) || (board[2] == XOrO[i] && board[4] == XOrO[i] && board[6] == XOrO[i]))
                {
                    endStateOfgame(XOrO[i]);
                }
            }

            int counter = 0;

            //Check For Tie
            for (int y = 0; y < board.Length; y++)
            {
                if (board[y] == '#')
                {
                    counter++;
                }
            }
            if (counter == 0) endStateOfgame('T');

        }


        static void StartGame()
        {
            //Gets The Settings The User Wants
            if (!debuggingMode) Console.Clear();
            if (debuggingMode) Console.WriteLine("Debugging Mode Is Enabled.");
            Console.WriteLine("What Diffiulty Do You Want(Easy(E) Medium(M) Hard(H)) Enable/Disable Debuggin(D) Exit(999)");
            Console.WriteLine("Hard: The Computer Goes First And Is Smart And Will Do Anything To Make Sure You Lose.");
            Console.WriteLine("Medium: You go First And The Computer Is Smart But Goes Easy On You.");
            Console.Write("Easy: You Go First And The Computers Choice Is Random.\n>> ");
            diffecultyChoice = Console.ReadLine().ToUpper();


            switch (diffecultyChoice[0])
            {
                case 'E':
                    diffecultyChoice = "Easy";
                    playersTurn = true;
                    break;
                case 'M':
                    diffecultyChoice = "Medium";
                    playersTurn = true;
                    break;
                case 'H':
                    diffecultyChoice = "Hard";
                    playersTurn = false;
                    break;
                case '9':
                    Environment.Exit(0);
                    break;
                case 'D':
                    if (debuggingMode)
                    {
                        debuggingMode = false;
                    }
                    else 
                    {
                        debuggingMode = true;
                    }
                    if (!debuggingMode) Console.Clear();
                    StartGame();
                    break;
                default:
                    Console.WriteLine("'{0}' is not an option. Type 'H' for Hard AND/OR 'E' for Easy\nPress any key to continue.", diffecultyChoice);
                    Console.ReadKey();
                    StartGame();
                    break;
            }
            //Prints Board Tells User The Diffeculty They Chose And Initiates The Board
            if (!debuggingMode) Console.Clear();
            printBoard(true);
            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            StartGame();
        }
    }
}