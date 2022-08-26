namespace Menu
{

    class MenuManager
    {
        public MenuView menuView;
        public int pointer { get; set; }
        public bool onfile { get; set; }
        public bool selected { get; set; }
        public string toOpen { get; set; }

        public MenuManager()
        {
            getDirectoryContent();
            startMenu();
        }

        public void getDirectoryContent()
        {
            pointer = 0;
            menuView = new MenuView(Directory.EnumerateFileSystemEntries("./test").ToArray());
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
                    if (pointer == 0)
                    {
                        break;
                    }
                    pointer--;
                    if (!onfile)
                    {
                        menuView.showMenu(pointer);
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (pointer == menuView.lines.Length - 1)
                    {
                        break;
                    }
                    pointer++;
                    if (!onfile)
                    {
                        menuView.showMenu(pointer);
                    }
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
                    if(onfile) {
                        onfile = false;
                    }
                    break;
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
            }
            else
            {
                Console.Clear();
                onfile = true;

                foreach (string str in File.ReadAllLines(toOpen))
                {
                    Console.WriteLine(str);
                }

                while (onfile)
                {
                    KeyPress(Console.ReadKey(true).Key);
                }

                getDirectoryContent();
                startMenu();
            }
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