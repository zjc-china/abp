using System;
using System.Collections.Generic;
using System.Runtime.Loader;

namespace Volo.Abp.Modularity.PlugIns;

public class FilePlugInSource : IPlugInSource
{
    public string[] FilePaths { get; }

    /// <summary>
    /// STUDYCODE:params 关键字用于定义方法的可变参数列表,只能用在方法的最后一个参数
    /// 实例:new FilePlugInSource("2","1","3")
    /// </summary>
    /// <param name="filePaths"></param>
    public FilePlugInSource(params string[]? filePaths)
    {
        FilePaths = filePaths ?? new string[0];
    }

    public Type[] GetModules()
    {
        var modules = new List<Type>();

        foreach (var filePath in FilePaths)
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(filePath);

            try
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (AbpModule.IsAbpModule(type))
                    {
                        modules.AddIfNotContains(type);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new AbpException("Could not get module types from assembly: " + assembly.FullName, ex);
            }
        }

        return modules.ToArray();
    }
}
