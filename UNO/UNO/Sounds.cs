using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO
{
    internal class Sounds
    {
        private static int MasterVolume = 100;
        private static int SFXVolume = 100;
        private static int MusicVolume = 100;


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
        public static int GetMaster() 
        { 
            return MasterVolume;
        }

        /// <summary>
        /// sets the SFX volume
        /// </summary>
        /// <param name="level"></param>
        public static void SetSFX(int level) 
        { 
            SFXVolume = level;
        }

        /// <summary>
        /// gets the SFX volume
        /// </summary>
        /// <returns></returns>
        public static int GetSFX() 
        { 
            return SFXVolume;
        }

        /// <summary>
        /// sets the music volume
        /// </summary>
        /// <param name="level"></param>
        public static void SetMusic(int level)
        {
            MusicVolume = level;
        }

        /// <summary>
        /// gets the music volume
        /// </summary>
        /// <returns></returns>
        public static int GetMusic() 
        { 
            return MusicVolume;
        }

        /// <summary>
        /// returns the SFX output in comparison with the Master Volume
        /// </summary>
        /// <returns></returns>
        public static int GetConvertedSFX()
        {
            return MasterVolume * (SFXVolume / 100);
        }

        /// <summary>
        /// returns the Music output in comparison with the Master Volume
        /// </summary>
        /// <returns></returns>
        public static int GetConvertedMusic()
        {
            return MasterVolume * (MusicVolume / 100);
        }
    }
}
