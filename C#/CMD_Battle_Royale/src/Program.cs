using System;
using System.Threading;
using System.Collections.Generic;

namespace BattleRoyale
{
    class Program
    {
        public static Random Random {get; set;}

        private Board game;
        private string name;
        private int ai;
        private bool autotick;
        private int tickspeed;

        public static void Main(string[] args)
        {
            Random = new Random();
            Program p = new Program();
            p.Start();
        }

        public void Start()
        {
            if (FileHandler.SettingsExists())
            {
                List<string> file = FileHandler.ReadSettings();
                
                foreach (string line in file)
                {
                    string[] args = line.Split(' ');

                    switch(args[0])
                    {
                        case "name":
                            name = args[1];
                            break;
                        case "ai":
                            ai = int.Parse(args[1]);
                            break;
                        case "autotick":
                            autotick = bool.Parse(args[1]);
                            break;
                        case "tickspeed":
                            tickspeed = int.Parse(args[1]);
                            break;
                        case "fcolor":
                            Console.ForegroundColor = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), args[1], true);
                            break;
                        case "bcolor":
                            Console.BackgroundColor = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), args[1], true);
                            break;
                        default:
                            throw new Exception("Settings file has failed you, master :(");
                    }
                }
            }
            else
            {
                name = "User";
                ai = 0;
                autotick = true;
                tickspeed = 1000;
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Save_Settings();
            }
            
            Show_Splash_Screen();
            Handle_Main_Menu();  
        }

        public void Save_Settings()
        {
                List<string> file = new List<string>();
                file.Add("name " + name);
                file.Add("ai " + ai.ToString());
                file.Add("autotick " + autotick.ToString());
                file.Add("tickspeed " + tickspeed.ToString());
                file.Add("fcolor " + Enum.GetName(typeof(ConsoleColor), Console.ForegroundColor));
                file.Add("bcolor " + Enum.GetName(typeof(ConsoleColor), Console.BackgroundColor));

                FileHandler.SaveSettings(file);
        }

        public void Show_Splash_Screen()
        {
            Console.Clear();
            Console.Write("=============================\n" +
                          "| B A T T L E   R O Y A L E |\n" +
                          "|  C O N S O L E   G A M E  |\n" +
                          "=============================\n" +
                          "\n" +
                          "- By HartoeHajek");
            Thread.Sleep(1500);
        }

        public void Handle_Main_Menu()
        {
            Console.Clear();
            Console.Write("==========================================\n" +
                          "Welcome to Hartoe's Console Battle Royale!\n" +
                          "Type a command and press enter.\n" +
                          "\n" +
                          " - play: Start a new game\n" +
                          " - options: Go to the options menu\n" +
                          " - quit: quit the program\n" +
                          "==========================================\n" +
                          name + "> ");
            string input = Console.ReadLine().ToLower();
            
            switch(input)
            {
                case "play":
                    Start_Game();
                    break;
                case "options":
                    Handle_Options();
                    break;
                case "quit":
                    Quit();
                    break;
                default:
                    Console.Write("\nCommand " + input + " was not a recognized command!\n" +
                                  "Press any key to continue...\n");
                    Console.ReadKey();
                    Handle_Main_Menu();
                    break;
            }
        }

        public void Start_Game()
        {
            Console.Clear();
            game = new Board(name, autotick, tickspeed, ai);
            game.Start_New_Game();
            Handle_Main_Menu();
        }

        public void Handle_Options()
        {
            Console.Clear();
            Console.Write("=============\n" +
                          "O P T I O N S\n" +
                          "=============\n" +
                          "\n" +
                          " - set [option] [value]: Sets an option to the given value\n" +
                          " - [option]: Prints the current value of an option\n" +
                          " - list: Shows all options\n" +
                          " - save: Saves current options as the default configuration\n" +
                          " - back: Go back to the Main Menu\n" +
                          "\n" +
                          name + "> ");
            string input = Console.ReadLine().ToLower();

            if(input == "")
            {
                Console.Write("You have to enter a command!\n" +
                              "Press any key to continue...\n");
                Console.ReadKey();
                Handle_Options();
                return;
            }

            string[] args = input.Split(' ');
            switch(args[0])
            {
                case "save":
                    Handle_Save();
                    break;
                case "list":
                    List_Options();
                    break;
                case "set":
                    Set_Option(args);
                    break;
                case "back":
                    Handle_Main_Menu();
                    break;
                default:
                    Get_Option(args);
                    break;
            }
        }

        public void Handle_Save()
        {
            Console.Clear();
            Console.WriteLine("Saving current settings as the default configurations!");
            Save_Settings();
            Console.WriteLine("Save Complete!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Handle_Options();
        }

        public void List_Options()
        {
            Console.Clear();
            Console.Write("==========================================================================\n" +
                          "\n" +
                          "ai: Determines the difficulty level of the ai. Ranges from 0 to 2.\n" +
                          "autotick: Determines if the game updates automatically or on player input.\n" +
                          "tickspeed: Determines the speed at which the game updates. Minimum of 500.\n" +
                          "name: Determines the username.\n" + 
                          "fcolor: Determines the text color.\n" +
                          "bcolor: Determines the background color.\n" +
                          "\n" +
                          "==========================================================================\n" +
                          "\n" +
                          "Press any key to continue...");
            Console.ReadKey();
            Handle_Options();
        }

        public void Set_Option(string[] args)
        {
            Console.Clear();
            if (args.Length < 3)
            {
                Console.WriteLine("You didn't fill in all parameters for the 'set' command!");
            }
            else
            {
                string option = args[1];
                string val    = args[2];
                
                switch(option)
                {
                    case "ai":
                        try
                        {
                            int new_ai = Math.Min(Math.Max(0, int.Parse(val)), 2);
                            ai = new_ai;
                            Console.WriteLine("Option ai difficulty was set to " + new_ai.ToString());
                        }
                        catch
                        {
                            Console.WriteLine("Value " + val + " was not a valid number!");
                        }
                        break;
                    case "autotick":
                        if (val == "true")
                        {
                            autotick = true;
                            Console.WriteLine("Option autotick was set to " + autotick.ToString());
                        }
                        else if (val == "false")
                        {
                            autotick = false;
                            Console.WriteLine("Option autotick was set to " + autotick.ToString());
                        }
                        else
                        {
                            Console.WriteLine("Value " + val + " has to be either 'true' or 'false'!");
                        }
                        break;
                    case "tickspeed":
                        try
                        {
                            int new_tickspeed = Math.Max(500, int.Parse(val));
                            tickspeed = new_tickspeed;
                            Console.WriteLine("Option tickspeed was set to " + new_tickspeed.ToString());
                        }
                        catch
                        {
                            Console.WriteLine("Value " + val + " was not a valid number!");
                        }
                        break;
                    case "name":
                        name = val;
                        Console.WriteLine("Option username was set to " + val);
                        break;
                    case "fcolor":
                        try
                        {
                            Console.ForegroundColor = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), val, true);
                            Console.WriteLine("Option fcolor was set to " + Enum.GetName(typeof(ConsoleColor), Console.ForegroundColor));
                        }
                        catch
                        {
                            Console.WriteLine("Value " + val + " is not a recognized color!");
                        }
                        break;
                    case "bcolor":
                        try
                        {
                            Console.BackgroundColor = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), val, true);
                            Console.WriteLine("Option bcolor was set to " + Enum.GetName(typeof(ConsoleColor), Console.BackgroundColor));
                        }
                        catch
                        {
                            Console.WriteLine("Value " + val + " is not a recognized color!");
                        }
                        break;
                    default:
                        Console.WriteLine("Option " + option + " was not recognized!");
                        break;
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Handle_Options();
        }

        public void Get_Option(string[] args)
        {
            Console.Clear();
            string input = args[0];
            switch(input)
            {
                case "ai":
                    Console.WriteLine("Option ai difficulty is currently set to " + ai.ToString());
                    break;
                case "autotick":
                    Console.WriteLine("Option autotick is currently set to " + autotick.ToString());
                    break;
                case "tickspeed":
                    Console.WriteLine("Option tickspeed is currently set to " + tickspeed.ToString());
                    break;
                case "name":
                    Console.WriteLine("Option username is currently set to " + name);
                    break;
                case "fcolor":
                    Console.WriteLine("Option foreground color is currently set to " + Enum.GetName(typeof(ConsoleColor), Console.ForegroundColor));
                    break;
                case "bcolor":
                    Console.WriteLine("Option background color is currently set to " + Enum.GetName(typeof(ConsoleColor), Console.BackgroundColor));
                    break;
                default:
                    Console.WriteLine("Option " + input + " was not recognized!");
                    break;
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Handle_Options();
        }

        public void Quit()
        {
            Console.Clear();
            Console.Write("Are you sure you want to quit? (yes/no)\n" +
                          name + "> ");
            string input = Console.ReadLine().ToLower();
            
            switch(input)
            {
                case "yes":
                    Console.WriteLine("Goodbye! ^_^");
                    Console.ReadKey();
                    Console.ResetColor();
                    Console.Clear();
                    Environment.Exit(0);
                    break;
                case "no":
                    Handle_Main_Menu();
                    break;
                default:
                    Console.Write("Please type in 'yes' or 'no'\n" + 
                                  "Press any key to continue...");
                    Console.ReadKey();
                    Quit();
                    break;
            }
        }
    }
}
