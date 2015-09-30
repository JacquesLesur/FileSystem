using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    class Directory: File
    {
        
        
        List<File> listeFils = new List<File>();
        public Directory(string name, Directory parent):base(name, parent)
        {      
        }

        public Directory(string name, bool racine): base(name, racine)
        {
        }


        public bool Mkdir(string name)
        {
            if (this.CanWrite() == true)
            {
                File newDirectory = new Directory(name, this); //cré le nouveau dossier
                listeFils.Add(newDirectory);//et on l'ajout à la liste de fils du parent
                return true;
            }
            return false;
        }
       
        public File Cd(string name)
        {
            foreach(File f in listeFils)// compare pour chaque fils le nom voulu et le retourne si il le trouve
            {   
                    if (name == f.GetName())
                    {
                        return f;
                    }
            }
            return null;
        }

        public List<File> Ls()
        {
            return listeFils;
        }

        public bool CreateNewFile(string name)//même principe que mkdir
        {
            if (this.CanWrite() == true)
            {
                File newFile = new File(name, this);
                listeFils.Add(newFile);
                return true;
            }
            return false;
        }
        public List<File> Search(string name)
        {
            List<File> listSearch = new List<File>();
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
            return listSearch;
        }
        public bool Delete(string fileSuppr)
        {
            if (this.Cd(fileSuppr).CanWrite() == true)//si le fichier voulu trouver dans la listeFils peut être modifier alors le modifier
            {
                listeFils.Remove(this.Cd(fileSuppr));
                return true;
            }
            else
            {

                return false;
            }

        }

        
        
    }
}
