﻿using projetoAPPLivraria.Models;

namespace projetoAPPLivraria.Repository.Contract
{
    public interface IStatusRepository
    {
        IEnumerable<Status> obterStatus();

    }
}
