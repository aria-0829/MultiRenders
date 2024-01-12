using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Windows.Forms;

namespace MultiRenders
{
    internal class Models
    {
        public Model Mesh { get; set; }
        public Matrix Translation { get; set; }
        public Matrix Rotation { get; set; }
        public Matrix Scale { get; set; }
        public Effect Shader { get; set; }

        //Texturing
        public Texture Texture { get; set; }

        //Lighting Variables
        public Vector3 DiffuseColor { get; set; }
        public Vector3 SpecularColor { get; set; }
        public float SpecularPower { get; set; }

        public Vector3 LightColor { get; set; }
        public Vector3 LightDirection { get; set; }

        public Models(Model _mesh, Vector3 _position, float _scale)
        {
            Mesh = _mesh;
            Translation = Matrix.CreateTranslation(_position);
            Rotation = Matrix.Identity;
            Scale = Matrix.CreateScale(_scale);
            DiffuseColor = new Vector3(1, 1, 1);
            SpecularColor = new Vector3(0, 0, 1); //Blue
            SpecularPower = 4;
            LightDirection = new Vector3(0, -2, 1);
            LightColor = new Vector3(1, 1, 1); //White
        }

        public void SetShader(Effect _effect)
        {
            Shader = _effect;

            foreach (ModelMesh mesh in Mesh.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = Shader;
                }
            }
        }

        public void SetTexture(Texture _texture)
        {
            Texture = _texture;
        }

        public Matrix GetTransform()
        {
            return Scale * Rotation * Translation;
        }

        public void UpdatePosColorParameters(Matrix _view, Matrix _projection, Vector3 _cameraPosition)
        {
            Shader.Parameters["World"].SetValue(GetTransform());
            Shader.Parameters["WorldViewProjection"].SetValue(GetTransform() * _view * _projection);
            Shader.Parameters["Texture"].SetValue(Texture);
            Shader.Parameters["DiffuseColor"].SetValue(DiffuseColor);
        }
        
        public void UpdatePhongParameters(Matrix _view, Matrix _projection, Vector3 _cameraPosition)
        {
            Shader.Parameters["World"].SetValue(GetTransform());
            Shader.Parameters["WorldViewProjection"].SetValue(GetTransform() * _view * _projection);
            Shader.Parameters["Texture"].SetValue(Texture);
            Shader.Parameters["CameraPosition"].SetValue(_cameraPosition);
            Shader.Parameters["DiffuseColor"].SetValue(DiffuseColor);
            Shader.Parameters["SpecularColor"].SetValue(SpecularColor);
            Shader.Parameters["SpecularPower"].SetValue(SpecularPower);
            Shader.Parameters["LightDirection"].SetValue(LightDirection);
            Shader.Parameters["LightColor"].SetValue(LightColor);
        }

        public void Render()
        {
            foreach (ModelMesh mesh in Mesh.Meshes)
            {
                mesh.Draw();
            }
        }
    }
}
