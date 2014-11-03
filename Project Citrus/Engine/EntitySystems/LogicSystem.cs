using Microsoft.Xna.Framework;
using Project_Citrus.Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_Citrus.Engine.EntitySystems
{
    class LogicSystem : EntitySystem
    {
        public override string[] Required_Components
        {
            get { return new String[] { "logic_script" }; }
        }

        //params[0] = GameTime
        public override void Execute(params object[] param)
        {
            GameTime gameTime = (GameTime)param[0];
            foreach (Entity entity in entities)
            {
                LogicScript logicScript = (LogicScript)entity.Get_Component("logic_script");
                if (logicScript != null && logicScript.Script != null)
                    try
                    {
                        Program.pLuaVM.DoFile(@"C:\Users\Alex\Documents\visual studio 2013\Projects\Project Citrus\Project Citrus\res\scripts\" + logicScript.Script);
                        Program.pLuaVM.GetFunction("Execute").Call(entity,Citrus.keyboardManager);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.Message);
                    }
            }
        }

        public override bool Initialize(params object[] param)
        {
            return true;
        }
    }
}
