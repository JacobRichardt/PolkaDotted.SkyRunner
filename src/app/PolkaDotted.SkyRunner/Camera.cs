using OpenTK;
using OpenTK.Graphics.OpenGL;
using PolkaDotted.SkyRunner.Entities;

namespace PolkaDotted.SkyRunner
{
	public class Camera
	{
		private readonly GameWindow _gameWindow;
		private IFollowableEntity _entity;

		public Camera(GameWindow gameWindow)
		{
			_gameWindow = gameWindow;
		}

		public void SetTarget(IFollowableEntity entity)
		{
			_entity = entity;
		}

		public void Update()
		{
			if (_entity == null)
				GL.Ortho(0, _gameWindow.Width, 0, _gameWindow.Height, 0, 10);
			else
			{
				var center = _entity.FollowPoint.ToDisplayTK();
				var halfWidth = _gameWindow.Width / 2d;
				var halfHeight = _gameWindow.Height / 2d;

				GL.Ortho(center.X - halfWidth, center.X + halfWidth, center.Y - halfHeight, center.Y + halfHeight, 0, 10);
			}
		}
	}
}