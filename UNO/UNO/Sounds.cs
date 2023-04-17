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
        
        private static int SFXOutput = 100;
        private static int MusicOutput = 100;


        /// <summary>
        /// sets the master volume
        /// </summary>
        /// <param name="level"></param>
        public static void SetMaster(int level)
        {
            MasterVolume = level;
            ConvertAudio();
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
            ConvertAudio();
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
            ConvertAudio();
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
            return SFXOutput;
        }

        /// <summary>
        /// returns the Music output in comparison with the Master Volume
        /// </summary>
        /// <returns></returns>
        public static int GetConvertedMusic()
        {
            return MusicOutput;
        }

        /// <summary>
        /// calculates the converted audio 
        /// </summary>
        private static void ConvertAudio()
        {
            SFXOutput = MasterVolume * (SFXVolume / 100);
            MusicOutput = MasterVolume * (MusicVolume / 100);
        }
    }
}
