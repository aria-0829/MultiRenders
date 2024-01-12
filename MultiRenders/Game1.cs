using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MultiRenders
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _sprite;
        private SpriteFont _font;

        private Models teapot;
        private Models sphere;
        private Models cube;
        private List<Models> cubes = new List<Models>();
        private List<Models> cubesToRemove = new List<Models>();

        private Effect posColor;
        private Effect phong;

        private Texture texSmiley;
        private Texture texMetal;

        private Vector3 cameraPosition = new Vector3(0, 40, 40);
        private Matrix world = Matrix.Identity;
        private Matrix view = Matrix.Identity;
        private Matrix projection = Matrix.Identity;

        private ToolBox toolBox;
        private MouseState mouseState;
        private MouseState previousMouseState;
        private Vector3 teapotPosition = new Vector3(0, 0, 0);
        private Vector3 lightDirection = new Vector3(0, 0, 1);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _sprite = new SpriteBatch(GraphicsDevice);

            toolBox = new ToolBox();
            toolBox.Show();
            toolBox.TopMost = true;

            world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
            view = Matrix.CreateLookAt(new Vector3(0, 0, 5), new Vector3(0, 0, 0), Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), _graphics.GraphicsDevice.Viewport.AspectRatio, 0.1f, 100f);
            
            Mouse.SetPosition(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            mouseState = Mouse.GetState();
            previousMouseState = Mouse.GetState();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _font = Content.Load<SpriteFont>("Arial14");

            posColor = Content.Load<Effect>("PosColor");
            phong = Content.Load<Effect>("Phong");

            texMetal = Content.Load<Texture2D>("Metal");
            texSmiley = Content.Load<Texture2D>("Smiley2");

            teapot = new Models(Content.Load<Model>("Teapot"), teapotPosition, 5);
            teapot.SetTexture(texSmiley);
            teapot.SetShader(posColor);

            sphere = new Models(Content.Load<Model>("Sphere"), new Vector3(0, 0, 0), 0.5f);
            sphere.SetTexture(texMetal);
            sphere.SetShader(phong);
            sphere.SpecularColor = new Vector3(1, 0, 0);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseState = Mouse.GetState();

            if (toolBox.isPosColor)
            {
                ChangeColorByPosition();
            }
            else if (toolBox.isDynamicLight)
            {
                teapot.Translation = Matrix.CreateTranslation(new Vector3(0, 0, 0));
                ChangeLightDirection();
            }
            else if(toolBox.isMoveCube)
            {
                SpawnRandomCubes();

            }

            previousMouseState = mouseState;

            base.Update(gameTime);
        }

        private void ChangeColorByPosition()
        {
            float moveSpeed = 0.01f;

            float deltaX = mouseState.X - previousMouseState.X;
            float deltaY = mouseState.Y - previousMouseState.Y;
            float deltaZ = (mouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue) / 120;
            teapotPosition.X += deltaX * moveSpeed;
            teapotPosition.Y -= deltaY * moveSpeed;
            teapotPosition.Z += deltaZ * moveSpeed;

            teapot.Translation = Matrix.CreateTranslation(teapotPosition);
        }

        private void ChangeLightDirection()
        {
            float moveSpeed = 0.02f;
           
            if (mouseState != previousMouseState)
            {
                float deltaX = mouseState.X - previousMouseState.X;
                float deltaY = mouseState.Y - previousMouseState.Y;

                lightDirection.X += deltaX * moveSpeed;
                lightDirection.Y -= deltaY * moveSpeed;
                teapot.LightDirection = lightDirection;
            }

            if (toolBox.isButtonClicked)
            {
                lightDirection = new Vector3(0, 0, 1);
                teapot.LightDirection = lightDirection;
                mouseState = Mouse.GetState();
                previousMouseState = Mouse.GetState();

                toolBox.isButtonClicked = false;
            }
        }

        private void SpawnRandomCubes()
        {
            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                Random random = new Random();
                Vector3 cubePosition = new Vector3(random.Next(-5, 5), random.Next(-5, 5), random.Next(-5, 5));
                Models newCube = new Models(Content.Load<Model>("Cube"), cubePosition, 0.5f);
                newCube.SetTexture(texSmiley);
                newCube.SetShader(phong);
                newCube.SpecularColor = new Vector3(1, 0, 0);
                cubes.Add(newCube);
            }

            foreach (var cube in cubes)
            {
                if (cube.Translation.Translation != sphere.Translation.Translation)
                {
                    float moveSpeed = 0.02f;
                    Vector3 cubeMovement = Vector3.Normalize(sphere.Translation.Translation - cube.Translation.Translation) * moveSpeed;
                    cube.Translation *= Matrix.CreateTranslation(cubeMovement);
                    if (Vector3.Distance(cube.Translation.Translation, sphere.Translation.Translation) <= moveSpeed)
                    {
                        cubesToRemove.Add(cube);
                    }
                }
                else
                {
                    cubesToRemove.Add(cube);
                }
            }

            foreach (var cubeToRemove in cubesToRemove)
            {
                cubes.Remove(cubeToRemove);
            }
            cubesToRemove.Clear();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if (toolBox.isPosColor)
            {
                teapot.SetTexture(texSmiley);
                teapot.SetShader(posColor);
                teapot.UpdatePosColorParameters(view, projection, cameraPosition);
                teapot.Render();

                _sprite.Begin();
                string output = "Teapot Position: " + teapotPosition.ToString();
                _sprite.DrawString(_font, output, new Vector2(10, 10), Color.White);
                _sprite.End();
            }

            else if (toolBox.isDynamicLight)
            {
                teapot.SetTexture(texMetal);
                teapot.SetShader(phong);
                teapot.UpdatePhongParameters(view, projection, cameraPosition);
                teapot.Render();

                _sprite.Begin();
                string output = "Light Position: " + teapot.LightDirection.ToString();
                _sprite.DrawString(_font, output, new Vector2(10, 10), Color.White);
                _sprite.End();
            }

            else if (toolBox.isMoveCube)
            {
                sphere.UpdatePhongParameters(view, projection, cameraPosition);
                sphere.Render();
                foreach (Models cube in cubes)
                {
                    cube.UpdatePhongParameters(view, projection, cameraPosition);
                    cube.Render();
                }

                _sprite.Begin();
                string output = "Cubes Count: " + cubes.Count.ToString();
                _sprite.DrawString(_font, output, new Vector2(10, 10), Color.LightGreen);
                _sprite.End();
            }

            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            base.Draw(gameTime);
        }
    }
}