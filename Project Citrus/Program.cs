#region Using Statements
using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using NLua;
using System.Reflection;
using Project_Citrus.lua;
using System.Collections;
#endregion

namespace Project_Citrus
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        private static Lua pLuaVM = new Lua();
        public static Hashtable pLuaFuncs = new Hashtable();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            String working_dir = @"C:\Users\Alex\Documents\visual studio 2013\Projects\Project Citrus\Project Citrus\res\scripts\";

            using (var game = new Citrus())
            {
                registerLuaFunctions(game);
                //pLuaVM.RegisterFunction("outString", typeof(DeepCopy).GetMethod("outString"));
                //pLuaVM.DoFile(@working_dir + "script.lua");
                game.Run();
            }
        }

        public static void registerLuaFunctions(Object pTarget)
        {
            // Sanity checks
            if (pLuaVM == null || pLuaFuncs == null)
                return;

            // Get the target type
            Type pTrgType = pTarget.GetType();

            // ... and simply iterate through all it's methods
            foreach (MethodInfo mInfo in pTrgType.GetMethods())
            {
                // ... then through all this method's attributes
                foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                {
                    // and if they happen to be one of our AttrLuaFunc attributes
                    if (attr.GetType() == typeof(LuaFunctionAttr))
                    {
                        LuaFunctionAttr pAttr = (LuaFunctionAttr)attr;
                        Hashtable pParams = new Hashtable();

                        // Get the desired function name and doc string, along with parameter info
                        String strFName = pAttr.getFuncName();
                        String strFDoc = pAttr.getFuncDoc();
                        String[] pPrmDocs = pAttr.getFuncParams();

                        // Now get the expected parameters from the MethodInfo object
                        ParameterInfo[] pPrmInfo = mInfo.GetParameters();

                        // If they don't match, someone forgot to add some documentation to the
                        // attribute, complain and go to the next method
                        if (pPrmDocs != null && (pPrmInfo.Length != pPrmDocs.Length))
                        {
                            Console.WriteLine("Function " + mInfo.Name + " (exported as " +
                                              strFName + ") argument number mismatch. Declared " +
                                              pPrmDocs.Length + " but requires " +
                                              pPrmInfo.Length + ".");
                            break;
                        }

                        // Build a parameter <-> parameter doc hashtable
                        for (int i = 0; i < pPrmInfo.Length; i++)
                        {
                            pParams.Add(pPrmInfo[i].Name, pPrmDocs[i]);
                        }

                        // Get a new function descriptor from this information
                        LuaFuncDescriptor pDesc = new LuaFuncDescriptor(strFName, strFDoc, new ArrayList(pParams.Keys), new ArrayList(pParams.Values));

                        // Add it to the global hashtable
                        pLuaFuncs.Add(strFName, pDesc);

                        // And tell the VM to register it.
                        pLuaVM.RegisterFunction(strFName, pTarget, mInfo);
                    }
                }
            }
        }
    }
#endif
}
