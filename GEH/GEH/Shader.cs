using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Threading;
namespace GEH
{
   public  class Shader:Component
    {
        public bool ShaderIsLoad;
        private int _idVertexShader;
        private int _idFragmentShader;
        private Dictionary<string, Matrix4> _dictionaryForShaderUniforms = new Dictionary<string, Matrix4>();
      
        public int Handle { get; private set; }
        public Shader()
        {

        }
        public void AddUniform(string nameUniformInShader,Matrix4 matrixForUniform)
        {
            Thread.Sleep(1);
            _dictionaryForShaderUniforms.Add(nameUniformInShader, matrixForUniform);
            Console.WriteLine("Fine");
        }
        private void SetAllUniforms()
        {
            foreach (var item in _dictionaryForShaderUniforms)
            {
             //  Thread.Sleep(1);
                int location = GL.GetUniformLocation(Handle, item.Key);
          
                Matrix4 sendMatrix = item.Value;
                GL.UniformMatrix4(location,false,ref sendMatrix);
                Console.WriteLine("\n"+item.Value+"\n"+ Handle);
            }
        }
        public void LoadShaders(string pathShaderVetrex, string pathShaderColor)
        {
            string VertexShaderSource;

            using (StreamReader reader = new StreamReader(pathShaderVetrex, Encoding.UTF8))
            {
                VertexShaderSource = reader.ReadToEnd();
            }

            string FragmentShaderSource;

            using (StreamReader reader = new StreamReader(pathShaderColor, Encoding.UTF8))
            {
                FragmentShaderSource = reader.ReadToEnd();
            }
            _idVertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(_idVertexShader, VertexShaderSource);

            _idFragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(_idFragmentShader, FragmentShaderSource);
            GL.CompileShader(_idVertexShader);

            string infoLogVert = GL.GetShaderInfoLog(_idVertexShader);
            if (infoLogVert != String.Empty)
                Console.WriteLine(infoLogVert);

            GL.CompileShader(_idFragmentShader);

            string infoLogFrag = GL.GetShaderInfoLog(_idFragmentShader);

            if (infoLogFrag !=String.Empty)
                Console.WriteLine(infoLogFrag);
        
            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, _idVertexShader);
            GL.AttachShader(Handle, _idFragmentShader);
         
            GL.LinkProgram(Handle);
            GL.DetachShader(Handle, _idVertexShader);
            GL.DetachShader(Handle, _idFragmentShader);
            GL.DeleteShader(_idFragmentShader);
            GL.DeleteShader(_idVertexShader);

            ShaderIsLoad = true;
          
        }
        public void Use()
        {
          
            GL.UseProgram(Handle);
            SetAllUniforms();
        }
    }
}
