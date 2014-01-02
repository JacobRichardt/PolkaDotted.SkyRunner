using System;
using System.Collections.Generic;
using System.Drawing;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using PolkaDotted.SkyRunner.Entities;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace PolkaDotted.SkyRunner
{
	public class SkyRunnerGame : IDisposable
	{
		private readonly GameWindow _game;
		private readonly World _farSeer;

		private readonly IList<AEntity> _enitities;

		public SkyRunnerGame(GameWindow game)
		{
			_game = game;
			_farSeer = new World(new Vector2(0, 9));
			ConvertUnits.SetDisplayUnitToSimUnitRatio(0.01f);

			_enitities = new List<AEntity>();

			_game.Load += (o, args) => Load();
			_game.UpdateFrame += (o, args) => Update();
			_game.RenderFrame += (sender, e) => Render();
		}

		private void Load()
		{
			_enitities.Add(new Ball(0, 0, 2));
			_enitities.Add(new Ball(6, 6, 2));
			_enitities.Add(new Ball(-6, -6, 2));
			_enitities.Add(new ControllableBall(0, -6, 2));

			foreach (var enitity in _enitities)
				enitity.Load(_farSeer, _game);
		}

		private void Update()
		{
			if (_game.Keyboard[Key.Escape])
				_game.Exit();

			_farSeer.Step((float)_game.TargetUpdatePeriod);

			foreach (var enitity in _enitities)
				enitity.Update();
		}

		private void Render()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

			foreach (var enitity in _enitities)
				enitity.Draw();

			_game.SwapBuffers();
		}

		public void Dispose()
		{
			
		}
	}
}