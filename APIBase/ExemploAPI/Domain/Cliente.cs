﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Domain
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }   
        public bool Ativo { get; set; }
    }
}
