using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace EversonTGS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //verficar se existe banco
            ArquivoExistis();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void ArquivoExistis()
        {
            if (System.IO.Directory.Exists(@"C:\teste\"))
            {
                if (System.IO.File.Exists(@"C:\teste\acme.sqlite"))
                {
                    DataBase.ConectarBanco();
                    DataBase.CriarTabela();

                }
            }
            else
            {
                System.IO.Directory.CreateDirectory(@"C:\teste\");
                DataBase.CriarBanco();
                ArquivoExistis();
            }
            //@"C:\SQLite\
        }
    }
}
