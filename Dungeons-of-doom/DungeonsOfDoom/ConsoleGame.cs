using System;
using System.Collections.Generic;
using System.Linq;
using Utils;
using DungeonsOfDoom.Core;


namespace DungeonsOfDoom
{
    class ConsoleGame
    {
        Player player;
        Room[,] world;
        const int WorldHeight = 5;
        const int WorldWidth = 20;

        public void Play()
        {
            SetUpGame();
            PlayGame();
            EndGame();
        }


        public void SetUpGame()
        {
            CreatePlayer();
            CreateWorld();
            InitialPrint();

            void InitialPrint()
            {
                Console.WriteLine();
                Console.Write("              ");
                TextUtils.AnimateText("Dungeons of Doom", 200);
                TextUtils.AnimateText("...", 600);
                Console.ReadKey();
            }
            void CreatePlayer()
            {
                player = new Player(50, 0, 0);
            }
            void CreateWorld()
            {
                world = new Room[WorldWidth, WorldHeight];

                for (int y = 0; y < world.GetLength(1); y++)
                {
                    for (int x = 0; x < world.GetLength(0); x++)
                    {
                        world[x, y] = new Room();

                        if (x != 0 && y != 0)
                        {
                            int randResult = RandomUtils.OneToCustomRnd(100);

                            if (randResult < 5)
                                world[x, y].Monster = new Skeleton(30);
                            else if (randResult < 10)
                                world[x, y].Monster = new Orc(50);
                            else if (randResult < 15)
                                world[x, y].Item = new Sword();
                            else if (randResult < 20)
                                world[x, y].Item = new Axe();
                            else if (randResult < 25)
                                world[x, y].Item = new Potion();
                            else if (randResult < 40)
                                world[x, y].Obstacle = new Obstacle();
                        }
                    }
                }
            }
        }

        public void PlayGame()
        {
            do
            {
                DisplayWorld();
                DisplayStats();
                AskForMovement();
                CheckRoom();

            } while (player.Health > 0 && Monster.remainingMonsters > 0);


            void DisplayWorld()
            {
                Console.Clear();
                Console.WriteLine();
                Console.Write("              ");
                Console.WriteLine("Dungeons of Doom");

                Console.WriteLine(" ___________________________________________");
                for (int y = 0; y < world.GetLength(1); y++)
                {
                    Console.WriteLine("|                                           |");
                    Console.Write("| ");
                    for (int x = 0; x < world.GetLength(0); x++)
                    {
                        Console.Write(" ");
                        Room room = world[x, y];
                        if (player.X == x && player.Y == y)
                            Console.Write("P");
                        else if (room.Monster != null)
                            Console.Write("M");
                        else if (room.Item != null)
                            Console.Write("I");
                        else if (room.Obstacle != null)
                            Console.Write("#");
                        else
                            Console.Write(".");
                    }
                    Console.Write("  |");
                    Console.WriteLine();
                }
                Console.WriteLine("|___________________________________________|");
            }
            void DisplayStats()
            {
                Console.WriteLine();
                Console.WriteLine($"Health: {player.Health}");
                Console.WriteLine($"Strength: {player.Strength}");

                Console.WriteLine();
                Console.WriteLine($"Remaining monsters: {Monster.remainingMonsters}");

                Console.WriteLine();
                Console.WriteLine("Press 'm' to see backpack");
            }
            void DisplayBackpack()
            {
                Console.WriteLine("*---------------------*");
                Console.WriteLine("|      BackPack       |");
                Console.WriteLine("*---------------------*");
                foreach (List<IBackpackAble> itemList in player.backpack)
                {
                    Console.Write("| ");
                    if (itemList[0] is Monster)
                    {
                        Console.Write($"{itemList[0].Name} corpses: {itemList.Count()}".PadRight(19));
                    }
                    else
                    {
                        Console.Write($"{itemList[0].Name}s: {itemList.Count()}".PadRight(19));
                    }
                    Console.WriteLine(" |");
                }
                Console.WriteLine("*---------------------*");
                askForMenuInput();
            }
            void askForMenuInput()
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (char.IsNumber(keyInfo.KeyChar))
                {
                    player.useItemInBackpack(int.Parse(keyInfo.KeyChar.ToString()));

                }
            }
            void AskForMovement()
            {
                int newX = player.X;
                int newY = player.Y;
                bool isValidKey = true;

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.RightArrow: newX++; break;
                    case ConsoleKey.LeftArrow: newX--; break;
                    case ConsoleKey.UpArrow: newY--; break;
                    case ConsoleKey.DownArrow: newY++; break;
                    case ConsoleKey.M: DisplayBackpack(); break;

                    default: isValidKey = false; break;
                }

