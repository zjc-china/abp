using System;
using System.Collections.Generic;

namespace Volo.Abp.Collections;

/// <summary>
/// A shortcut for <see cref="ITypeList{TBaseType}"/> to use object as base type.
/// </summary>
public interface ITypeList : ITypeList<object>
{

}

/// <summary>
/// Extends <see cref="IList{Type}"/> to add restriction a specific base type.
/// STUDYCODE:
/// https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/in-generic-modifier
/// 对于泛型类型参数，in 关键字可指定类型参数是逆变的。 可以在泛型接口和委托中使用 in 关键字
/// </summary>
/// <typeparam name="TBaseType">Base Type of <see cref="Type"/>s in this list</typeparam>
public interface ITypeList<in TBaseType> : IList<Type>
{
    /// <summary>
    /// Adds a type to list.
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    void Add<T>() where T : TBaseType;

    /// <summary>
    /// Adds a type to list if it's not already in the list.
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    bool TryAdd<T>() where T : TBaseType;

    /// <summary>
    /// Checks if a type exists in the list.
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <returns></returns>
    bool Contains<T>() where T : TBaseType;

    /// <summary>
    /// Removes a type from list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    void Remove<T>() where T : TBaseType;
}
