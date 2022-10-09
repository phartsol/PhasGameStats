using Data.Model;
using Service.Repository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Service.Factory
{
    public class GameStatsUnitOfWork : IGameStatsUnitOfWork, IDisposable
    {
        public GameStatsUnitOfWork()
        {
        }


        private GameStatEntities _entities;
        public GameStatEntities localEntities
        {
            get
            {
                return _entities ?? (_entities = new GameStatEntities());
            }
        }


        private GameStatsRepository _gameStatsRepository;


        public IGameStatsRepository GameStatsRepository
        {
            get
            {
                return _gameStatsRepository ?? (_gameStatsRepository = new GameStatsRepository(localEntities));
            }
        }

        //IGameStatsRepository IGameStatsUnitOfWork.GameStatsRepository => throw new NotImplementedException();

        public void Dispose()
        {
            try
            {
                if (_entities != null)
                {
                    _entities = null;
                }
            }
            catch
            {
                throw new NotImplementedException();
            }
        }




    }
}
