using DataAccess.Infrastructure;
using System;
using System.Data;
using DataAccess.Repositories.Interfaces;
using Shared.Commands;
using Shared.Handlers;

namespace DataAccess.Repositories
{
    public class UtilityRepository : IUtilityRepository
    {
        readonly IConnectionFactory _connectionFactory;
        private readonly LoggingCommandHandlerDecorator<LogCommand> _logHandler;
        private IDbTransaction Transaction { get; set; }
        IDbTransaction IUtilityRepository.Transaction { get => Transaction; set => Transaction = value; }

        public UtilityRepository(IConnectionFactory connectionFactory, LoggingCommandHandlerDecorator<LogCommand> loghandler)
        {
            _connectionFactory = connectionFactory;
            _logHandler = loghandler ?? throw new ArgumentNullException(nameof(loghandler), @"logging commandHandlerDecorator instance is null");
        }

        IDbConnection IUtilityRepository.GetConnection()
        {
            return _connectionFactory.GetConnection;
        }
    }
}

