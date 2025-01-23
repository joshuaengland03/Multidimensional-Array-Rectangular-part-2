// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

//--------------------------------- Global Variables ---------------------------------------//
Random rand = new Random();
bool playerDead = false;
string[] Challenges = { "Empty Space", "Monster", "Puzzle", "Trap", "Exit" };
bool KeepPlaying = true;
bool invalidPosition = true;
int XPosition = 0;
int YPosition = 0;
int playerHealth = 100;
int[,] Dungeon = new int[5, 5]
{
    { 0, 1, 0, 2, 0 },
    { 3, 0, 0, 1, 0 },
    { 0, 0, 4, 0, 2 },
    { 1, 3, 0, 0, 0 },
    { 2, 0, 1, 3, 0 }
};

//------------------------------- Main Code that runs ---------------------------------------//
ChooseStartPosition();
GameStart();

while (KeepPlaying)
{
    Move();

}

ThankYouForPlaying();


//-------------------------------------- Methods --------------------------------------------//
void ChooseStartPosition()
{
    while (invalidPosition)
    {
        XPosition = rand.Next(0, 4);
        YPosition = rand.Next(0, 4);
        if (XPosition == 2 && YPosition == 2)
        {

        }
        else
        {
            invalidPosition = false;
            break;
        }

    }
    Console.WriteLine(XPosition + " " + YPosition);
}

void GameStart()
{
    Console.WriteLine("Welcome to the Dungeon of Horrors!");
    Console.WriteLine("Use W, A, S, D to move!");
    Console.WriteLine("You are at the coordinates " + (XPosition + 1) + ", " + (YPosition + 1));
}

void Move()
{
    if (playerHealth <= 0)
    {
        Console.WriteLine("You Died!");
        playerDead = true;
        KeepPlaying = false;
    }
    else
    {
        Console.WriteLine("Where would you like to go?");
        string playerInput = Console.ReadLine().Trim().ToLower();
        if ((playerInput == "w") && ((YPosition - 1) >= 0))
        {
            YPosition = YPosition - 1;
        }
        else if ((playerInput == "a") && ((XPosition - 1) >= 0))
        {
            XPosition = XPosition - 1;
        }
        else if ((playerInput == "s") && ((YPosition + 1) <= 4))
        {
            YPosition = YPosition + 1;
        }
        else if ((playerInput == "d") && ((XPosition + 1) <= 4))
        {
            XPosition = XPosition + 1;
        }
        else
        {
            Console.WriteLine("That's not a valid input, try again!");
        }


        if (XPosition == 2 && YPosition == 2)
        {
            KeepPlaying = false;
        }
        else
        {
            KeepPlaying = true;
            Console.WriteLine("You are at the coordinates " + (XPosition + 1) + ", " + (YPosition + 1));
            int currentSpace = (Dungeon[XPosition, YPosition]);
            Console.WriteLine("You found a " + Challenges[currentSpace] + "!");


            if (currentSpace == 1)
            {
                Console.WriteLine("You found a monster! It attacked you, but you killed it.");
                playerHealth = playerHealth - 20;
                Console.WriteLine("You have " + playerHealth + " health remaining.");
                Console.WriteLine();
            }
            else if (currentSpace == 2)
            {
                int evilDoor = rand.Next(1, 3);
                Console.WriteLine("You see three doors. One door leads to certain doom. The other two lead to safety.");
                Console.WriteLine("Choose a door: 1, 2, 3");
                bool playPuzzle = true;
                while (playPuzzle)
                {
                    string doorChoiceInput = Console.ReadLine();
                    int doorChoice;
                    bool doorChoiceValid = int.TryParse(doorChoiceInput, out doorChoice);
                    if (doorChoiceValid && doorChoice > 0 && doorChoice < 4)
                    {
                        if (doorChoice == evilDoor)
                        {
                            playerHealth = playerHealth - 60;
                            Console.WriteLine("You picked the wrong door. You lost 60 health.");
                            Console.WriteLine("You have " + playerHealth + " health remaining.");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("The door leads to safety!");
                            Console.WriteLine("You have " + playerHealth + " health remaining.");
                            Console.WriteLine();
                        }
                        playPuzzle = false;
                    }
                    else
                    {
                        Console.WriteLine("Try Again!");
                    }
                }

            }
            else if (currentSpace == 3)
            {
                playerHealth = playerHealth - 60;
                Console.WriteLine("The trap hurt you. You lost 50 health.");
                Console.WriteLine("You have " + playerHealth + " health remaining.");
                Console.WriteLine();
            }
        }
    }
}
void ThankYouForPlaying()
{
    if (playerDead)
    {
        Console.WriteLine("Oh well! Thank you for playing!");
    }
    else
    {
        Console.WriteLine("You found the Exit! Thank you for playing!");
    }
}
