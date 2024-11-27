﻿using ExcepcionesPropias;
using LogicaNegocio.EntidadesDominio;
using LogicaNegocio.ValueObject;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Mappers
{
    public class UsuarioMapper
    {

        public static Usuario FromLogin(LoginDTO dto)//PARA MAPEAR DTO A USUARIO 
        {
            //ALTA DE USUARIO
            if (dto != null)
            {
                Email mail = new Email(dto.Email);
                Contrasenia pass = new Contrasenia(dto.Password);
                DateTime fechaFicticia = new DateTime(2001, 1, 1); // Año, Mes, Día
                Usuario usu = new Usuario(mail, pass, 0, "", fechaFicticia);
                return usu;
            }

            return null;
        }
        public static ListadoUsuariosDTO FromUsuario(Usuario usu)//PARA AGREGAR USUARIO A IENUMERABLE PARA LISTADO
        {
            ListadoUsuariosDTO dto = new ListadoUsuariosDTO()
            {
                Email = usu.Email.Valor,
                NombreRol = usu.Rol.Nombre,
                Id = usu.Id,
            };

            return dto;
        }

        //PARA OBTENER LISTADO DE USUARIOS
        public static IEnumerable<ListadoUsuariosDTO> FromUsuarios(IEnumerable<Usuario> usuarios)
        {
            List<ListadoUsuariosDTO> dtos = new List<ListadoUsuariosDTO>();

            foreach (Usuario usu in usuarios)
            {
                dtos.Add(FromUsuario(usu));
            }

            return dtos;
        }

        
    }
}
