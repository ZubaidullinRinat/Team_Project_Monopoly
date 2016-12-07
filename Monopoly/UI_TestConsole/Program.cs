using Logic.DataProcess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_TestConsole.Models;

namespace UI_TestConsole
{
    class Program
    {
        Repository r;
        static void Main(string[] args)
        {           
            Program p = new Program();                            
            Console.ReadKey();
        }
        /// <summary>
        /// Этот констурктор - тестовая точка входа
        /// </summary>
        public Program()
        {
            //Новый репозиторий
            r = Repository.getInstance();

            //Этот кусок будет вызывать новую viewModel для определения количества пользователей
            r.Session.Users = new List<User>
            {
                new User("John"),
                new User("Andrew"),
                new User("Bob"),
                new User("Max")
            };
            

        }
    }  
        
}
