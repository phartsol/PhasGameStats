using Service.Repository;
using System;

namespace Service.Factory
{
    public interface IGameStatsUnitOfWork : IDisposable
    {
        IGameStatsRepository GameStatsRepository { get; }
    }
}
