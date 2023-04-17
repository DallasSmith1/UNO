using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace UNO
{
    internal class Sounds
    {
        private static double MasterVolume = 10;
        private static double SFXVolume = 10;
        private static double MusicVolume = 10;

        /// <summary>
        /// sets the master volume
        /// </summary>
        /// <param name="level"></param>
        public static void SetMaster(int level)
        {
            MasterVolume = level;
        }

        /// <summary>
        /// getst he master volume
        /// </summary>
        /// <returns></returns>
        public static double GetMaster() 
        { 
            return MasterVolume;
        }

        /// <summary>
        /// sets the SFX volume
        /// </summary>
        /// <param name="level"></param>
        public static void SetSFX(double level) 
        { 
            SFXVolume = level;
        }

        /// <summary>
        /// gets the SFX volume
        /// </summary>
        /// <returns></returns>
        public static double GetSFX() 
        { 
            return SFXVolume;
        }

        /// <summary>
        /// sets the music volume
        /// </summary>
        /// <param name="level"></param>
        public static void SetMusic(double level)
        {
            MusicVolume = level;
        }

        /// <summary>
        /// gets the music volume
        /// </summary>
        /// <returns></returns>
        public static double GetMusic() 
        { 
            return MusicVolume;
        }
    }
}
