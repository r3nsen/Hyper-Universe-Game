using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hyper_Galaxy.sound
{
    class SoundManager
    {
        private Song[] songs;
        private SoundEffect[] effects;
        public int pan;
        public int volume;
        
        public SoundManager()
        {
            songs = new Song[1];
            effects = new SoundEffect[0];
        }

        public void PlaySong(int i, bool repeat)
        {
            MediaPlayer.Play(songs[i]);
            MediaPlayer.IsRepeating = repeat;
        }

        public void StopCurrentSong()
        {
            MediaPlayer.Stop();
        }

        public void PlayEffect(int effectNumber)
        {
            effects[effectNumber].Play();
        }

        public void AddSong(Song song, int position)
        {
            songs[position] = song;
            
        }
        public void AddEffect(SoundEffect effect, int position)
        {
            effects[position] = effect;
        }

    }
}
