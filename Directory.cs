using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    public class Directory : File
    {


        List<File> listeFils = new List<File>();
        public Directory(string name, Directory parent)
            : base(name, parent)
        {
        }

        public Directory(string name, bool racine)
            : base(name, racine)
        {
        }


        public bool Mkdir(string name)
        {


            if (this.IsDirectory() == true) //Vérifi si c'est bien un dossier
            {
                if (this.Cd(name) == null) //Vérifi si se fihier n'existe pas déjà
                {

                    if (this.CanWrite() == true)
                    {
                        File newDirectory = new Directory(name, this); //cré le nouveau dossier
                        listeFils.Add(newDirectory);//et on l'ajout à la liste de fils du parent
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Erreur, Dossier non créé, vous n'aves pas les droits.");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Se dossier existe déjà");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Vous êtes pas dans un dossier tu es dans un fichier");
                return false;
            }

        }

        public File Cd(string name)
        {
            if (this.CanRead() == true)
            {
                if (this.IsDirectory() == true)
                {


                    foreach (File f in listeFils)// compare pour chaque fils le nom voulu et le retourne si il le trouve
                    {
                        if (name == f.GetName())
                        {
                            return f;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Vous ne pouvez pas car vous êtes dans un Fichier");
                }
            }
            return null;


        }

        public List<File> Ls()
        {
            if (this.CanRead() == true)
            {
                return listeFils;
            }
            return null;
        }

        public bool CreateNewFile(string name)//même principe que mkdir
        {


            if (this.IsDirectory() == true)
            {

                if (this.Cd(name) == null) //Vérifi si se fihier n'existe pas déjà
                {



                    if (this.CanWrite() == true)
                    {
                        File newFile = new File(name, this);
                        listeFils.Add(newFile);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Erreur, Dossier non créé, vous n'aves pas les droits.");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Se Fichier existe déjà");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Vous ne pouvez pas car vous êtes dans un Fichier");
                return false;
            }
        }
        public List<File> Search(string name)
        {

            List<File> listSearch = new List<File>();
            if (this.CanRead() == true)
            {
                foreach (File f in listeFils)//compare les fils avec le nom charché
                {
                    if (name == f.GetName())
                    {
                        listSearch.Add(f);
                    }
                    if (f.IsDirectory() == true)// si c'est un dossier il rappelle la fonction pour qu'elle cherche dedans
                    {
                        Directory test = (Directory)f;
                        List<File> resultat = test.Search(name);
                        foreach (File d in resultat)
                        {
                            listSearch.Add(d);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Vous n'avez pas les droits");
            }
            return listSearch;
        }
        public bool Delete(string fileSuppr)
        {         
            if (this.Cd(fileSuppr) != null)// que le dossier existe
            {
                
                if (this.Cd(fileSuppr).CanWrite() == true)//si le fichier voulu trouver dans la listeFils peut être modifier alors le modifier
            {
                listeFils.Remove(this.Cd(fileSuppr));
                return true;
            }
                else
                {
                    Console.WriteLine("Fichier non supprimmé, vous n'avez pas les droits");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Ce fichier n'existe pas !");
                return false;
            }

        }



    }
}
