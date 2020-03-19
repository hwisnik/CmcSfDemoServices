
using StructureMap.Building;
using StructureMap.Pipeline;
using System;
using System.Runtime.CompilerServices;
using Shared.Handlers;

#pragma warning disable 1591

namespace CmcSfRestServices.DependencyResolution.Generics
{
    //public class GenericCommandHandlerInstanceFactory : Instance
    //{
    //    //See http://structuremap.github.io/generics/ for reference
    //    public override Instance CloseType(Type[] types)
    //    {
    //        // StructureMap will cache the object built out of this,
    //        // so the expensive Reflection hit only happens
    //        // once
    //        var instanceType = typeof(CommandHandlerDecoratorInstance<>).MakeGenericType(types);
    //        return (Instance)Activator.CreateInstance(instanceType);
    //    }

    //    public override string Description => "Build CommandHandlerDecorator<TCommand>";

    //    public override Type ReturnedType => typeof(CommandHandlerDecorator<>);

    //    public override IDependencySource ToDependencySource(Type pluginType)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}


    //public static class CommandHandlerDecoratorBuilder
    //{
    //    public static ICommandHandler<TCommand> Build<TCommand>()
    //    {
    //        return new CommandHandlerDecorator<TCommand>();
    //    }
    //}


    //public class CommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    //{
    //    public void HandleLog(TCommand command, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)

    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Handle(TCommand command)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}


    //public class CommandHandlerDecoratorInstance<TCommand> : LambdaInstance<ICommandHandler<TCommand>>
    //{
    //    public CommandHandlerDecoratorInstance() : base(() => CommandHandlerDecoratorBuilder.Build<TCommand>())
    //    {
    //    }

    //    // This is purely to make the diagnostic views prettier
    //    public override string Description => $"RepositoryBuilder.Build<{typeof(TCommand).Name}>()";
    //}

    //public class GenericRepositoryInstanceFactory : Instance
    //{
    //    //See http://structuremap.github.io/generics/ for reference
    //    public override Instance CloseType(Type[] types)
    //    {
    //        // StructureMap will cache the object built out of this,
    //        // so the expensive Reflection hit only happens
    //        // once
    //        var instanceType = typeof(GenericRepositoryInstance<>).MakeGenericType(types);
    //        return (Instance)Activator.CreateInstance(instanceType);
    //    }

    //    public override string Description => "Build GenericRepository<TCommand>";

    //    public override Type ReturnedType => typeof(GenericRepository<>);

    //    public override IDependencySource ToDependencySource(Type pluginType)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}