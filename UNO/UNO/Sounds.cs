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
        private static bool Mute = false;
        private static double SFXVolume = 2;
        private static double MusicVolume = 1;
        private static double AmbientVolume = 1;

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
            if (Mute)
            {
                return 0;
            }
            else
            {
                return SFXVolume;
            }
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
            if (Mute)
            {
                return 0;
            }
            else
            {
                return MusicVolume;
            }
        }

        /// <summary>
        /// setst he ambient audio 
        /// </summary>
        /// <param name="level"></param>
        public static void SetAmbient(double level)
        {
            AmbientVolume = level;
        }

        /// <summary>
        /// getst he ambient volume
        /// </summary>
        /// <returns></returns>
        public static double GetAmbient()
        {
            if (Mute)
            {
                return 0;
            }
            else
            {
                return AmbientVolume;
            }
        }

        /// <summary>
        /// sets whether the sounds are muted or not
        /// </summary>
        /// <param name="mute"></param>
        public static void SetMute(bool mute)
        {
            Mute = mute;
        }

        /// <summary>
        /// gets whether the audio is muted or not
        /// </summary>
        /// <returns></returns>
        public static bool IsMuted()
        {
            return Mute;
        }
    }
}
