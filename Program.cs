using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("......:::::[Start]:::::.....");
            Console.ResetColor();
            string commande = "";

            Directory currentDir = new Directory("[/]", true);
            File curent = new Directory("[/]", true);


            while (commande != "exit")
            {
                try
                {
                    currentDir = (Directory)curent;
                }
                catch
                {

                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(curent.GetName() + "# ");
                Console.ResetColor();
                string mot1 = "";
                string mot2 = "";
                string mot3 = "";
                commande = Console.ReadLine();
                string[] commandeTab = commande.Split(' '); //Permet de metre dans un tableau tout les mots séparéments
                if (commandeTab.Length == 3)
                {
                    mot1 = commandeTab[0];
                    mot2 = commandeTab[1];
                    mot3 = commandeTab[2];
                }
                if (commandeTab.Length == 2)
                {
                    mot1 = commandeTab[0];
                    mot2 = commandeTab[1];
                }
                if (commandeTab.Length == 1)
                {

                    mot1 = commandeTab[0];
                }
                if (commandeTab.Length > 3)
                {
                    Console.WriteLine("Votre saisie est inexacte");
                }
                else if (commandeTab[0] == null)
                {
                }
                else
                {

                    if (mot1 == "mkdir")
                    {

                        bool dossieCree = currentDir.Mkdir(mot2);
                        if (dossieCree == true)
                        {
                            Console.WriteLine("Dossier Créé");
                        }


                    }
                    else if (mot1 == "create") // meme principe que mkdir
                    {
                        bool dossieCree = currentDir.CreateNewFile(mot2);
                        if (dossieCree == true)
                        {
                            Console.WriteLine("Fichier Créé");
                        }
                    }
                    else if (mot1 == "ls")
                    {


                        List<File> listeFils = currentDir.Ls(); // vas chercher la liste de file du dossier

                        foreach (File f in listeFils)
                        {
                            string genre;

                            if (f.IsDirectory() == true) // vérifi si c'est un direcory pour pouvoir l'afficher
                            {
                                genre = "(D) ";
                            }
                            else
                            {
                                genre = "(F) ";
                            }
                            string wer;
                            if (f.CanWrite() == true) // demande les permition
                            {
                                wer = "W";
                            }
                            else
                            {
                                wer = "*";
                            }

                            if (f.CanExecute() == true)
                            {
                                wer += "E";
                            }
                            else
                            {
                                wer += "*";
                            }

                            if (f.CanRead() == true)
                            {
                                wer += "R";
                            }
                            else
                            {
                                wer += "*";
                            }

                            String name = f.GetName();
                            Console.WriteLine(genre + "  " + wer + "  " + name);
                        }
                    }
                    else if (mot1 == "cd")
                    {
                        File dossierCd = currentDir.Cd(mot2);
                        if (dossierCd != null)      //Si dossier n'est pas null: remplacer le courent par le fichier voulu
                        {
                            curent = dossierCd;
                        }
                        else
                        {
                            Console.WriteLine("Ce dossier n'existe pas");
                        }
                    }
                    else if (mot1 == "parent")
                    {

                        if (curent.racine == true) //Vérifi si vous n'êtes pas au bout du monde
                        {
                            Console.WriteLine("Vous êtes a la racine");
                        }
                        else
                        {
                            curent = curent.GetParent();
                        }
                    }
                    else if (mot1 == "root") //afficher le premier dossier après la racine
                    {
                        File root = curent.getRoot();
                        Console.WriteLine(root.GetName());
                    }
                    else if (mot1 == "path")
                    {

                        Console.WriteLine(curent.GetPath()); //vas chercher le path et l'affiche
                    }
                    else if (mot1 == "search")
                    {
                        if (curent.IsDirectory() == true)
                        {
                            List<File> fileTrouve = new List<File>();
                            fileTrouve = currentDir.Search(mot2);
                            if (fileTrouve.Count != 0)
                            {
                                foreach (File f in fileTrouve) //pour tout les fichier trouvé, affiche le path
                                {
                                    Console.WriteLine(f.GetPath());
                                }
                            }
                            else
                            {
                                Console.WriteLine("Se fichier n'existe pas");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Vous ne pouvez pas car vous êtes dans un Fichier");
                        }
                    }
                    else if (mot1 == "rename")
                    {
                        if (curent.IsDirectory() == true)
                        {
                            if (currentDir.Cd(mot2) != null) // vérifier que le fichier existe
                            {
                                File fichierRenomer = currentDir.Cd(mot2);
                                bool reponse = fichierRenomer.RenameTo(mot3);
                                if (reponse == true)
                                {
                                    Console.WriteLine("Fichier Bien renomé");
                                }
                                else
                                {
                                    Console.WriteLine("Fichier non renomé !");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ce fichier n'existe pas !");

                            }

                        }
                        else
                        {
                            Console.WriteLine("Vous ne pouvez pas car vous êtes dans un Fichier");
                        }

                    }
                    else if (mot1 == "file")
                    {
                        bool isFile = curent.IsFile();
                        if (isFile == true)
                        {
                            Console.WriteLine("Vous êtes dans un fichier");
                        }
                        else
                        {
                            Console.WriteLine("Vous n'êtes pas dans un fichier");
                        }
                    }
                    else if (mot1 == "directory")
                    {
                        bool isDirectory = curent.IsDirectory();
                        if (isDirectory == true)
                        {
                            Console.WriteLine("Vous êtes dans un Dossier");
                        }
                        else
                        {
                            Console.WriteLine("Vous n'êtes pas dans un Dossier");
                        }
                    }
                    else if (mot1 == "delete")
                    {
                        if (curent.IsDirectory() == true) // vérifi que l'on se trouve bien dans un dossier
                        {

                            bool estSuppr = currentDir.Delete(mot2);
                            if (estSuppr == true)
                            {
                                Console.WriteLine("Fichier supprimé");
                            }

                        }
                        else
                        {
                            Console.WriteLine("Vous ne pouvez pas car vous êtes dans un Fichier");
                        }

                    }
                    else if (mot1 == "chmod")
                    {
                        if (IsNumeric(mot2) == true)
                        {
                            if (int.Parse(mot2) == 1 | int.Parse(mot2) == 2 | int.Parse(mot2) == 4 | int.Parse(mot2) == 6 | int.Parse(mot2) == 7)
                            {
                                curent.Chmod(int.Parse(mot2));
                            }
                            else
                            {
                                Console.WriteLine("Nombre non reconnu pour chmod");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Un nombre est requi");
                        }
                    }
                    else if (mot1 == "name")
                    {
                        Console.WriteLine(curent.GetName());
                    }

                    else
                    {
                        Console.WriteLine("Faute de frappe ou commande inconnu");
                    }
                }
            }

        }
        static bool IsNumeric(string Nombre)
        {
            try
            {
                int.Parse(Nombre);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
