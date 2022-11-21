using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OthelloMinMaxAI
{
    class ParticleEngine
    {
        //Random random;
        Vector2 startLocation;
        List<Particle> particles;

        public ParticleEngine(Vector2 pos, List<Texture2D> textures, int num, Color color, int direction, Random random)
        {
            random = new Random();
            startLocation = pos;

            particles = new List<Particle>();

            for (int i = 0; i < num; i++)
            {
                Particle particle = new Particle(startLocation, textures[random.Next(textures.Count())], random.Next(20, 31) / 10, 0.1f * (float)(random.NextDouble() * 2 - 1), Randomize(color, random), direction * random.Next(30, 90), random.Next(45 + direction * 45, 136 + direction * 45));

                particles.Add(particle);
            }

        }

        public Color Randomize(Color color, Random random)
        {
            return new Color(color, (float)random.Next(7, 10) / 10);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Particle p in particles)
            {
                p.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (Particle p in particles)
            {
                p.Draw(sb);
            }
        }
    }

}
