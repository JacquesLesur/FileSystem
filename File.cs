using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    class File
    {
        string name;
        Directory parent;
        public bool racine { get; set; }
        int permission = 4;

        public File(string name, bool racine) //constructeur de la racine
        {
            this.name = name;
            this.racine = racine;
        }
        public File(string name, Directory parent) //constructeur des autre files
        {
            this.name = name;
            this.parent = parent;
            
        }
        public string GetName(){
        return name;
        }
        public bool RenameTo(string name)
        {
           
            if (this.CanWrite() == true) //Vérifi que l'on peu écrir dans le fichier en cours
            {
                this.name = name;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsDirectory() //demande si c'est un directory
        {
            if (this.GetType().ToString() == "FileSystem.Directory")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsFile()
        {
            if (this.GetType().ToString() == "FileSystem.File")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Directory GetParent()
        {
            return parent;
        }
        
        public string GetPath()
        {
            File path = this;

            List<string> nameParent = new List<string>();
            String pathComplet = "";
            while (path.racine != true) //tant que l'on est pas a la racine sela stock le nom du parent
            {

                path = path.GetParent();
                nameParent.Add(path.GetName());
            }
            for (int i = nameParent.Count - 1; i >= 0; i = i - 1)
            {
                if (i == 0)
                {
                    pathComplet +=nameParent[i];
                }
                else
                {

                    pathComplet += "\\" + nameParent[i];
                }
            }
            return pathComplet += "\\" + this.GetName();
        }

        public bool CanWrite()
        {
            return (permission & 2) > 0;
        }
        public bool CanExecute()
        {
            return (permission & 1) > 0;
        }
        public bool CanRead()
        {
            return (permission & 4) > 0;
        }

        public void Chmod(int nbrPermission)
        {
            permission = nbrPermission;
        }
        public File getRoot()
        {
            File root = this;
            while (root.GetParent().racine != true) //tant que la racine du parent n'est pas le bout du monde, on remonte au prent
            {

                root = root.GetParent();
                
            }
            return root;
        }
        

        
    }
}
