using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Web;

namespace WorkTime.Utils.Dto
{
    /// <summary>
    /// DToFactory
    /// внутренний синглтон сборки. Используется для создания и межпотокового доступа к 
    ///     AssemblyBuilder'у и сгенерированным типам в динамической сборки
    /// </summary>
    /// 

    public sealed class DtoFactory
    {

        internal static string AssemblyName = "DtoFactory.DynamicAssembly";
        internal static string ModuleName = "DtoFactory.DynamicModule";



        internal Type GetDynamicType(string TypeName)
        {
            lock (syncRoot)
            {
                try
                {
                    return AppDomain
                        .CurrentDomain
                        .GetAssemblies()
                        .Where(
                            a => a.GetName()
                                    .Name == AssemblyName
                        )
                        .ToList()
                        .SelectMany(
                            a => a
                                    .GetTypes()
                        )
                        .ToList()
                        .FirstOrDefault(
                            t => t
                                    .Name == TypeName
                        );
                }
                catch
                {
                    return null;
                }
            }
        }


        internal bool TypeExists(string TypeName)
        {
            lock (syncRoot)
            {
                try
                {
                    return (
                        AppDomain
                            .CurrentDomain
                            .GetAssemblies()
                            .Where(
                                a => a
                                        .GetName()
                                        .Name == AssemblyName
                            )
                            .ToList()
                            .SelectMany(
                                a => a
                                        .GetTypes()
                            )
                            .ToList()
                            .Count(
                                t => t
                                        .Name == TypeName
                            ) > 0
                    );
                }
                catch
                {
                    return false;
                }
            }
        }

        private static volatile DtoFactory instance;
        private static object syncRoot = new Object();

        public AssemblyBuilder Builder
        {
            get
            {
                lock (syncRoot)
                {

                    if ((AssemblyBuilder)AppDomain.CurrentDomain.GetData(AssemblyName) == null)
                    {
                        AppDomain
                            .CurrentDomain
                            .SetData(
                                AssemblyName,
                                AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(AssemblyName), AssemblyBuilderAccess.Run)
                            );
                    }
                    return (AssemblyBuilder)AppDomain.CurrentDomain.GetData(AssemblyName);
                }
            }
        }


        private DtoFactory() { }

        public static DtoFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DtoFactory();
                    }
                }

                return instance;
            }
        }
    }
}