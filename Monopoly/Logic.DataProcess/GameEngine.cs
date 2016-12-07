using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DataProcess
{
    //Основные методы игры
    public class GameEngine
    {
        #region Singleton
        private static GameEngine instance;

        public static GameEngine getInstance()
        {
            if (instance == null)
                instance = new GameEngine();
            return instance;
        }
        #endregion Singleton

        #region Random
        static Random random;
        /// <summary>
        /// Создаем объект типа Random
        /// </summary>
        static void SeedRandom()
        {
            random = new Random();
        }
        #endregion

        public GameEngine()
        {
            SeedRandom();
        }

        
    }
}
