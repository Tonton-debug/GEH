using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineHrundelGUI
{
    interface ICommand
    {
        void Exit();
        void RunDLL(string path);
        void RotateCamera(int angleX,int angleY);
    }
}