                if (isValidKey &&
                    newX >= 0 && newX < world.GetLength(0) &&
                    newY >= 0 && newY < world.GetLength(1))
                {
                    player.X = newX;
                    player.Y = newY;
                }
            }
            void CheckRoom()
            {
                Room currentRoom = world[player.X, player.Y];
                if (currentRoom.Item != null)
                {
                    Console.WriteLine($"You find a {currentRoom.Item.Name}");
                    player.addItemToBackpack(currentRoom.Item);
                    Console.ReadKey();
                }
                if (currentRoom.Monster != null)
                {
                    Console.WriteLine($"Eeek, a {currentRoom.Monster.Name}!");
                    Console.ReadKey();

                    bool willThereBeAFight = currentRoom.Monster.Encounter(player);

                    if (willThereBeAFight)
                    {
                        bool wonFight = Fight(player, currentRoom.Monster);
                        if (wonFight)
                        {
                            player.addItemToBackpack(currentRoom.Monster);
                        }
                    }

                }
                if (currentRoom.Obstacle != null)
                {
                    while (currentRoom.Obstacle.Health > 0)
                    {
                        player.Attack(currentRoom.Obstacle);
                        Console.WriteLine($"You attack the Obstacle");
                        Console.ReadKey();
                    }
                }
                currentRoom.ResetRoom();
            }
            bool Fight(Character inputPlayer, Monster monster)
            {
                bool wonFight = false;
                Console.WriteLine($"The {monster.Name} wants to fight, brace yourself!");
                Console.ReadKey(false);
                Console.WriteLine($"Player Health:       {monster.Name} Health:");
                Console.Write($" {inputPlayer.Health}                   {monster.Health}");
                Console.WriteLine();

                while (inputPlayer.Health > 0 && monster.Health > 0)
                {
                    //Console.WriteLine($"You attack {monster.Name}");
                    monster.Attack(inputPlayer);
                    Console.ReadKey();
                    Console.Write($"{inputPlayer.Health}                  ");

                    if (inputPlayer.Health > 0)
                    {
                        //Console.WriteLine($"{monster.Name} attacks you");
                        inputPlayer.Attack(monster);
                        Console.ReadKey();
                        Console.Write($"{monster.Health}");

                        Console.WriteLine();

                    }
                }
                Console.ReadKey();
                if (inputPlayer.Health > 0)
                {
                    wonFight = true;
                }
                else
                {
                    wonFight = false;
                }
                return wonFight;

            }
        }

        public void EndGame()
        {
            GameOver();

            void GameOver()
            {
                Console.Clear();

                if (player.Health <= 0)
                {
                    Console.WriteLine("You died!");
                    Console.WriteLine("Game over...");
                    ResetGame();
                }
                else if (Monster.remainingMonsters == 0)
                {
                    Console.WriteLine("You won! All the monsters are defeated!");
                    Console.WriteLine("Press a key to start again");
                }

                Console.ReadKey();
                Console.Clear();
                void ResetGame()
                {
                    player.backpack.Clear();
                    Monster.counter = 0;
                    //Sword.ResetCounter();
                    //Axe.ResetCounter();
                }
                Play();
            }
        }

    }
}