using System;
using System.Collections.Generic;
using System.Linq;
using serio_cli.Models;
using serio_cli.Repositories;

namespace serio_cli
{
    class Program
    {
        static SeriesRepo _seriesRepo = new SeriesRepo();

        static void Main(string[] args)
        {

            //Do initial setup then show the menu.

            Console.Title = "Serio-CLI - Gerenciador de Séries";
            string[] options = new string[]{"Listar todas as séries cadastradas.","Inserir série.","Atualizar série.","Remover série.","Sair"};
            int selected = 0;
            Menu(options, selected);
        }

        //Draw options on screen.
        static void Menu(string[] options, int selected)
        {
            
            while (true != false) //Probably not the best approach. [°_°]
            {

                //Draw options on screen.

                Console.SetCursorPosition(0, Console.CursorTop = 0);
                Console.Write("\n  Bem-vindo ao Serio-CLI.\n  Utilize as setas do teclado para navegar no menu.\n\n  Selecione uma opção da lista e pressione enter:\n\n");

                for (int i = 0;i < options.Length; i++)
                {
                    if(i == selected)
                    {
                        Console.WriteLine($" *{options[i]}", Console.BackgroundColor = ConsoleColor.Red);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("  "+options[i]);
                    }
                }

                //Get key pressed and increase or decrease based on selected index.

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                    Console.Beep(37,1);
                    selected--;
                    break;
                    case ConsoleKey.DownArrow:
                    Console.Beep(37,1);
                    selected++;
                    break;
                    case ConsoleKey.Enter:
                    Console.Beep(2000,1);
                    switch (selected)
                    {
                        case 0:
                        RefreshList();
                        break;
                        case 1:
                        Console.Clear();
                        AddScreen();
                        RefreshList();
                        Console.Beep(2000,1);
                        break;
                        case 2:
                        Console.Clear();
                        UpdateScreen();
                        RefreshList();
                        Console.Beep(2000,1);
                        break;
                        case 3:
                        Console.Clear();
                        RemoveScreen();
                        RefreshList();
                        Console.Beep(2000,1);
                        break;
                        case 4:
                        Environment.Exit(0);
                        break;
                    }
                    break;
                }

                //Selected index cycles through the options.

                if(selected == options.Length)
                {
                    selected = 0;
                }
                else if (selected < 0)
                {
                    selected = options.Length-1;
                }          

            }

        }

        //Show all items from list.
        static void RefreshList()
        {
            
            Console.Clear();
            Console.SetCursorPosition(0, 12);
             foreach (var item in _seriesRepo.GetAllSeries())
            {
                var line = string.Format("| {0,-5}| {3,-13}| {1,-40}| {2} |",item.Id, item.Title, item.Year.ToString("0000"), item.Genre.ToString());
                Console.WriteLine($"  {line}");
            }
        
        }

        //Show form and create new item.
        static void AddScreen()
        {
            SerieModel serieModel = new SerieModel();
            Console.WriteLine("\n  Digite o título da série:");
            Console.SetCursorPosition(2, 3);
            serieModel.Title = Console.ReadLine().ToString();
            Console.Clear();

            Console.WriteLine("\n  Digite o gênero da série:");
            Console.SetCursorPosition(2, 5);
            foreach (var item in typeof(Genre).GetEnumValues())
            {
                Console.SetCursorPosition(2, Console.GetCursorPosition().Top);
                Console.WriteLine( (int)item + " - " + item.ToString());
            }
            
            Console.SetCursorPosition(2, 3);
            int.TryParse(Console.ReadLine(), out int result);
            serieModel.Genre = (Genre)result;
            Console.Clear();

            Console.WriteLine("\n  Digite o ano de lançamento da série:");
            Console.SetCursorPosition(2, 3);
            int.TryParse(Console.ReadLine(), out int year);
            serieModel.Year = year;

            _seriesRepo.AddSerie(serieModel);
        }

        //Show form and update the item.
        static void UpdateScreen()
        {
            Console.WriteLine("\n  Digite o id da série:");
            Console.SetCursorPosition(2, 3);
            int.TryParse(Console.ReadLine(), out int id);

            if (_seriesRepo.SelectSerie(id) != null)
            {
                Console.Clear();
                SerieModel serieNew = new SerieModel();

            Console.WriteLine("\n  Alterendo o título da série para:");
            Console.SetCursorPosition(2, 3);
            serieNew.Title = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("\n  Alterendo o gênero da série para:");
            Console.SetCursorPosition(2, 5);
            foreach (var item in typeof(Genre).GetEnumValues())
            {
                Console.SetCursorPosition(2, Console.GetCursorPosition().Top);
                Console.WriteLine( (int)item + " - " + item.ToString());
            }

            Console.SetCursorPosition(2, 3);
            int.TryParse(Console.ReadLine(), out int genre);
            serieNew.Genre = (Genre)genre;
            Console.Clear();

            Console.WriteLine("\n  Alterendo o ano de lançamento da série para:");
            Console.SetCursorPosition(2, 3);
            int.TryParse(Console.ReadLine(), out int year);
            serieNew.Year = year;

            _seriesRepo.UpdateSerie(serieNew, id);
            }
            
        }

        //Show form and remove the item.
        static void RemoveScreen()
        {
            Console.WriteLine("\n  Digite o id da série que deseja remover:");
            Console.SetCursorPosition(2, 3);
            int.TryParse(Console.ReadLine(), out int id);
           _seriesRepo.RemoveSerie(id);
        }

    }
}
