using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace PolkaDotted.SkyRunner
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var game = new GameWindow(1600, 900))
			{
				game.Location = new Point(30, 30);

				game.Load += (sender, e) =>
				{
					game.VSync = VSyncMode.On;
				};

				game.Resize += (sender, e) =>
				{
					GL.Viewport(0, 0, game.Width, game.Height);
				};

				using (new SkyRunnerGame(game))
				{
					game.Run(60.0);
				}
			}
		}
	}
}
