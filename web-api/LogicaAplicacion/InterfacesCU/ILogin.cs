﻿using DTO;
using LogicaNegocio.EntidadesDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCU
{
    public interface ILogin
    {
        ListadoUsuariosDTO Loguear(LoginDTO login);
    }
}
