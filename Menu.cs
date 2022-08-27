namespace Menu
{

    class MenuManager
    {
        public MenuView menuView;
        public int pointer { get; set; }
        public bool onfile { get; set; }
        public bool selected { get; set; }
        public string toOpen { get; set; }
        public string root { get; } = "./test";

        public MenuManager()
        {
            getDirectoryContent();
            startMenu();
        }

        public void getDirectoryContent()
        {
            pointer = 0;
            menuView = new MenuView(Directory.EnumerateFileSystemEntries(root).ToArray());
            toOpen = root;
            menuView.showMenu(pointer);
        }


        public void startMenu()
        {

            pointer = 0;

            while (!selected)
            {
                KeyPress(Console.ReadKey(true).Key);
            }

            selected = false;

        }

        public void KeyPress(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    DecrementPointer();
                    break;
                case ConsoleKey.DownArrow:
                    IncrementPointer();
                    break;
                case ConsoleKey.Enter:
                    if (!onfile)
                    {
                        selected = true;
                        toOpen = menuView.lines[pointer];
                        enter();
                    }
                    break;
                case ConsoleKey.Escape:
                    if (onfile) onfile = false;
                    break;
                case ConsoleKey.LeftArrow:
                    if (onfile) break;
                    if (toOpen == root) break;
                    previousDir();
                    break;
            }
        }

        public void DecrementPointer()
        {
            if (pointer == 0)
            {
                return;
            }
            pointer--;
            if (!onfile)
            {
                menuView.showMenu(pointer);
            }
        }

        public void IncrementPointer()
        {
            if (pointer == menuView.lines.Length - 1)
            {
                return;
            }
            pointer++;
            if (!onfile)
            {
                menuView.showMenu(pointer);
            }
        }

        public void enter()
        {
            pointer = 0;
            selected = false;

            if (Directory.Exists(toOpen))
            {
                menuView = new MenuView(Directory.EnumerateFileSystemEntries(toOpen).ToArray());
                menuView.showMenu(pointer);
                return;
            }
            
            ReadAndShowFile();
        }

        public void ReadAndShowFile()
        {
            Console.Clear();
            onfile = true;

            foreach (string line in File.ReadAllLines(toOpen))
            {
                Console.WriteLine(line);
            }

            while (onfile)
            {
                KeyPress(Console.ReadKey(true).Key);
            }

            previousDir();
        }

        public void previousDir()
        {
            string[] dirs = toOpen.Split(@"\");
            toOpen = root;
            for (int i = 1; i < dirs.Length - 1; i++)
            {
                toOpen += $"/{dirs[i]}";
            }
            enter();
        }
    }

    class MenuView
    {

        public string[] lines { get; set; }
        public MenuView(string[] lines)
        {
            this.lines = lines;
        }

        public void showMenu(int pointer)
        {
            Console.Clear();
            Console.WriteLine("Files: ");

            for (int i = 0; i < lines.Length; i++)
            {
                if (i == pointer)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }

                Console.WriteLine(" - {0}", lines[i]);

                Console.ForegroundColor = ConsoleColor.White;
            }
        }

    }

}